using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Devcat.Core;
using ServiceCore;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService
{
	public class GuildStorageManager
	{
		public GuildEntity Parent { get; set; }

		public IEntityProxy ItemConn { get; set; }

		public int StorageCount { get; set; }

		public int GoldPickLimit { get; set; }

		public long AccessLimitTag { get; set; }

		public bool Processing { get; set; }

		public ICollection<SlotInfo> SlotInfo { get; set; }

		public int GoldHolding { get; set; }

		public ICollection<GuildStorageBriefLogElement> BriefTodayLogs { get; set; }

		public ICollection<GuildStorageBriefLogElement> BriefOldLogs { get; set; }

		public ICollection<GuildStorageItemLogElement> ItemTodayLogs { get; set; }

		public ICollection<GuildStorageItemLogElement> ItemOldLogs { get; set; }

		private DateTime LastLogCrawl { get; set; }

		public bool Valid
		{
			get
			{
				return this.SlotInfo != null;
			}
		}

		public GuildInventoryInfoMessage InventoryMessage
		{
			get
			{
				return new GuildInventoryInfoMessage(this.IsEnabled, this.StorageCount, this.GoldPickLimit, this.AccessLimitTag, this.SlotInfo);
			}
		}

		public bool IsEnabled
		{
			get
			{
				return this.Parent.GetRegularMemberCount() >= FeatureMatrix.GetInteger("GuildStorageRequiredMember");
			}
		}

		public GuildStorageLogsMessage LogMessage(bool isTodayLog)
		{
			if (isTodayLog)
			{
				return new GuildStorageLogsMessage(isTodayLog, this.BriefTodayLogs, this.ItemTodayLogs);
			}
			return new GuildStorageLogsMessage(isTodayLog, this.BriefOldLogs, this.ItemOldLogs);
		}

        public GuildStorageManager(GuildEntity parent)
        {
            GuildStorageManager slotCount = this;
            if (!FeatureMatrix.IsEnable("GuildStorage"))
            {
                return;
            }
            this.Parent = parent;
            this.SlotInfo = null;
            GuildService instance = GuildService.Instance;
            IEntity entity = this.Parent.Entity;
            Location location = new Location()
            {
                ID = (long)parent.GuildSN,
                Category = "PlayerService.PlayerService"
            };
            this.ItemConn = instance.Connect(entity, location);
            this.StorageCount = 0;
            this.Processing = true;
            this.BriefTodayLogs = new List<GuildStorageBriefLogElement>();
            this.BriefOldLogs = new List<GuildStorageBriefLogElement>();
            this.ItemTodayLogs = new List<GuildStorageItemLogElement>();
            this.ItemOldLogs = new List<GuildStorageItemLogElement>();
            JoinWithGuildInventory joinWithGuildInventory = new JoinWithGuildInventory();
            QuildGuildStorageStatus quildGuildStorageStatu = new QuildGuildStorageStatus();
            this.LastLogCrawl = DateTime.MinValue;
            joinWithGuildInventory.OnComplete += new Action<Operation>((Operation __) => slotCount.RequestItemServiceOperation(quildGuildStorageStatu));
            joinWithGuildInventory.OnFail += new Action<Operation>((Operation __) => this.Processing = false);
            quildGuildStorageStatu.OnComplete += new Action<Operation>((Operation __) =>
            {
                slotCount.StorageCount = quildGuildStorageStatu.Status.SlotCount;
                slotCount.ApplyStorage(quildGuildStorageStatu.Status.Inventory);
                slotCount.Processing = false;
                try
                {
                    using (HeroesDataContext heroesDataContext = new HeroesDataContext())
                    {
                        GetGuildStorageSetting getGuildStorageSetting = heroesDataContext.GetGuildStorageSetting(new int?(parent.GuildSN)).FirstOrDefault<GetGuildStorageSetting>();
                        if (getGuildStorageSetting != null)
                        {
                            slotCount.GoldPickLimit = getGuildStorageSetting.GoldLimit;
                            slotCount.AccessLimitTag = getGuildStorageSetting.AccessLimitTag;
                        }
                        else
                        {
                            heroesDataContext.InitGuildStorageSetting(new int?(parent.GuildSN));
                        }
                    }
                }
                catch (Exception exception)
                {
                    Log<GuildStorageManager>.Logger.Error(exception);
                }
                slotCount.BroadCastInventoryInfo();
            });
            quildGuildStorageStatu.OnFail += new Action<Operation>((Operation __) => this.Processing = false);
            this.ItemConn.ConnectionSucceeded += new EventHandler<EventArgs<IEntityProxy>>((object s, EventArgs<IEntityProxy> e) => slotCount.RequestItemServiceOperation(joinWithGuildInventory));
            this.QueryStorageLog(true);
        }

        public void RequestItemServiceOperation(Operation op)
		{
			this.ItemConn.RequestOperation(op);
		}

		public void Close()
		{
			if (!FeatureMatrix.IsEnable("GuildStorage"))
			{
				return;
			}
			this.ItemConn.Close();
		}

		public void BroadCastInventoryInfo()
		{
			foreach (OnlineGuildMember onlineGuildMember in this.Parent.OnlineMembers.Values)
			{
				if (onlineGuildMember.IsGuildStorageListening)
				{
					onlineGuildMember.SendGuildStorageInfoMessage();
				}
			}
		}

		public void QueryStorageLog(bool reloadOldLog)
		{
			reloadOldLog |= (DateTime.Now.Date != this.LastLogCrawl.Date);
			using (HeroesDataContext heroesDataContext = new HeroesDataContext())
			{
				ISingleResult<GetGuildStorageBriefLogsToday> guildStorageBriefLogsToday = heroesDataContext.GetGuildStorageBriefLogsToday(new int?(this.Parent.GuildSN));
				this.BriefTodayLogs.Clear();
				foreach (GetGuildStorageBriefLogsToday getGuildStorageBriefLogsToday in guildStorageBriefLogsToday)
				{
					GuildStorageBriefLogElement item = new GuildStorageBriefLogElement
					{
						CharacterName = getGuildStorageBriefLogsToday.CharacterName,
						OperationType = (GuildStorageOperationType)getGuildStorageBriefLogsToday.OperationType,
						AddCount = getGuildStorageBriefLogsToday.AddCount,
						PickCount = getGuildStorageBriefLogsToday.PickCount,
						Datestamp = getGuildStorageBriefLogsToday.Datestamp,
						Timestamp = getGuildStorageBriefLogsToday.Timestamp
					};
					this.BriefTodayLogs.Add(item);
				}
				ISingleResult<GetGuildStorageItemLogsToday> guildStorageItemLogsToday = heroesDataContext.GetGuildStorageItemLogsToday(new int?(this.Parent.GuildSN));
				this.ItemTodayLogs.Clear();
				foreach (GetGuildStorageItemLogsToday getGuildStorageItemLogsToday in guildStorageItemLogsToday)
				{
					GuildStorageItemLogElement item2 = new GuildStorageItemLogElement
					{
						CharacterName = getGuildStorageItemLogsToday.CharacterName,
						IsAddItem = (getGuildStorageItemLogsToday.OperationType == 1),
						ItemClass = getGuildStorageItemLogsToday.ItemClass,
						Count = getGuildStorageItemLogsToday.Count,
						Datestamp = getGuildStorageItemLogsToday.Datestamp,
						Timestamp = getGuildStorageItemLogsToday.Timestamp,
						Color1 = getGuildStorageItemLogsToday.Color1,
						Color2 = getGuildStorageItemLogsToday.Color2,
						Color3 = getGuildStorageItemLogsToday.Color3
					};
					this.ItemTodayLogs.Add(item2);
				}
				if (reloadOldLog)
				{
					this.LastLogCrawl = DateTime.Now;
					heroesDataContext.RemoveOldGuildStorageLog(new int?(this.Parent.GuildSN));
					ISingleResult<GetGuildStorageBriefLogs> guildStorageBriefLogs = heroesDataContext.GetGuildStorageBriefLogs(new int?(this.Parent.GuildSN));
					this.BriefOldLogs.Clear();
					foreach (GetGuildStorageBriefLogs getGuildStorageBriefLogs in guildStorageBriefLogs)
					{
						GuildStorageBriefLogElement item3 = new GuildStorageBriefLogElement
						{
							CharacterName = getGuildStorageBriefLogs.CharacterName,
							OperationType = (GuildStorageOperationType)getGuildStorageBriefLogs.OperationType,
							AddCount = getGuildStorageBriefLogs.AddCount,
							PickCount = getGuildStorageBriefLogs.PickCount,
							Datestamp = getGuildStorageBriefLogs.Datestamp,
							Timestamp = getGuildStorageBriefLogs.Timestamp
						};
						this.BriefOldLogs.Add(item3);
					}
					ISingleResult<GetGuildStorageItemLogs> guildStorageItemLogs = heroesDataContext.GetGuildStorageItemLogs(new int?(this.Parent.GuildSN));
					this.ItemOldLogs.Clear();
					foreach (GetGuildStorageItemLogs getGuildStorageItemLogs in guildStorageItemLogs)
					{
						GuildStorageItemLogElement item4 = new GuildStorageItemLogElement
						{
							CharacterName = getGuildStorageItemLogs.CharacterName,
							IsAddItem = (getGuildStorageItemLogs.OperationType == 1),
							ItemClass = getGuildStorageItemLogs.ItemClass,
							Count = getGuildStorageItemLogs.Count,
							Datestamp = getGuildStorageItemLogs.Datestamp,
							Timestamp = getGuildStorageItemLogs.Timestamp,
							Color1 = getGuildStorageItemLogs.Color1,
							Color2 = getGuildStorageItemLogs.Color2,
							Color3 = getGuildStorageItemLogs.Color3
						};
						this.ItemOldLogs.Add(item4);
					}
					this.BroadCastStorageLog(false);
				}
			}
			this.BroadCastStorageLog(true);
		}

		public void BroadCastStorageLog(bool isTodayLog)
		{
			foreach (OnlineGuildMember onlineGuildMember in this.Parent.OnlineMembers.Values)
			{
				if (onlineGuildMember.IsGuildStorageListening)
				{
					onlineGuildMember.SendGuildStorageLogsMessage(isTodayLog);
				}
			}
		}

		public void ReportAddItem(string name, string itemclass, int amount, int color1, int color2, int color3)
		{
			try
			{
				using (HeroesDataContext heroesDataContext = new HeroesDataContext())
				{
					if (itemclass == "gold")
					{
						heroesDataContext.AddGuildStorageBriefLog(new int?(this.Parent.GuildSN), name, new byte?(1), new int?(amount), new int?(0));
					}
					else
					{
						heroesDataContext.AddGuildStorageBriefLog(new int?(this.Parent.GuildSN), name, new byte?(2), new int?(1), new int?(0));
						heroesDataContext.AddGuildStorageItemLog(new int?(this.Parent.GuildSN), name, new byte?(1), itemclass, new int?(amount), new int?(color1), new int?(color2), new int?(color3));
					}
				}
			}
			catch (Exception ex)
			{
				Log<GuildStorageManager>.Logger.Error(ex);
			}
			this.QueryStorageLog(false);
		}

		public void ReportPickItem(string name, string itemclass, int amount, int color1, int color2, int color3)
		{
			try
			{
				using (HeroesDataContext heroesDataContext = new HeroesDataContext())
				{
					if (itemclass == "gold")
					{
						heroesDataContext.AddGuildStorageBriefLog(new int?(this.Parent.GuildSN), name, new byte?(1), new int?(0), new int?(amount));
					}
					else
					{
						heroesDataContext.AddGuildStorageBriefLog(new int?(this.Parent.GuildSN), name, new byte?(2), new int?(0), new int?(1));
						heroesDataContext.AddGuildStorageItemLog(new int?(this.Parent.GuildSN), name, new byte?(0), itemclass, new int?(amount), new int?(color1), new int?(color2), new int?(color3));
					}
				}
			}
			catch (Exception ex)
			{
				Log<GuildStorageManager>.Logger.Error(ex);
			}
			this.QueryStorageLog(false);
		}

		public void ReportExtendSlot(string name, int amount)
		{
			try
			{
				using (HeroesDataContext heroesDataContext = new HeroesDataContext())
				{
					heroesDataContext.AddGuildStorageBriefLog(new int?(this.Parent.GuildSN), name, new byte?(3), new int?(1), new int?(0));
				}
			}
			catch (Exception ex)
			{
				Log<GuildStorageManager>.Logger.Error(ex);
			}
			this.QueryStorageLog(false);
		}

		public bool IsPickLimited(OnlineGuildMember member, string itemclass, int slotNo, int amount)
		{
			if (member.GuildMember.Rank > GuildMemberRank.Member)
			{
				member.SendOperationFailedDialog("GuildStorageFail_RankLimited");
				return true;
			}
			if (itemclass == "gold")
			{
				if (this.GoldHolding - amount < this.GoldPickLimit)
				{
					member.SendOperationFailedDialog("GuildStorageFail_GoldPickLimitBySetting");
					return true;
				}
				int integer = FeatureMatrix.GetInteger("GuildStorageGoldDailyLimit");
				if (integer == 0)
				{
					return false;
				}
				int num = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
				foreach (GuildStorageBriefLogElement guildStorageBriefLogElement in this.BriefTodayLogs)
				{
					if (guildStorageBriefLogElement.Datestamp == num && guildStorageBriefLogElement.CharacterName == member.CharacterName && guildStorageBriefLogElement.OperationType == GuildStorageOperationType.Gold)
					{
						if (guildStorageBriefLogElement.PickCount + amount - guildStorageBriefLogElement.AddCount > integer)
						{
							member.SendOperationFailedDialog("GuildStorageFail_GoldPickDailyLimit");
							return true;
						}
						return false;
					}
				}
				if (amount > integer)
				{
					member.SendOperationFailedDialog("GuildStorageFail_GoldPickDailyLimit");
					return true;
				}
				return false;
			}
			else
			{
				int num2 = slotNo / this.GuildStorageSlotsPerTab;
				int num3 = (int)(this.AccessLimitTag >> num2 * this.GuildStorageSettingFlagBitsPerTab & (long)this.GuildStorageSettingMask);
				if (num3 != 0 && member.GuildMember.Rank > (GuildMemberRank)num3)
				{
					member.SendOperationFailedDialog("GuildStorageFail_RankLimited");
					return true;
				}
				return false;
			}
		}

		public bool IsAddLimited(OnlineGuildMember member, string itemclass, int slotNo, int amount, int targetTab)
		{
			if (member.GuildMember.Rank > GuildMemberRank.Member)
			{
				member.SendOperationFailedDialog("GuildStorageFail_RankLimited");
				return true;
			}
			if (itemclass == "gold")
			{
				int num = FeatureMatrix.GetInteger("GuildStorageGoldHoldingLimit");
				if (num == 0)
				{
					num = int.MaxValue;
				}
				if (this.GoldHolding > num - amount)
				{
					member.SendOperationFailedDialog("GuildStorageFail_GoldAddLimit");
					return true;
				}
				return false;
			}
			else
			{
				int num2 = (int)(this.AccessLimitTag >> targetTab * this.GuildStorageSettingFlagBitsPerTab & (long)this.GuildStorageSettingMask);
				if (num2 != 0 && member.GuildMember.Rank > (GuildMemberRank)num2)
				{
					member.SendOperationFailedDialog("GuildStorageFail_RankLimited");
					return true;
				}
				return false;
			}
		}

		public void UpdateSetting(int goldLimit, long accessTag)
		{
			try
			{
				using (HeroesDataContext heroesDataContext = new HeroesDataContext())
				{
					heroesDataContext.SetGuildStorageSetting(new int?(this.Parent.GuildSN), new int?(goldLimit), new long?(accessTag));
					this.GoldPickLimit = goldLimit;
					this.AccessLimitTag = accessTag;
				}
			}
			catch (Exception ex)
			{
				Log<GuildStorageManager>.Logger.Error(ex);
			}
		}

		public void ApplyStorage(ICollection<SlotInfo> inventory)
		{
			this.SlotInfo = inventory;
			this.GoldHolding = 0;
			foreach (SlotInfo slotInfo in inventory)
			{
				if (slotInfo.ItemClass == "gold")
				{
					this.GoldHolding = slotInfo.Num;
				}
			}
			if (this.SlotInfo == null)
			{
				this.AddGuildStorageLedger(-1L, GuildStorageOperationCode.Report, GuildStorageEventCode.GoldCount_Login, "Gold", this.GoldHolding);
				this.AddGuildStorageLedger(-1L, GuildStorageOperationCode.Report, GuildStorageEventCode.ItemCount_Login, "Item", this.SlotInfo.Count);
			}
		}

		public void AddGuildStorageLedger(long CID, GuildStorageOperationCode opCode, GuildStorageEventCode eventCode)
		{
			this.AddGuildStorageLedger(CID, opCode, eventCode, "", 0, -1, -1, -1, -1, -1);
		}

		public void AddGuildStorageLedger(long CID, GuildStorageOperationCode opCode, GuildStorageEventCode eventCode, string itemClassEX, int amount)
		{
			this.AddGuildStorageLedger(CID, opCode, eventCode, itemClassEX, amount, -1, -1, -1, -1, -1);
		}

		public void AddGuildStorageLedger(long CID, GuildStorageOperationCode opCode, GuildStorageEventCode eventCode, string itemClassEX, int amount, int color1, int color2, int color3, int reduceDurability, int maxDurabilityBonus)
		{
			try
			{
				HeroesLogDataContext heroesLogDataContext = new HeroesLogDataContext();
				heroesLogDataContext.AddGuildStorageLedger(new long?((long)this.Parent.GuildSN), new long?(CID), new short?((short)((byte)opCode)), new short?((short)((byte)eventCode)), itemClassEX, new int?(amount), new int?(color1), new int?(color2), new int?(color3), new int?(reduceDurability), new int?(maxDurabilityBonus));
			}
			catch (Exception ex)
			{
				Log<GuildStorageManager>.Logger.Error(ex);
			}
		}

		public readonly int GuildStorageSlotsFirstPurchase;

		public readonly int GuildStorageSlotsPerPurchase;

		public readonly int GuildStorageSlotsPerTab;

		public readonly int GuildStorageSlotsMax;

		public readonly int GuildStorageSettingFlagBitsPerTab;

		public readonly int GuildStorageSettingMask;
	}
}
