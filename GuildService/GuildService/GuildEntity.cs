using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Devcat.Core;
using Devcat.Core.Threading;
using GuildService.API;
using ServiceCore;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.GuildServiceOperations;
using ServiceCore.HeroesContents;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService
{
	public class GuildEntity
	{
		public GuildChatRoom ChatRoom { get; set; }

		public GuildService Service { get; set; }

		public IEntity Entity { get; set; }

		public long GuildID { get; set; }

		public int GuildSN { get; set; }

		public InGameGuildInfo GuildInfo { get; set; }

		public Dictionary<string, GuildMember> GuildMemberDict { get; set; }

		public Dictionary<string, OnlineGuildMember> OnlineMembers { get; set; }

		public bool IsInitialized
		{
			get
			{
				return this.GuildInfo != null && this.GuildMemberDict != null;
			}
		}

		public event Action InitializCompleted;

		public event EventHandler NewbieRecommendChanged;

		public GuildStorageManager Storage { get; set; }

		public bool IsQueriedGuildInfo { get; set; }

		public GuildEntity(GuildService service, IEntity entity, long guildID)
		{
			this.Service = service;
			this.Entity = entity;
			this.GuildID = guildID;
			this.GuildSN = (int)guildID;
			this.GuildInfo = null;
			this.GuildMemberDict = null;
			this.OnlineMembers = new Dictionary<string, OnlineGuildMember>();
			this.IsQueriedGuildInfo = false;
			this.InitializeSyncSystem();
			this.QueryGuildInfo();
			this.Storage = new GuildStorageManager(this);
			this.ChatRoom = new GuildChatRoom(service, guildID);
		}

		public override string ToString()
		{
			if (this.IsInitialized)
			{
				return string.Format("Guild({0})", this.GuildInfo.GuildName);
			}
			return string.Format("Guild({0})", this.GuildID);
		}

		public int GetRegularMemberCount()
		{
			int num = 0;
			foreach (GuildMember guildMember in this.GuildMemberDict.Values)
			{
				if (guildMember.Rank.IsRegularMember())
				{
					num++;
				}
			}
			return num;
		}

		public int GetMemberCount()
		{
			int num = 0;
			foreach (GuildMember guildMember in this.GuildMemberDict.Values)
			{
				if (guildMember.Rank.IsMember())
				{
					num++;
				}
			}
			return num;
		}

		public void QueryGuildInfo()
		{
			if (!this.IsInitialized)
			{
				if (this.IsQueriedGuildInfo)
				{
					Log<GuildEntity>.Logger.WarnFormat("Already Queried Guild Info [IsQueriedGuildInfo = true,  GID = {0}]", this.GuildID);
				}
				JobProcessor mainThread = JobProcessor.Current;
				JobProcessor jobThread = new JobProcessor();
				jobThread.ExceptionOccur += delegate(object s, EventArgs<Exception> e)
				{
					Log<GuildService>.Logger.Fatal("Exception occurred in AsyncFunc QueryGuildInfo Thread", e.Value);
				};
				jobThread.Start();
				jobThread.Enqueue(Job.Create(delegate
				{
					HeroesGuildInfo guildInfo = null;
					List<HeroesGuildMemberInfo> list = new List<HeroesGuildMemberInfo>();
					GetInGameGuildInfo guildResult = new GetInGameGuildInfo();
					try
					{
						if (this.GuildInfo == null)
						{
							guildInfo = GuildAPI.GetAPI().GetGuildInfo(this.GuildSN);
						}
						if (this.GuildMemberDict == null)
						{
							list = GuildAPI.GetAPI().GetMembers(this.GuildSN);
						}
						if (ServiceCore.FeatureMatrix.IsEnable("NewbieGuildRecommend"))
						{
							HeroesDataContext heroesDataContext = new HeroesDataContext();
							guildResult = heroesDataContext.GetGuildInfo(this.GuildSN);
						}
					}
					catch (Exception ex)
					{
						Log<GuildService>.Logger.Error("Exception occurred in QueryGuildInfo", ex);
					}
					mainThread.Enqueue(Job.Create(delegate
					{
						if (this.GuildInfo == null)
						{
							this.LoadGroupInfo(guildInfo);
						}
						if (this.GuildMemberDict == null)
						{
							this.LoadGroupMemberInfo(list);
						}
						if (this.GuildInfo != null)
						{
							this.GuildInfo.IsNewbieRecommend = guildResult.NewbieRecommend;
							this.GuildInfo.GuildLevel = guildResult.Level;
							this.GuildInfo.GuildPoint = guildResult.Exp;
							this.GuildInfo.GuildNotice = guildResult.Notice;
						}
						if (this.InitializCompleted != null)
						{
							this.InitializCompleted();
							this.InitializCompleted = null;
						}
						if (ServiceCore.FeatureMatrix.IsEnable("GuildWebChat"))
						{
							if (this.Service.ChatRelay != null)
							{
								this.Service.ChatRelay.RequestMemberInfos(this.GuildID);
							}
							else
							{
								Log<GuildEntity>.Logger.ErrorFormat("cannot to requestMemberInfos call.", new object[0]);
							}
						}
						this.Sync();
						jobThread.Stop(false);
					}));
				}));
			}
			this.IsQueriedGuildInfo = true;
		}

		public void LoadGroupInfo(HeroesGuildInfo info)
		{
			if (info != null)
			{
				this.GuildInfo = info.ToGuildInfo();
				if (this.GuildInfo != null)
				{
					Log<GuildEntity>.Logger.WarnFormat("Loading Guild info finished. [GID = {0}]", this.GuildID);
					return;
				}
			}
			else
			{
				Log<GuildEntity>.Logger.ErrorFormat("Loading Guild Info failed! [GID = {0}]", this.GuildID);
			}
		}

		public void LoadGroupMemberInfo(List<HeroesGuildMemberInfo> list)
		{
			if (list != null)
			{
				this.GuildMemberDict = new Dictionary<string, GuildMember>();
				using (HeroesDataContext heroesDataContext = new HeroesDataContext())
				{
					foreach (HeroesGuildMemberInfo groupMemberInfo in list)
					{
						GuildMember guildMember = new GuildMember(this, groupMemberInfo, heroesDataContext);
						if (guildMember.IsValid())
						{
							this.GuildMemberDict[guildMember.Key.CharacterName] = guildMember;
						}
					}
				}
				if (this.GuildMemberDict != null)
				{
					Log<GuildEntity>.Logger.WarnFormat("Loading Guild Members finished. [GID = {0}] [{1} members]", this.GuildID, list.Count);
					return;
				}
			}
			else
			{
				Log<GuildEntity>.Logger.ErrorFormat("Loading Guild Members Failed! [GID = {0}]", this.GuildID);
			}
		}

		public GuildMember GetGuildMember(string name)
		{
			return this.GuildMemberDict.TryGetValue(name);
		}

		public OnlineGuildMember GetOnlineMember(long CID)
		{
			foreach (OnlineGuildMember onlineGuildMember in this.OnlineMembers.Values)
			{
				if (onlineGuildMember.Key.CID == CID)
				{
					return onlineGuildMember;
				}
			}
			return null;
		}

		public int GetMaxMemberCount()
		{
			if (this.GuildInfo.MaxMemberCount > 0)
			{
				return this.GuildInfo.MaxMemberCount;
			}
			return 0;
		}

		public void Close()
		{
			this.Storage.Close();
			if (this.ChatRoom != null)
			{
				this.ChatRoom.LeaveAllMembers(this.GuildID);
			}
		}

		public int GiveGP(int gp, GPGainType gainType, long CID)
		{
			if (gp == 0 || !ServiceCore.FeatureMatrix.IsEnable("GuildLevel"))
			{
				return 0;
			}
			if (ServiceCore.FeatureMatrix.IsEnable("Rank_GuildPointRank"))
			{
				using (HeroesDataContext heroesDataContext = new HeroesDataContext())
				{
					heroesDataContext.UpdateGuildInfo(new int?(ServiceCore.FeatureMatrix.GameCode), new int?(GuildAPI.ServerCode), new int?(this.GuildSN), null, null, null, null, null, new int?(ServiceCore.FeatureMatrix.GetInteger("InGameGuild_MaxMember")), new long?((long)gp));
				}
			}
			int num = GuildContents.GuildLevelUpInfoDic.Keys.Max() + 1;
			if (this.GuildInfo.GuildLevel >= num)
			{
				return 0;
			}
			GuildLevelUpInfo guildLevelUpInfo;
			if (!GuildContents.GuildLevelUpInfoDic.TryGetValue(this.GuildInfo.GuildLevel, out guildLevelUpInfo))
			{
				return 0;
			}
			if (GPGainTypeUtil.IsValidGPGainTypeValue(gainType, false))
			{
				int num2 = 0;
				if (!this.GuildInfo.DailyGainGP.TryGetValue((byte)gainType, out num2))
				{
					this.GuildInfo.DailyGainGP.Add((byte)gainType, 0);
				}
				if (gp > 0)
				{
					int dailyGPLimit = GuildContents.GetDailyGPLimit(gainType);
					if (dailyGPLimit >= 0)
					{
						if (num2 >= dailyGPLimit)
						{
							return 0;
						}
						if (num2 + gp > dailyGPLimit)
						{
							gp = dailyGPLimit - num2;
							if (gp < 0)
							{
								return 0;
							}
						}
					}
				}
				else
				{
					if (num2 + gp < 0)
					{
						gp = -num2;
					}
					if (gp == 0)
					{
						return 0;
					}
				}
				Dictionary<byte, int> dailyGainGP;
				(dailyGainGP = this.GuildInfo.DailyGainGP)[(byte)gainType] = dailyGainGP[(byte)gainType] + gp;
				using (HeroesDataContext heroesDataContext2 = new HeroesDataContext())
				{
					heroesDataContext2.UpdateInGameGuildDailyGainGP(this.GuildInfo.GuildSN, gainType, this.GuildInfo.DailyGainGP[(byte)gainType]);
				}
				GuildGainGPMessage serializeObject = new GuildGainGPMessage(this.GuildInfo.GuildPoint, this.GuildInfo.DailyGainGP);
				foreach (OnlineGuildMember onlineGuildMember in this.OnlineMembers.Values)
				{
					onlineGuildMember.RequestFrontendOperation(SendPacket.Create<GuildGainGPMessage>(serializeObject));
				}
			}
			GuildLog.AddGuildLedger(new LogData((long)this.GuildSN, CID, OperationType.GainGuildPoint, GuildLedgerEventType.UpdateGuildPoint, this.GuildInfo.GuildLevel.ToString(), (this.GuildInfo.GuildPoint + (long)gp).ToString()));
			long num3 = this.GuildInfo.GuildPoint + (long)gp - guildLevelUpInfo.RequiredExp;
			if (num3 >= 0L)
			{
				while (num3 >= 0L)
				{
					int guildLevel = this.GuildInfo.GuildLevel;
					this.GuildInfo.GuildLevel++;
					this.GuildInfo.GuildPoint = num3;
					GuildLog.AddGuildLedger(new LogData((long)this.GuildSN, CID, OperationType.GuildLevelUp, GuildLedgerEventType.LevelUp, this.GuildInfo.GuildLevel.ToString(), this.GuildInfo.GuildPoint.ToString()));
					this.BroadcastGuildLevelUp();
					this.GiveLevelUpRewardToMembers(guildLevel);
					if (this.GuildInfo.GuildLevel >= num)
					{
						this.GuildInfo.GuildPoint = 0L;
						break;
					}
					if (!GuildContents.GuildLevelUpInfoDic.TryGetValue(this.GuildInfo.GuildLevel, out guildLevelUpInfo))
					{
						Log<GuildEntity>.Logger.ErrorFormat("No proper guild level-up info is loaded. GuildLevel: {0}", this.GuildInfo.GuildLevel);
						break;
					}
					num3 = this.GuildInfo.GuildPoint - guildLevelUpInfo.RequiredExp;
					this.ReportGuildLevelChanged();
				}
			}
			else
			{
				this.GuildInfo.GuildPoint += (long)gp;
			}
			return gp;
		}

		public void SetGuildLevel(int level, int gpPercent)
		{
			int num = GuildContents.GuildLevelUpInfoDic.Keys.Max() + 1;
			if (level < 1 || num < level)
			{
				return;
			}
			if (gpPercent < 0 || 100 <= gpPercent)
			{
				return;
			}
			if (!ServiceCore.FeatureMatrix.IsEnable("GuildLevel"))
			{
				return;
			}
			this.GuildInfo.GuildLevel = level;
			GuildLevelUpInfo guildLevelUpInfo = GuildContents.GuildLevelUpInfoDic.TryGetValue(level);
			if (guildLevelUpInfo == null)
			{
				this.GuildInfo.GuildPoint = 0L;
			}
			else
			{
				this.GuildInfo.GuildPoint = guildLevelUpInfo.RequiredExp * (long)gpPercent / 100L;
			}
			this.ReportGuildLevelChanged();
			this.BroadcastGuildLevelUp();
			this.GiveLevelUpRewardToMembers(level - 1);
		}

		private void BroadcastGuildLevelUp()
		{
			GuildLevelUpMessage message = new GuildLevelUpMessage
			{
				Level = this.GuildInfo.GuildLevel
			};
			foreach (OnlineGuildMember onlineGuildMember in this.OnlineMembers.Values)
			{
				onlineGuildMember.SendMessage<GuildLevelUpMessage>(message);
			}
		}

		private void GiveLevelUpRewardToMembers(int rewardGuildLevel)
		{
			string guildLevelUpRewardItemClass = GuildContents.GetGuildLevelUpRewardItemClass(rewardGuildLevel);
			if (guildLevelUpRewardItemClass == null || guildLevelUpRewardItemClass.Length == 0)
			{
				return;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary[guildLevelUpRewardItemClass] = 1;
			string mailTitle = string.Format("GameUI_Heroes_GuildLevelUpItemMail_Title", new object[0]);
			string mailContent = string.Format("GameUI_Heroes_GuildLevelUpItemMail_Content,{0},{1}", this.GuildInfo.GuildName, this.GuildInfo.GuildLevel);
			List<long> list = new List<long>();
			foreach (KeyValuePair<string, GuildMember> keyValuePair in this.GuildMemberDict)
			{
				if (keyValuePair.Value.Rank.IsRegularMember())
				{
					HeroesDataContext heroesDataContext = new HeroesDataContext();
					int num = 0;
					ISingleResult<GetGuildCharacterMaxLevel> guildCharacterMaxLevel = heroesDataContext.GetGuildCharacterMaxLevel(new long?(keyValuePair.Value.Key.CID));
					using (IEnumerator<GetGuildCharacterMaxLevel> enumerator2 = guildCharacterMaxLevel.GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							GetGuildCharacterMaxLevel getGuildCharacterMaxLevel = enumerator2.Current;
							num = ((getGuildCharacterMaxLevel == null) ? 0 : getGuildCharacterMaxLevel.MaxLevel);
						}
					}
					if (num < rewardGuildLevel + 1)
					{
						OnlineGuildMember onlineMember;
						if (this.OnlineMembers.TryGetValue(keyValuePair.Key, out onlineMember))
						{
							this.SendItemToMail(onlineMember, dictionary, mailTitle, mailContent);
						}
						else
						{
							list.Add(keyValuePair.Value.Key.CID);
						}
						heroesDataContext.UpdateGuildCharacterMaxLevel(new long?(keyValuePair.Value.Key.CID), new int?(rewardGuildLevel + 1));
					}
				}
			}
			if (list.Count > 0)
			{
				this.SendItemToQueue(list, dictionary, mailTitle, mailContent);
			}
		}

		private void SendItemToMail(OnlineGuildMember onlineMember, Dictionary<string, int> item, string mailTitle, string mailContent)
		{
			string itemClass = item.Keys.FirstOrDefault<string>();
			int itemCount = item.Values.FirstOrDefault<int>();
			GuildLog.AddGuildLedger(new LogData((long)this.GuildSN, onlineMember.CID, OperationType.GuildLevelUp, GuildLedgerEventType.SendRewardItemMail, itemClass));
			SendSystemMail op = new SendSystemMail(MailType.GuildLevelUpItemMail, onlineMember.CID, mailTitle, mailContent, -1L, itemClass, itemCount, 0);
			op.OnComplete += delegate(Operation _)
			{
				if (op.ResultCode == MailSentMessage.ErrorCodeEnum.Successful)
				{
					GuildLog.AddGuildLedger(new LogData((long)this.GuildSN, onlineMember.CID, OperationType.GuildLevelUp, GuildLedgerEventType.SendRewardItemMailComplete, itemClass));
					return;
				}
				GuildLog.AddGuildLedger(new LogData((long)this.GuildSN, onlineMember.CID, OperationType.GuildLevelUp, GuildLedgerEventType.SendRewardItemMailFail, itemClass));
				List<long> list = new List<long>();
				list.Add(onlineMember.CID);
				this.SendItemToQueue(list, item, mailTitle, mailContent);
			};
			op.OnFail += delegate(Operation _)
			{
				GuildLog.AddGuildLedger(new LogData((long)this.GuildSN, onlineMember.CID, OperationType.GuildLevelUp, GuildLedgerEventType.SendRewardItemMailFail, itemClass));
				List<long> list = new List<long>();
				list.Add(onlineMember.CID);
				this.SendItemToQueue(list, item, mailTitle, mailContent);
			};
			onlineMember.PlayerConn.RequestOperation(op);
		}

		private void SendItemToQueue(List<long> queuedItemOwnerCIDList, Dictionary<string, int> item, string mailTitle, string mailContent)
		{
			TimeSpan t = TimeSpan.FromDays(7.0);
			string itemClass = item.Keys.FirstOrDefault<string>();
			using (List<long>.Enumerator enumerator = queuedItemOwnerCIDList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					long cid = enumerator.Current;
					long guildSN = (long)this.GuildSN;
					long cid5 = cid;
					OperationType operation = OperationType.GuildLevelUp;
					GuildLedgerEventType eventType = GuildLedgerEventType.SendRewardItemToQueue;
					long cid2 = cid;
					GuildLog.AddGuildLedger(new LogData(guildSN, cid5, operation, eventType, cid2.ToString(), itemClass));
					StoreToQueuedItem storeToQueuedItem = new StoreToQueuedItem
					{
						CID = cid,
						ItemDictionary = item,
						EventCode = "GuildLevelUp",
						MailTitle = mailTitle,
						MailContent = mailContent,
						ExpireTime = DateTime.UtcNow + t
					};
					storeToQueuedItem.OnComplete += delegate(Operation _)
					{
						long guildSN2 = (long)this.GuildSN;
						long cid3 = cid;
						OperationType operation2 = OperationType.GuildLevelUp;
						GuildLedgerEventType eventType2 = GuildLedgerEventType.SendRewardItemToQueueComplete;
						long cid4 = cid;
						GuildLog.AddGuildLedger(new LogData(guildSN2, cid3, operation2, eventType2, cid4.ToString(), itemClass));
					};
					storeToQueuedItem.OnFail += delegate(Operation ___)
					{
						Log<GuildEntity>.Logger.ErrorFormat("Error while storing guild level up reward item to queue. CID: {0}, ItemClass: {1}", cid, itemClass);
						long guildSN2 = (long)this.GuildSN;
						long cid3 = cid;
						OperationType operation2 = OperationType.GuildLevelUp;
						GuildLedgerEventType eventType2 = GuildLedgerEventType.SendRewardItemToQueueFail;
						long cid4 = cid;
						GuildLog.AddGuildLedger(new LogData(guildSN2, cid3, operation2, eventType2, cid4.ToString(), itemClass));
					};
					this.Service.RequestOperation("PlayerService.PlayerService", storeToQueuedItem);
				}
			}
		}

		public bool ChangeGuildMaster(GuildMember newMaster, GuildMember oldMaster)
		{
			bool result;
			try
			{
				result = GuildAPI.GetAPI().ChangeMaster(this, newMaster, oldMaster);
			}
			catch (Exception ex)
			{
				Log<GuildEntity>.Logger.Error("Error while ChangeGuildMaster", ex);
				result = false;
			}
			return result;
		}

		public void Connect(GuildMemberKey key, long fid)
		{
			OnlineGuildMember value = new OnlineGuildMember(this, key, fid);
			if (this.OnlineMembers.ContainsKey(key.CharacterName))
			{
				Log<GuildEntity>.Logger.FatalFormat("Duplicate connection. [Name {0} GuildSN {1}]", key.CharacterName, this.GuildSN);
			}
			this.OnlineMembers[key.CharacterName] = value;
			this.ReportGuildMemberConnected(key);
			if (this.IsInitialized)
			{
				if (this.GuildMemberDict.ContainsKey(key.CharacterName))
				{
					this.ReportGuildMemberChanged(key);
					this.Sync();
				}
				else
				{
					string text = null;
					foreach (GuildMember guildMember in this.GuildMemberDict.Values)
					{
						if (guildMember.Key.CID == key.CID)
						{
							text = guildMember.Key.CharacterName;
							break;
						}
					}
					if (text != null)
					{
						this.GuildMemberDict[key.CharacterName] = new GuildMember(this, GuildAPI.GetAPI().GetMemberInfo(this, key));
						this.GuildMemberDict.Remove(text);
						if (this.GuildInfo.MasterName == text)
						{
							this.GuildInfo.MasterName = key.CharacterName;
							this.ReportGuildInfoChanged();
						}
						foreach (OnlineGuildMember onlineGuildMember in this.OnlineMembers.Values)
						{
							if (!this.NotSynchedGuildMember.Contains(onlineGuildMember.CharacterName))
							{
								this.NotSynchedGuildMember.Add(onlineGuildMember.CharacterName);
							}
						}
						this.ReportGuildMemberChanged(key);
						this.Sync();
					}
					else
					{
						Log<GuildEntity>.Logger.FatalFormat("No information on guild : Critical error. [Name {0} GuildSN {1}]", key.CharacterName, this.GuildSN);
					}
				}
				if (this.OnlineMembers.Count == 1 && this.NewbieRecommendChanged != null)
				{
					this.NewbieRecommendChanged(this, EventArgs.Empty);
				}
			}
		}

		public void Disconnect(GuildMemberKey key)
		{
			if (this.OnlineMembers.TryGetValue(key.CharacterName) == null)
			{
				return;
			}
			this.OnlineMembers.Remove(key.CharacterName);
			this.ReportGuildMemberChanged(key);
			Log<GuildEntity>.Logger.WarnFormat("[{0}] Guild Logout", key.CharacterName);
			if (this.OnlineMembers.Count == 0 && this.ChatRoom.IsEmpty)
			{
				Log<GuildEntity>.Logger.WarnFormat("Closing Guild Entity.", new object[0]);
				this.Entity.Close();
			}
		}

		public void UpdateGroupMemberInfo(GuildMemberKey key, HeroesGuildMemberInfo info)
		{
			GuildMember guildMember = this.GetGuildMember(key.CharacterName);
			if (info == null || info.emGroupUserType.ToGuildMemberRank().IsInvalid())
			{
				this.GuildMemberDict.Remove(key.CharacterName);
				if (guildMember != null && guildMember.Rank.IsRegularMember())
				{
					this.ReportGuildInfoChanged();
				}
			}
			else
			{
				this.GuildMemberDict[key.CharacterName] = new GuildMember(this, info);
				if (guildMember != null && info != null && guildMember.Rank == GuildMemberRank.Wait && info.emGroupUserType.ToGuildMemberRank().IsRegularMember())
				{
					this.ReportGuildInfoChanged();
				}
			}
			this.ReportGuildMemberChanged(key);
		}

		public void UpdateGroupInfo()
		{
			this.ReportGuildInfoChanged();
		}

		public List<GuildOperationResult> DoGuildOperation(List<GuildOperationInfo> opList, GuildMemberRank operatorRank)
		{
			List<GuildOperationResult> list = new List<GuildOperationResult>();
			int num = 0;
			foreach (GuildOperationInfo guildOperationInfo in opList)
			{
				if (!(guildOperationInfo.Target == " "))
				{
					if (this.GetGuildMember(guildOperationInfo.Target) == null)
					{
						Log<GuildEntity>.Logger.ErrorFormat("No member(opList in DoGuildOperation). [ {0} ]", guildOperationInfo.Target);
					}
					else if (guildOperationInfo.Command == "Join" && guildOperationInfo.Value == 1)
					{
						num++;
					}
				}
			}
			int num2 = 0;
			try
			{
				HeroesDataContext heroesDataContext = new HeroesDataContext();
				num2 = heroesDataContext.GetMaxMemberLimit(this.GuildSN);
			}
			catch (Exception ex)
			{
				Log<GuildEntity>.Logger.Error("Error while getMaxMemberLimit in DoGuildOperation", ex);
			}
			if (num + this.GetRegularMemberCount() > num2)
			{
				list.Add(new GuildOperationResult(false, null, null));
				return list;
			}
			foreach (GuildOperationInfo guildOperationInfo2 in opList)
			{
				if (ServiceCore.FeatureMatrix.IsEnable("NewbieGuildRecommend") && guildOperationInfo2.Target == " ")
				{
					bool flag = false;
					if (guildOperationInfo2.Command == "NewbieRecommend")
					{
						bool flag2 = Convert.ToBoolean(guildOperationInfo2.Value);
						try
						{
							if (operatorRank.HasNewbieRecommendPermission())
							{
								HeroesDataContext heroesDataContext2 = new HeroesDataContext();
								flag = heroesDataContext2.UpdateNewbieRecommend(this.GuildSN, flag2);
							}
						}
						catch (Exception ex2)
						{
							Log<GuildEntity>.Logger.Error(string.Format("Error on Operation. [ {0} {1} {2}]", guildOperationInfo2.Command, guildOperationInfo2.ToString(), operatorRank.ToString()), ex2);
							flag = false;
						}
						if (!flag)
						{
							list.Add(new GuildOperationResult(false, null, null));
						}
						else
						{
							this.GuildInfo.IsNewbieRecommend = flag2;
							this.ReportGuildInfoChanged();
							if (this.NewbieRecommendChanged != null)
							{
								this.NewbieRecommendChanged(this, EventArgs.Empty);
							}
						}
					}
				}
				else
				{
					bool flag3 = false;
					GuildMember guildMember = this.GetGuildMember(guildOperationInfo2.Target);
					if (guildMember == null)
					{
						Log<GuildEntity>.Logger.ErrorFormat("No member (NewbieGuildRecommend == false). [ {0} ]", guildOperationInfo2.Target);
					}
					else
					{
						try
						{
							if (guildOperationInfo2.Command == "Join")
							{
								flag3 = GuildAPI.GetAPI().AcceptJoin(this, operatorRank, guildMember, guildOperationInfo2.Value == 1);
							}
							else if (guildOperationInfo2.Command == "Rank")
							{
								Log<GuildEntity>.Logger.Info(string.Format("Rank [ {0} ]", ((GuildMemberRank)guildOperationInfo2.Value).ToGroupUserType()));
								flag3 = GuildAPI.GetAPI().ChangeRank(this, operatorRank, guildMember, (GuildMemberRank)guildOperationInfo2.Value);
							}
						}
						catch (Exception ex3)
						{
							Log<GuildEntity>.Logger.Error(string.Format("Error on Operation. [ {0} {1} {2}]", guildOperationInfo2, operatorRank.ToString(), guildMember.Key.ToString()), ex3);
							flag3 = false;
						}
					}
					if (flag3)
					{
						list.Add(new GuildOperationResult(true, guildMember.Key, GuildAPI.GetAPI().GetMemberInfo(this, guildMember.Key)));
					}
					else
					{
						list.Add(new GuildOperationResult(false, null, null));
					}
				}
			}
			return list;
		}

		public void RequestUpdateBriefGuildInfo(List<GuildOperationInfo> opList)
		{
			foreach (GuildOperationInfo guildOperationInfo in opList)
			{
				try
				{
					if (guildOperationInfo.Command == "Join")
					{
						if (guildOperationInfo.Value == 1)
						{
							GuildMember guildMember = this.GetGuildMember(guildOperationInfo.Target);
							if (guildMember == null)
							{
								Log<GuildEntity>.Logger.ErrorFormat("No member (RequestUpdateBriefGuildInfo). [ {0} ]", guildOperationInfo.Target);
							}
							else if (this.GuildMemberDict != null && this.GuildMemberDict.ContainsKey(guildMember.Key.CharacterName))
							{
								GuildMemberRank rank = this.GuildMemberDict[guildMember.Key.CharacterName].Rank;
								if (!rank.IsInvalid())
								{
									OnlineGuildMember onlineGuildMember = this.OnlineMembers.TryGetValue(guildMember.Key.CharacterName);
									if (onlineGuildMember != null && this.GuildInfo != null)
									{
										onlineGuildMember.PlayerConn.RequestOperation(new UpdateBriefGuildInfo(new BriefGuildInfo(this.GuildID, this.GuildInfo.GuildName, this.GuildInfo.GuildLevel, rank, this.GetMaxMemberCount()), true));
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Log<GuildEntity>.Logger.Error(string.Format("Error on Operation. [ {0} {1}]", guildOperationInfo.Command, guildOperationInfo.ToString()), ex);
				}
			}
		}

		public void GiveGuildLevelUpRewardAP(List<GuildOperationInfo> opList)
		{
			if (this.GuildInfo == null)
			{
				return;
			}
			foreach (GuildOperationInfo guildOperationInfo in opList)
			{
				try
				{
					if (guildOperationInfo.Command == "Join")
					{
						if (guildOperationInfo.Value == 1)
						{
							GuildMember guildMember = this.GetGuildMember(guildOperationInfo.Target);
							if (guildMember == null)
							{
								Log<GuildEntity>.Logger.ErrorFormat("No member (RequestUpdateBriefGuildInfo). [ {0} ]", guildOperationInfo.Target);
							}
							else if (this.GuildMemberDict != null && this.GuildMemberDict.ContainsKey(guildMember.Key.CharacterName))
							{
								GuildMemberRank rank = this.GuildMemberDict[guildMember.Key.CharacterName].Rank;
								if (!rank.IsInvalid())
								{
									HeroesDataContext heroesDataContext = new HeroesDataContext();
									int num = 0;
									ISingleResult<GetGuildCharacterMaxLevel> guildCharacterMaxLevel = heroesDataContext.GetGuildCharacterMaxLevel(new long?(guildMember.Key.CID));
									using (IEnumerator<GetGuildCharacterMaxLevel> enumerator2 = guildCharacterMaxLevel.GetEnumerator())
									{
										if (enumerator2.MoveNext())
										{
											GetGuildCharacterMaxLevel getGuildCharacterMaxLevel = enumerator2.Current;
											num = ((getGuildCharacterMaxLevel == null) ? 0 : getGuildCharacterMaxLevel.MaxLevel);
										}
									}
									if (num < this.GuildInfo.GuildLevel)
									{
										string mailTitle = string.Format("GameUI_Heroes_GuildLevelUpItemMail_Title", new object[0]);
										for (int i = num; i < this.GuildInfo.GuildLevel; i++)
										{
											Dictionary<string, int> dictionary = new Dictionary<string, int>();
											string guildLevelUpRewardItemClass = GuildContents.GetGuildLevelUpRewardItemClass(i);
											if (guildLevelUpRewardItemClass != null && guildLevelUpRewardItemClass.Length != 0)
											{
												string mailContent = string.Format("GameUI_Heroes_GuildLevelUpItemMail_Content,{0},{1}", this.GuildInfo.GuildName, i + 1);
												dictionary[guildLevelUpRewardItemClass] = 1;
												OnlineGuildMember onlineGuildMember = this.OnlineMembers.TryGetValue(guildMember.Key.CharacterName);
												if (onlineGuildMember != null)
												{
													this.SendItemToMail(onlineGuildMember, dictionary, mailTitle, mailContent);
												}
												else
												{
													this.SendItemToQueue(new List<long>
													{
														guildMember.Key.CID
													}, dictionary, mailTitle, mailContent);
												}
											}
										}
										heroesDataContext.UpdateGuildCharacterMaxLevel(new long?(guildMember.Key.CID), new int?(this.GuildInfo.GuildLevel));
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Log<GuildEntity>.Logger.Error(string.Format("Error on Operation. [ {0} {1}]", guildOperationInfo.Command, guildOperationInfo.ToString()), ex);
				}
			}
		}

		public IEnumerable<GuildOperationResult> SyncOperationResult(List<GuildOperationResult> opResultList)
		{
			foreach (GuildOperationResult opResult in opResultList)
			{
				if (opResult.Result)
				{
					this.UpdateGroupMemberInfo(opResult.Key, opResult.HeroesGuildMemberInfo);
					yield return opResult;
				}
				else
				{
					yield return null;
				}
			}
			this.Sync();
			yield break;
		}

		private bool IsGuildInfoChanged { get; set; }

		private bool IsGuildLevelChanged { get; set; }

		private HashSet<string> ChangedGuildMember { get; set; }

		private HashSet<string> NotSynchedGuildMember { get; set; }

		private void InitializeSyncSystem()
		{
			this.IsGuildInfoChanged = false;
			this.IsGuildLevelChanged = false;
			this.ChangedGuildMember = new HashSet<string>();
			this.NotSynchedGuildMember = new HashSet<string>();
		}

		public void ReportGuildInfoChanged()
		{
			this.IsGuildInfoChanged = true;
		}

		public void ReportGuildLevelChanged()
		{
			this.IsGuildLevelChanged = true;
		}

		public void ReportGuildMemberChanged(GuildMemberKey key)
		{
			this.ChangedGuildMember.Add(key.CharacterName);
		}

		public void ReportGuildMemberConnected(GuildMemberKey key)
		{
			this.NotSynchedGuildMember.Add(key.CharacterName);
		}

		private GuildInfoMessage MakeGuildInfoMessage()
		{
			this.GuildInfo.MemberCount = this.GetRegularMemberCount();
			return new GuildInfoMessage(this.GuildInfo);
		}

		private GuildMemberListMessage MakeGuildMemberMessage_Full()
		{
			List<GuildMemberInfo> list = new List<GuildMemberInfo>();
			foreach (GuildMember guildMember in this.GuildMemberDict.Values)
			{
				if (!guildMember.Rank.IsInvalid())
				{
					GuildMemberInfo guildMemberInfo = guildMember.GetGuildMemberInfo();
					guildMemberInfo.IsOnline = this.OnlineMembers.ContainsKey(guildMemberInfo.CharacterName);
					list.Add(guildMemberInfo);
				}
			}
			return new GuildMemberListMessage(true, list);
		}

		private GuildMemberListMessage MakeGuildMemberMessage_Update()
		{
			List<GuildMemberInfo> list = new List<GuildMemberInfo>();
			foreach (string text in this.ChangedGuildMember)
			{
				if (this.GuildMemberDict.ContainsKey(text))
				{
					GuildMember guildMember = this.GuildMemberDict.TryGetValue(text);
					GuildMemberInfo guildMemberInfo = guildMember.GetGuildMemberInfo();
					guildMemberInfo.IsOnline = this.OnlineMembers.ContainsKey(guildMemberInfo.CharacterName);
					list.Add(guildMemberInfo);
				}
				else
				{
					list.Add(GuildMemberInfo.InvalidMember(text));
				}
			}
			return new GuildMemberListMessage(false, list);
		}

		private bool IsRegularMember(string name)
		{
			if (this.GuildMemberDict.ContainsKey(name))
			{
				GuildMemberRank rank = this.GuildMemberDict[name].Rank;
				return rank.IsRegularMember();
			}
			return false;
		}

        public void Sync()
        {
            if (!this.IsInitialized)
            {
                Log<GuildEntity>.Logger.Warn("Sync Not Initialized");
                return;
            }
            Log<GuildEntity>.Logger.Info("GuildSync Start");
            if (this.IsGuildInfoChanged || this.NotSynchedGuildMember.Count > 0)
            {
                GuildInfoMessage guildInfoMessage = this.MakeGuildInfoMessage();
                Log<GuildEntity>.Logger.Info("GuildInfo Send");
                if (!this.IsGuildInfoChanged)
                {
                    Log<GuildEntity>.Logger.InfoFormat("Sync NotSynchedGuildMember Count {0} Send GuildInfo", this.NotSynchedGuildMember.Count);
                    foreach (string notSynchedGuildMember in this.NotSynchedGuildMember)
                    {
                        OnlineGuildMember onlineGuildMember = this.OnlineMembers.TryGetValue<string, OnlineGuildMember>(notSynchedGuildMember);
                        if (onlineGuildMember == null)
                        {
                            continue;
                        }
                        onlineGuildMember.SendMessage<GuildInfoMessage>(guildInfoMessage);
                    }
                }
                else
                {
                    Log<GuildEntity>.Logger.InfoFormat("Sync GuildInfoChanged OnlineMember Count {0} Send GuildInfo", this.OnlineMembers.Count);
                    foreach (OnlineGuildMember value in this.OnlineMembers.Values)
                    {
                        value.SendMessage<GuildInfoMessage>(guildInfoMessage);
                    }
                }
            }
            if (this.ChangedGuildMember.Count > 0)
            {
                GuildMemberListMessage guildMemberListMessage = this.MakeGuildMemberMessage_Update();
                foreach (OnlineGuildMember value1 in this.OnlineMembers.Values)
                {
                    if (this.NotSynchedGuildMember.Contains(value1.CharacterName) || !this.IsRegularMember(value1.CharacterName))
                    {
                        continue;
                    }
                    value1.SendMessage<GuildMemberListMessage>(guildMemberListMessage);
                }
                foreach (string list in this.ChangedGuildMember.ToList<string>())
                {
                    if (this.GuildMemberDict.ContainsKey(list))
                    {
                        GuildMemberRank rank = this.GuildMemberDict[list].Rank;
                        if (!rank.IsInvalid())
                        {
                            OnlineGuildMember onlineGuildMember1 = this.OnlineMembers.TryGetValue<string, OnlineGuildMember>(list);
                            if (onlineGuildMember1 == null)
                            {
                                continue;
                            }
                            onlineGuildMember1.PlayerConn.RequestOperation(new UpdateBriefGuildInfo(new BriefGuildInfo(this.GuildID, this.GuildInfo.GuildName, this.GuildInfo.GuildLevel, rank, this.GetMaxMemberCount()), false));
                            onlineGuildMember1.SendMessage<GuildMemberListMessage>(this.MakeGuildMemberMessage_Full());
                            continue;
                        }
                    }
                    OnlineGuildMember onlineGuildMember2 = this.OnlineMembers.TryGetValue<string, OnlineGuildMember>(list);
                    if (onlineGuildMember2 == null)
                    {
                        continue;
                    }
                    Log<GuildEntity>.Logger.WarnFormat("Kicking {0}... [GID = {1}]", onlineGuildMember2.CharacterName, this.GuildID);
                    onlineGuildMember2.RequestFrontendOperation(new GuildKicked(GuildResultEnum.LeaveGuildSuccess));
                    onlineGuildMember2.Disconnect();
                }
                if (ServiceCore.FeatureMatrix.IsEnable("GuildStorage"))
                {
                    this.Storage.BroadCastInventoryInfo();
                }
            }
            if (this.NotSynchedGuildMember.Count > 0)
            {
                GuildMemberListMessage guildMemberListMessage1 = this.MakeGuildMemberMessage_Full();
                Log<GuildEntity>.Logger.InfoFormat("Sync NotSynchedGuildMember Count {0} Send MemberMessage", this.NotSynchedGuildMember.Count);
                foreach (string str in this.NotSynchedGuildMember)
                {
                    OnlineGuildMember onlineGuildMember3 = this.OnlineMembers.TryGetValue<string, OnlineGuildMember>(str);
                    if (onlineGuildMember3 == null || !this.IsRegularMember(onlineGuildMember3.CharacterName))
                    {
                        continue;
                    }
                    onlineGuildMember3.SendMessage<GuildMemberListMessage>(guildMemberListMessage1);
                }
            }
            if (this.IsGuildLevelChanged)
            {
                foreach (OnlineGuildMember value2 in this.OnlineMembers.Values)
                {
                    GuildMemberRank guildMemberRank = GuildMemberRank.Unknown;
                    if (this.GuildMemberDict.ContainsKey(value2.CharacterName))
                    {
                        guildMemberRank = this.GuildMemberDict[value2.CharacterName].Rank;
                    }
                    value2.PlayerConn.RequestOperation(new UpdateBriefGuildInfo(new BriefGuildInfo(this.GuildID, this.GuildInfo.GuildName, this.GuildInfo.GuildLevel, guildMemberRank, this.GetMaxMemberCount()), false));
                }
            }
            this.IsGuildInfoChanged = false;
            this.IsGuildLevelChanged = false;
            this.ChangedGuildMember.Clear();
            this.NotSynchedGuildMember.Clear();
        }

        public void SyncCloseGuild()
		{
			using (HeroesDataContext heroesDataContext = new HeroesDataContext())
			{
				foreach (GuildMember guildMember in this.GuildMemberDict.Values)
				{
					heroesDataContext.UpdateGuildCharacterInfo(new long?(guildMember.Key.CID), new long?(0L));
					GuildLog.AddGuildLedger(new LogData((long)this.GuildSN, guildMember.Key.CID, OperationType.GainGuildPoint, GuildLedgerEventType.UpdateGuildUserPoint, "0", "CloseGuild"));
				}
			}
			foreach (OnlineGuildMember onlineGuildMember in this.OnlineMembers.Values)
			{
				onlineGuildMember.RequestFrontendOperation(new GuildKicked(GuildResultEnum.CloseGuildSuccess));
			}
		}
	}
}
