using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Devcat.Core;
using Devcat.Core.Threading;
using GuildService.Properties;
using Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.Korea;
using ServiceCore;
using ServiceCore.Configuration;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Entity;
using Utility;
using WcfChatRelay.Server.GuildChat;

namespace GuildService
{
	public class GuildService : Service
	{
		public bool GuildChatToGameClient(long gid, long cid, string senderName, string message, bool isFromClient)
		{
			IEntity entityByID = base.GetEntityByID(gid);
			if (entityByID != null)
			{
				GuildEntity guildEntity = entityByID.Tag as GuildEntity;
				if (guildEntity != null && guildEntity.ChatRoom != null)
				{
					guildEntity.ChatRoom.OnReceiveChatMessage(cid, senderName, message, isFromClient);
					return true;
				}
			}
			return false;
		}

		internal HeroesGuildChatRelay ChatRelay
		{
			get
			{
				if (!this.chatRelayInitialized)
				{
					return null;
				}
				return this.chatRelay;
			}
		}

		private string MakeServiceName()
		{
			return this.MakeServiceName(base.ID);
		}

		private string MakeServiceName(int serviceId)
		{
			return string.Format("{0}:{1}:{2}", base.Category, serviceId, Service.ServerCode);
		}

		private void InitializeGuildWebChat(int serviceId)
		{
			this.chatRelay = new HeroesGuildChatRelay(ServiceCoreSettings.Default.GuildChatWcfService, this.MakeServiceName(serviceId));
			this.chatRelay.Logger = Log<HeroesGuildChatRelay>.Logger;
			this.chatRelay.Disconnected += delegate(object s, EventArgs e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<HeroesGuildChatRelay>(new Action<HeroesGuildChatRelay>(this.RelayServerDisconnected), this.chatRelay), 0);
			};
			this.chatRelay.WebClosed += delegate(object s, EventArgs e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<HeroesGuildChatRelay>(new Action<HeroesGuildChatRelay>(this.WebClosed), this.chatRelay), 0);
				Log<GuildService>.Logger.Fatal("Web Chat is Down.");
			};
			this.chatRelay.MembersSync += delegate(object s, ChatMemberSyncEventArg e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<HeroesGuildChatRelay, ChatMemberSyncEventArg>(new Action<HeroesGuildChatRelay, ChatMemberSyncEventArg>(this.SyncWebMember), this.chatRelay, e), 0);
			};
			this.chatRelay.JoinRequested += delegate(object s, ChatJoinEventArg e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<HeroesGuildChatRelay, ChatJoinEventArg>(new Action<HeroesGuildChatRelay, ChatJoinEventArg>(this.JoinWebChatMember), this.chatRelay, e), 0);
			};
			this.chatRelay.ChatReceived += delegate(object s, ChatEventArg e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<HeroesGuildChatRelay, ChatEventArg>(new Action<HeroesGuildChatRelay, ChatEventArg>(this.WebChat), this.chatRelay, e), 0);
			};
			this.chatRelay.LeaveRequested += delegate(object s, ChatOpEventArg e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<HeroesGuildChatRelay, long>(new Action<HeroesGuildChatRelay, long>(this.LeaveWebChatMember), this.chatRelay, e.CID), 0);
			};
			if (!this.ConnectToRelayServer())
			{
				base.BootFail();
			}
			Log<GuildService>.Logger.Info("Connected to Web Chat Relay Server.");
		}

		private void ReConnectToRelayServer()
		{
			if (this.ConnectToRelayServer())
			{
				if (base.Entities == null || base.Entities.Count<IEntity>() <= 0)
				{
					return;
				}
				foreach (IEntity entity in base.Entities)
				{
					if (entity != null && entity.Tag != null && entity.Tag is GuildEntity)
					{
						GuildEntity guildEntity = entity.Tag as GuildEntity;
						if (guildEntity != null && guildEntity.ChatRoom != null)
						{
							this.ChatRelay.SendMemberInfo(guildEntity.ChatRoom.GuildID, this.GetGameMembers(guildEntity.ChatRoom.GuildID), true);
							this.ChatRelay.RequestMemberInfos(guildEntity.GuildID);
						}
					}
					else
					{
						Log<GuildService>.Logger.ErrorFormat("Detect GuildEntity is null in ReconnectToRelayServer()", new object[0]);
					}
				}
			}
		}

		public bool ConnectToRelayServer()
		{
			if (this.chatRelay == null || !this.chatRelay.ConnectToService())
			{
				Log<GuildService>.Logger.Fatal("Fail to Connect Web Chat Relay Server.");
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.ReConnectToRelayServer)), 30000);
				return false;
			}
			this.chatRelayInitialized = true;
			this.PingToChatRelay();
			Log<GuildService>.Logger.Error("Connected to Web Chat Relay Server.");
			return true;
		}

		private void PingToChatRelay()
		{
			if (this.ChatRelay != null)
			{
				this.ChatRelay.Ping();
				this.chatRelaySchedule = Scheduler.Schedule(base.Thread, Job.Create(new Action(this.PingToChatRelay)), 60000);
			}
		}

		private void RelayServerDisconnected(HeroesGuildChatRelay server)
		{
			if (!this.chatRelay.GracefullyClosed)
			{
				Scheduler.Cancel(this.chatRelaySchedule);
				this.LeaveAllGuildChatWebMember(this.ChatRelay);
				this.chatRelayInitialized = false;
				Log<GuildService>.Logger.Fatal("Web Chat Relay Server Disconnected.");
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.ReConnectToRelayServer)), 30000);
				return;
			}
			Log<GuildService>.Logger.Info("Disconnected from Web Chat Relay Server.");
		}

		private void WebClosed(HeroesGuildChatRelay server)
		{
			this.LeaveAllGuildChatWebMember(server);
		}

		public void GuildChat(IGuildChatMember member, string message)
		{
			if (this.GuildChatToGameClient(member.GuildID, member.CID, member.Sender, message, true))
			{
				return;
			}
			Log<GuildService>.Logger.ErrorFormat("GuildChatToGameClient is failed in GuildChat() [{0}, {1}]", member, message);
		}

		private void WebChat(HeroesGuildChatRelay server, ChatEventArg arg)
		{
			GuildChatWebMember guildChatWebMember = server[arg.CID];
			if (guildChatWebMember != null)
			{
				IEntity entityByID = base.GetEntityByID(guildChatWebMember.GuildID);
				if (entityByID != null)
				{
					GuildEntity guildEntity = entityByID.Tag as GuildEntity;
					if (guildEntity != null && guildEntity.ChatRoom != null)
					{
						guildEntity.ChatRoom.OnReceiveChatMessage(guildChatWebMember.CID, guildChatWebMember.Sender, arg.Message, false);
					}
				}
			}
		}

		private bool DoSyncWebMember(GuildEntity entity, HeroesGuildChatRelay server, ChatMemberSyncEventArg arg)
		{
			if (entity != null && entity.ChatRoom != null)
			{
				GuildChatRoom chatRoom = entity.ChatRoom;
				chatRoom.LeaveAllMembers(entity.GuildID);
				foreach (KeyValuePair<long, string> keyValuePair in arg.Members)
				{
					Log<GuildService>.Logger.InfoFormat("DoSyncWebMember is called. [ {0}, {1}, {2} ]", arg.GuildKey, keyValuePair.Key, keyValuePair.Value);
					GuildChatWebMember guildChatWebMember = new GuildChatWebMember(keyValuePair.Key, arg.GuildKey, keyValuePair.Value, server);
					if (!chatRoom.JoinWebMember(guildChatWebMember))
					{
						this.ChatRelay.KickMember(guildChatWebMember.GuildID, guildChatWebMember.CID);
					}
					else
					{
						server.JoinWebMember(guildChatWebMember);
					}
				}
				return true;
			}
			return false;
		}

		public void SyncWebMember(HeroesGuildChatRelay server, ChatMemberSyncEventArg arg)
		{
			Log<GuildService>.Logger.InfoFormat("SyncWebMember is called. [ {0} ]", arg.GuildKey);
			IEntity entityByID = base.GetEntityByID(arg.GuildKey);
			if (entityByID != null && entityByID.Tag is GuildEntity)
			{
				Log<GuildService>.Logger.InfoFormat("SyncWebMember is called. [ {0}, found gid ]", arg.GuildKey);
				GuildEntity guildEntity = entityByID.Tag as GuildEntity;
				if (this.DoSyncWebMember(guildEntity, server, arg))
				{
					return;
				}
				Log<GuildService>.Logger.ErrorFormat("DoSyncWebMember is failed [{0}, {1}, {2}]", guildEntity.ToString(), server.ToString(), arg.ToString());
			}
		}

		public void LeaveChatRoom(IGuildChatMember member)
		{
			IEntity entityByID = base.GetEntityByID(member.GuildID);
			if (entityByID != null && entityByID.Tag is GuildEntity)
			{
				GuildEntity guildEntity = entityByID.Tag as GuildEntity;
				if (guildEntity != null && guildEntity.ChatRoom != null)
				{
					Log<GuildService>.Logger.InfoFormat("LeaveChatRoom is called. [ {0}, {1}, {2} ]", member.GuildID, member.CID, member.Sender);
					guildEntity.ChatRoom.LeaveMember(member);
				}
			}
		}

		public void JoinWebChatMember(HeroesGuildChatRelay server, ChatJoinEventArg arg)
		{
			if (!FeatureMatrix.IsEnable("UseHeroesGuildChatServer"))
			{
				arg.Callback(null, arg.AsyncResult);
			}
			Log<GuildService>.Logger.InfoFormat("JoinWebChatMember is called. [ {0}, {1}, {2} ]", arg.GuildKey, arg.CID, arg.Sender);
			IEntity entityByID = base.GetEntityByID(arg.GuildKey);
			if (entityByID != null && entityByID.Tag is GuildEntity)
			{
				Log<GuildService>.Logger.InfoFormat("JoinWebChatMember is called. [ {0}, {1}, {2}, found ]", arg.GuildKey, arg.CID, arg.Sender);
				GuildEntity entity = entityByID.Tag as GuildEntity;
				if (this.DoJoinWebChatMember(entity, server, arg))
				{
					arg.Callback(this.GetGameMembers(arg.GuildKey), arg.AsyncResult);
					return;
				}
				Log<GuildService>.Logger.WarnFormat("JoinWebChatMember(). DoJoinWebChatMember is failed [ {0}, {1}, {2} ]", arg.GuildKey, arg.CID, arg.Sender);
			}
			else
			{
				Log<GuildService>.Logger.InfoFormat("JoinWebChatMember(). cannot find guild entity [ {0}, {1}, {2} ]", arg.GuildKey, arg.CID, arg.Sender);
			}
			arg.Callback(null, arg.AsyncResult);
		}

		private bool DoJoinWebChatMember(GuildEntity entity, HeroesGuildChatRelay server, ChatJoinEventArg arg)
		{
			if (entity != null && entity.ChatRoom != null)
			{
				Log<GuildService>.Logger.InfoFormat("JoinWebChatMember is called. [ {0}, {1}, {2} ]", arg.GuildKey, arg.CID, arg.Sender);
				GuildChatWebMember member = new GuildChatWebMember(arg.CID, arg.GuildKey, arg.Sender, server);
				if (entity.ChatRoom.JoinWebMember(member))
				{
					string gameMembers = this.GetGameMembers(arg.GuildKey);
					Log<GuildService>.Logger.InfoFormat("JoinWebChatMember is called. [ {0}, {1}, {2}, {3} ]", new object[]
					{
						arg.GuildKey,
						arg.CID,
						arg.Sender,
						gameMembers
					});
					server.JoinWebMember(member);
					return true;
				}
			}
			return false;
		}

		public bool JoinChatRoomFromGame(GuildEntity entity, GuildMemberKey key)
		{
			Log<GuildService>.Logger.InfoFormat("JoinChatRoomFromGame is called", new object[0]);
			if (entity != null)
			{
				OnlineGuildMember onlineMember = entity.GetOnlineMember(key.CID);
				if (onlineMember != null)
				{
					Log<GuildService>.Logger.InfoFormat("JoinChatRoomFromGame onlineGuildMember is not null", new object[0]);
					GuildChatRoom chatRoom = entity.ChatRoom;
					return chatRoom.JoinGameMember(onlineMember);
				}
				Log<GuildService>.Logger.WarnFormat("JoinChatRoomFromGame onlineGuildMember is null", new object[0]);
			}
			return false;
		}

		private string GetGameMembers(long guildID)
		{
			string result = "";
			IEntity entityByID = base.GetEntityByID(guildID);
			if (entityByID != null && entityByID.Tag != null)
			{
				GuildEntity guildEntity = entityByID.Tag as GuildEntity;
				if (guildEntity != null)
				{
					result = string.Join(",", guildEntity.OnlineMembers.Keys.ToArray<string>());
				}
			}
			return result;
		}

		private void LeaveWebChatMember(HeroesGuildChatRelay server, long cid)
		{
			GuildChatWebMember guildChatWebMember = server[cid];
			if (guildChatWebMember != null)
			{
				this.LeaveChatRoom(guildChatWebMember);
				server.LeaveWebMember(cid);
			}
		}

		private void LeaveAllGuildChatWebMember(HeroesGuildChatRelay server)
		{
			Log<GuildService>.Logger.InfoFormat(" LeaveAllGuildChatWebMember is called.", new object[0]);
			foreach (GuildChatWebMember member in server.WebMembers)
			{
				this.LeaveChatRoom(member);
			}
			server.Clear();
		}

		public void KickFromChatRoom(GuildEntity entity, GuildMemberKey key)
		{
			if (this.ChatRelay != null)
			{
				GuildChatWebMember guildChatWebMember = this.ChatRelay[key.CID];
				if (guildChatWebMember != null)
				{
					Log<GuildService>.Logger.InfoFormat("KickFromChatRoom is called. [ {0} {1} {2} ]", guildChatWebMember.GuildID, guildChatWebMember.CID, guildChatWebMember.Sender);
					this.ChatRelay.KickMember(guildChatWebMember.GuildID, guildChatWebMember.CID);
					this.LeaveChatRoom(guildChatWebMember);
				}
			}
		}

		internal JobProcessor LogThread { get; set; }

		internal long DailyGPResetScheduleID { get; set; }

		internal List<long> OnlineGuildIDList { get; set; }

		internal List<long> NewbieRecommendGuild { get; private set; }

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			base.Initialize(thread, GuildServiceOperations.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterAllProcessors(Assembly.GetExecutingAssembly());
			if (FeatureMatrix.IsEnable("koKR") && !FeatureMatrix.IsEnable("NewbieGuildRecommend"))
			{
				try
				{
					GroupNMLinkSoapResult groupNMLinkSoapResult = new GroupNMLinkSoapWrapper(130076).Execute();
					if (groupNMLinkSoapResult.SoapErrorCode == 0)
					{
						Log<GuildService>.Logger.InfoFormat("길드 Refresh 성공", new object[0]);
					}
					else
					{
						Log<GuildService>.Logger.ErrorFormat("길드 Refresh 실패 : {0}", groupNMLinkSoapResult.SoapErrorCode);
					}
				}
				catch (Exception ex)
				{
					Log<GuildService>.Logger.Error("길드 Refresh 예외", ex);
				}
			}
			if (FeatureMatrix.IsEnable("GuildWebChat") && ServiceCoreSettings.Default.GuildChatWcfService.Length > 0)
			{
				base.OnSetupID += delegate(int id)
				{
					Scheduler.Schedule(base.Thread, Job.Create(delegate
					{
						this.InitializeGuildWebChat(id);
					}), 0);
				};
			}
			this.OnlineGuildIDList = new List<long>();
			DateTime d = GuildContents.GetPrevDailyGPResetTime() + TimeSpan.FromDays(1.0);
			this.DailyGPResetScheduleID = Scheduler.Schedule(thread, Job.Create(new Action(this.ResetOnlineGuildDailyGPScheduleFunc)), d - DateTime.UtcNow + TimeSpan.FromSeconds(30.0));
			this.NewbieRecommendGuild = new List<long>();
			this.LogThread = base.AcquireNewThread();
			this.LogThread.Start();
		}

		public override int CompareAndSwapServiceID(long id, string category, int beforeID)
		{
			if (category != base.Category)
			{
				return -1;
			}
			int result;
			using (EntityDataContext entityDataContext = new EntityDataContext())
			{
				result = entityDataContext.AcquireService(new long?(id), base.Category, new int?(base.ID), new int?(beforeID));
			}
			return result;
		}

		private void Entity_Closed(object sender, EventArgs e)
		{
			try
			{
				EntityDataContext entityDataContext = new EntityDataContext();
				entityDataContext.AcquireService(new long?((sender as IEntity).ID), base.Category, new int?(-1), new int?(base.ID));
			}
			catch (Exception ex)
			{
				Log<GuildService>.Logger.ErrorFormat("Entity_Closed [EntityID : {0}] [ServiceID : {1}] [Category : {2}] - {3} ", new object[]
				{
					(sender as IEntity).ID,
					base.ID,
					base.Category,
					ex
				});
			}
		}

		private void GuildEntity_UpdateNewbieRecommend(object sender, EventArgs arg)
		{
			GuildEntity guildEntity = sender as GuildEntity;
			if (guildEntity != null && guildEntity.GuildInfo != null)
			{
				if (guildEntity.GuildInfo.IsNewbieRecommend && guildEntity.OnlineMembers.Count != 0)
				{
					this.NewbieRecommendGuild.Add(guildEntity.GuildID);
					return;
				}
				this.NewbieRecommendGuild.Remove(guildEntity.GuildID);
			}
		}

        protected override IEntity MakeEntity(long id, string category)
        {
            IEntity entity = base.MakeEntity(id, category);
            GuildEntity guildEntity = new GuildEntity(this, entity, id);
            guildEntity.NewbieRecommendChanged += new EventHandler(this.GuildEntity_UpdateNewbieRecommend);
            entity.Tag = guildEntity;
            guildEntity.Entity = entity;
            entity.Closed += new EventHandler((object sender, EventArgs e) =>
            {
                this.OnlineGuildIDList.Remove(id);
                this.NewbieRecommendGuild.Remove(id);
                GuildEntity tag = entity.Tag as GuildEntity;
                if (tag != null)
                {
                    tag.Close();
                }
                Log<GuildService>.Logger.WarnFormat("<GID : {0}> guildEntity closed...", entity.ID);
            });
            entity.Closed += new EventHandler(this.Entity_Closed);
            entity.Used += new EventHandler<EventArgs<IEntityAdapter>>((object sender, EventArgs<IEntityAdapter> e) =>
            {
                if (entity.UseCount == 0)
                {
                    entity.Close();
                }
            });
            this.OnlineGuildIDList.Add(id);
            Log<GuildService>.Logger.WarnFormat("<GID : {0}> guildEntity makeEntity...", entity.ID);
            return entity;
        }

        public void ResetOnlineGuildDailyGPScheduleFunc()
		{
			HeroesDataContext heroesDataContext = new HeroesDataContext();
			List<long> list = new List<long>(this.OnlineGuildIDList);
			GuildGainGPMessage guildGainGPMessage = new GuildGainGPMessage(0L);
			foreach (long id in list)
			{
				IEntity entityByID = base.GetEntityByID(id);
				if (entityByID != null && !entityByID.IsClosed)
				{
					GuildEntity guildEntity = entityByID.Tag as GuildEntity;
					if (entityByID != null)
					{
						heroesDataContext.ResetInGameGuildDailyGainGP(guildEntity.GuildSN);
						guildEntity.GuildInfo.DailyGainGP.Clear();
						guildGainGPMessage.GuildPoint = guildEntity.GuildInfo.GuildPoint;
						foreach (OnlineGuildMember onlineGuildMember in guildEntity.OnlineMembers.Values)
						{
							onlineGuildMember.RequestFrontendOperation(SendPacket.Create<GuildGainGPMessage>(guildGainGPMessage));
						}
					}
				}
			}
			DateTime d = GuildContents.GetPrevDailyGPResetTime() + TimeSpan.FromDays(1.0);
			this.DailyGPResetScheduleID = Scheduler.Schedule(JobProcessor.Current, Job.Create(new Action(this.ResetOnlineGuildDailyGPScheduleFunc)), d - DateTime.UtcNow + TimeSpan.FromSeconds(30.0));
		}

		public static GuildService Instance { get; set; }

		public static Service StartService(string ip, string portstr)
		{
			GuildService.Instance = new GuildService();
			ServiceInvoker.StartService(ip, portstr, GuildService.Instance);
			GuildService.StartReporting(GuildService.Instance);
			return GuildService.Instance;
		}

		private static void StartReporting(GuildService serv)
		{
			if (!FeatureMatrix.IsEnable("ServiceReporter"))
			{
				return;
			}
			int num = ServiceReporterSettings.Get("GuildService.Interval", 60);
			ServiceReporter.Instance.Initialize("GuildService");
			ServiceReporter.Instance.AddGathering("Stat", new ServiceReporter.GatheringDelegate<int>(serv.OnGatheringStat));
			ServiceReporter.Instance.Start(num * 1000);
		}

		private void OnGatheringStat(Dictionary<string, int> dict)
		{
			dict.InsertOrIncrease("Entity", (int)this.GetEntityCount());
			dict.InsertOrIncrease("Queue", (int)this.GetQueueLength());
		}

		private const int ChatRelayReconnectPeriod = 30000;

		private const int ChatRelayPingPeriod = 60000;

		private bool chatRelayInitialized;

		private HeroesGuildChatRelay chatRelay;

		private long chatRelaySchedule;
	}
}
