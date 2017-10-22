using System;
using Devcat.Core;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.Configuration;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.TalkServiceOperations;
using TalkService.Processor;
using TalkService.Properties;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;
using WcfChatRelay.Server.Whisper;
using WcfChatRelay.Whisper;

namespace TalkService
{
	public class TalkService : Service
	{
		public int FindServiceID(long id, string category)
		{
			if (category != base.Category)
			{
				return -1;
			}
			int result;
			using (EntityDataContext entityDataContext = new EntityDataContext())
			{
				result = entityDataContext.AcquireService(new long?(id), base.Category, new int?(-1), new int?(-1));
			}
			return result;
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

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			base.Initialize(thread, MessageID.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterProcessor(typeof(Whisper), (Operation op) => new WhisperProcessor(this, op as Whisper));
			base.RegisterProcessor(typeof(WhisperToGameClient), (Operation op) => new WhisperToGameClientProcessor(this, op as WhisperToGameClient));
			this.InitializeRelayClient(ServiceCoreSettings.Default.TalkWcfService, string.Format("{0}:{1}", base.Category, Service.ServerCode));
		}

		protected override IEntity MakeEntity(long id, string category)
		{
			IEntity entity = base.MakeEntity(id, category);
			entity.Tag = new TalkClient(this, entity);
			entity.Using += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				IEntityAdapter value = e.Value;
				if (entity.Tag == null)
				{
					return;
				}
				if (value.RemoteCategory == "FrontendServiceCore.FrontendService")
				{
					TalkClient talkClient = entity.Tag as TalkClient;
					if (talkClient.FrontendConn != null)
					{
						talkClient.FrontendConn.Close();
					}
					talkClient.FrontendConn = this.Connect(entity, new Location(value.RemoteID, value.RemoteCategory));
					talkClient.FrontendConn.Closed += delegate(object _, EventArgs<IEntityProxy> __)
					{
						entity.Close();
					};
					talkClient.FrontendConn.OperationQueueOversized += delegate(object _, EventArgs<IEntityProxy> __)
					{
						entity.Close(true);
					};
				}
			};
			entity.Closed += delegate(object sender, EventArgs e)
			{
				try
				{
					EntityDataContext entityDataContext = new EntityDataContext();
					entityDataContext.AcquireService(new long?((sender as IEntity).ID), base.Category, new int?(-1), new int?(base.ID));
				}
				catch (Exception ex)
				{
					Log<TalkService>.Logger.ErrorFormat("Entity_Closed [EntityID : {0}] [ServiceID : {1}] [Category : {2}] - {3} ", new object[]
					{
						(sender as IEntity).ID,
						base.ID,
						base.Category,
						ex
					});
				}
			};
			entity.Used += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				if (entity.UseCount == 0)
				{
					entity.Close();
				}
			};
			return entity;
		}

		public static Service StartService(string ip, string portstr)
		{
			TalkService talkService = new TalkService();
			ServiceInvoker.StartService(ip, portstr, talkService);
			return talkService;
		}

		internal RelayClient RelayClient
		{
			get
			{
				if (!this.relayInitialized)
				{
					return null;
				}
				return this.relayClient;
			}
		}

		public void InitializeRelayClient(string url, string name)
		{
			this.relayClient = new RelayClient(url, name);
			this.relayClient.Logger = Log<RelayClient>.Logger;
			this.relayClient.Disconnected += delegate(object s, EventArgs e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<RelayClient>(new Action<RelayClient>(this.RelayClientDisconnected), this.RelayClient), 0);
			};
			this.relayClient.WebClosed += delegate(object s, EventArgs e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<RelayClient>(new Action<RelayClient>(this.WebClosed), this.RelayClient), 0);
				Log<TalkService>.Logger.Fatal("Web Chat is Down.");
			};
			this.relayClient.Whispered += delegate(object s, WhisperEventArg e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<string, long, string>(new Action<string, long, string>(this.WhisperFromApp), e.From, e.ToCID, e.Message), 0);
			};
			this.relayClient.WhisperedAsync += delegate(object s, WhisperAsyncEventArg e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<RelayClient, WhisperAsyncEventArg>(new Action<RelayClient, WhisperAsyncEventArg>(this.WhisperFromAppAsync), (RelayClient)s, e), 0);
			};
			this.relayClient.WhisperResultAsync += delegate(object s, WhisperResultAsyncEventArg e)
			{
				Scheduler.Schedule(base.Thread, Job.Create<RelayClient, WhisperResultAsyncEventArg>(new Action<RelayClient, WhisperResultAsyncEventArg>(this.WhisperResultFromAppAsync), (RelayClient)s, e), 0);
			};
			if (!this.ConnectToRelayServer())
			{
				base.BootFail();
			}
			Log<TalkService>.Logger.Info("Connected to Talk Relay Server.");
		}

		private void ReConnectToRelayServer()
		{
			this.ConnectToRelayServer();
		}

		private bool ConnectToRelayServer()
		{
			if (this.relayClient == null || !this.relayClient.ConnectToService())
			{
				Log<TalkService>.Logger.Fatal("Fail to Connect Talk Relay Server.");
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.ReConnectToRelayServer)), 30000);
				return false;
			}
			this.relayInitialized = true;
			this.PingToRelay();
			Log<TalkService>.Logger.Error("Connected to Talk Relay Server.");
			return true;
		}

		private void PingToRelay()
		{
			if (this.RelayClient != null)
			{
				this.RelayClient.Ping();
				this.relaySchedule = Scheduler.Schedule(base.Thread, Job.Create(new Action(this.PingToRelay)), 60000);
			}
		}

		private void RelayClientDisconnected(RelayClient client)
		{
			if (!client.GracefullyClosed)
			{
				this.relayInitialized = false;
				Scheduler.Cancel(this.relaySchedule);
				Log<TalkService>.Logger.Fatal("Talk Relay Server Disconnected.");
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.ReConnectToRelayServer)), 30000);
				return;
			}
			Log<TalkService>.Logger.Info("Disconnected from Web Chat Relay Server.");
		}

		private void WebClosed(RelayClient client)
		{
		}

		private void SendAsyncResult(TalkService.WhisperAnyncArguments args)
		{
			if (args != null)
			{
				IEntity entityByID = base.GetEntityByID(args.FromCid);
				if (entityByID != null && entityByID.Tag != null)
				{
					TalkClient talkClient = entityByID.Tag as TalkClient;
					if (talkClient.FrontendConn != null)
					{
						SendPacket op = SendPacket.Create<WhisperFailMessage>(new WhisperFailMessage(args.ToName, "GameUI_Heroes_Chat_Whisper_Not_Online"));
						talkClient.FrontendConn.RequestOperation(op);
					}
				}
			}
		}

		private void WhisperAsyncCallback(IAsyncResult ar)
		{
			if (this.RelayClient != null && !this.RelayClient.EndWhisper(ar))
			{
				Scheduler.Schedule(base.Thread, Job.Create<TalkService.WhisperAnyncArguments>(new Action<TalkService.WhisperAnyncArguments>(this.SendAsyncResult), (TalkService.WhisperAnyncArguments)ar.AsyncState), 0);
			}
		}

		public Whisper.WhisperResult WhisperToAll(string from, long fromCID, string to, string message)
		{
			long? cid = this.GetCID(to);
			if (cid == null)
			{
				return Whisper.WhisperResult.NoCharacter;
			}
			Whisper.WhisperResult result;
			try
			{
				if (this.WhisperToGameClient(from, cid.Value, message))
				{
					result = Whisper.WhisperResult.Success;
				}
				else if (this.RelayClient != null)
				{
					this.RelayClient.BeginWhisper(from, fromCID, to, message, new AsyncCallback(this.WhisperAsyncCallback), new TalkService.WhisperAnyncArguments(to, fromCID));
					result = Whisper.WhisperResult.Wait;
				}
				else
				{
					result = Whisper.WhisperResult.OffLine;
				}
			}
			catch (Exception ex)
			{
				Log<TalkService>.Logger.Error("WhisperToAll() occured exception", ex);
				result = Whisper.WhisperResult.OffLine;
			}
			return result;
		}

		public Whisper.WhisperResult GetWhisperResult(ISynchronizable syncObj)
		{
			AsyncResultSync asyncResultSync = syncObj as AsyncResultSync;
			if (asyncResultSync == null || this.RelayClient == null)
			{
				Log<TalkService>.Logger.Error("RelayClient is not connected");
				return Whisper.WhisperResult.LogicalFail;
			}
			if (!this.RelayClient.EndWhisper(asyncResultSync.AsyncResult))
			{
				return Whisper.WhisperResult.OffLine;
			}
			return Whisper.WhisperResult.Success;
		}

		private void WhisperResultFromAppAsync(RelayClient client, WhisperResultAsyncEventArg arg)
		{
			bool flag = this.WhipserResultToGameClient(arg.ToCID, arg.ResultNo, arg.ReceiverName);
			Log<TalkService>.Logger.InfoFormat("WhisperResultFromAppAsync is called. [returnValue: {0}]", flag);
			arg.Callback(flag, arg.AsyncResult);
		}

		private void WhisperFromAppAsync(RelayClient client, WhisperAsyncEventArg arg)
		{
			arg.Callback(this.WhisperToGameClient(arg.From, arg.ToCID, arg.Message), arg.AsyncResult);
		}

		private void WhisperFromApp(string from, long cid, string message)
		{
			this.WhisperToGameClient(from, cid, message);
		}

		public bool WhisperToGameClient(string from, long cid, string message)
		{
			IEntity entityByID = base.GetEntityByID(cid);
			if (entityByID == null)
			{
				if (base.LookUp.GetLocationCount(base.Category) > 1)
				{
					int num = this.FindServiceID(cid, base.Category);
					if (num > 0 && base.ID != num)
					{
						base.RequestOperation(num, new WhisperToGameClient(from, cid, message));
						return true;
					}
				}
				return false;
			}
			TalkClient talkClient = entityByID.Tag as TalkClient;
			if (talkClient != null && talkClient.FrontendConn != null)
			{
				talkClient.FrontendConn.RequestOperation(SendPacket.Create<WhisperMessage>(new WhisperMessage
				{
					Sender = from,
					Contents = message
				}));
				return true;
			}
			return false;
		}

		public bool WhipserResultToGameClient(long cid, int resultNo, string receiverName)
		{
			Log<TalkService>.Logger.InfoFormat("WhipserResultToGameClient is called. [ cid:{0}, result:{1}, receiver:{2} ]", cid, resultNo, receiverName);
			IEntity entityByID = base.GetEntityByID(cid);
			if (entityByID == null)
			{
				if (base.LookUp.GetLocationCount(base.Category) > 1)
				{
					int num = this.FindServiceID(cid, base.Category);
					if (num > 0 && base.ID != num)
					{
						base.RequestOperation(num, new WhisperResultToGameClient(cid, resultNo, receiverName));
						return true;
					}
				}
				Log<TalkService>.Logger.WarnFormat("WhipserResultToGameClient the end of the function", new object[0]);
				return false;
			}
			TalkClient talkClient = entityByID.Tag as TalkClient;
			if (talkClient != null && talkClient.FrontendConn != null)
			{
				if (resultNo == 11)
				{
					talkClient.FrontendConn.RequestOperation(SendPacket.Create<WhisperFailMessage>(new WhisperFailMessage(receiverName, "GameUI_Heroes_Chat_Whisper_Not_Online")));
				}
				else
				{
					Log<TalkService>.Logger.ErrorFormat("WhipserResultToGameClient Unknown ResultNo : {0}", resultNo);
				}
				return true;
			}
			Log<TalkService>.Logger.WarnFormat("WhipserResultToGameClient TalkClient is null.", new object[0]);
			return false;
		}

		private long? GetCID(string name)
		{
			if (this.NameCache.ContainsKey(name))
			{
				return new long?(this.NameCache[name]);
			}
			long? result = null;
			try
			{
				using (HeroesDataContext heroesDataContext = new HeroesDataContext())
				{
					heroesDataContext.GetCIDByName(name, ref result);
				}
			}
			catch (Exception ex)
			{
				Log<TalkService>.Logger.Error("Error while GetCIDByName : ", ex);
				result = null;
			}
			if (result != null)
			{
				this.NameCache.Add(name, result.Value);
			}
			return result;
		}

		private const int NameCacheSize = 40000;

		private const int RelayReconnectPeriod = 30000;

		private const int RelayPingPeriod = 60000;

		private LRUCache<string, long> NameCache = new LRUCache<string, long>(40000);

		private bool relayInitialized;

		private RelayClient relayClient;

		private long relaySchedule;

		public enum MessageType
		{
			GameSend = 1,
			AppSend,
			OffLine = 11,
			Read
		}

		private class WhisperAnyncArguments
		{
			public WhisperAnyncArguments(string toName, long fromCid)
			{
				this.ToName = toName;
				this.FromCid = fromCid;
			}

			public string ToName;

			public long FromCid;
		}
	}
}
