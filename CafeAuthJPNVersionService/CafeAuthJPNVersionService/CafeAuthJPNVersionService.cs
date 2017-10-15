using System;
using System.Collections.Generic;
using System.Net;
using CafeAuthJPNVersionServiceCore.Processor;
using Devcat.Core;
using Devcat.Core.Threading;
using Nexon.CafeAuthJPN;
using Nexon.CafeAuthJPN.Packets;
using ServiceCore;
using ServiceCore.CafeAuthServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CafeAuthJPNVersionServiceCore
{
	public class CafeAuthJPNVersionService : Service
	{
		internal bool Valid
		{
			get
			{
				return ServiceCoreSettings.Default.CafeAuthDomainSN != byte.MaxValue;
			}
		}

		internal bool Running
		{
			get
			{
				return this.connection.Connected;
			}
		}

		private CafeAuthJPNVersionService()
		{
			if (FeatureMatrix.IsEnable("jaJP"))
			{
				this.connection.SetGameSN(GameSN.HeroesJPN);
			}
			this.connection.ConnectionFail += this.connection_ConnectionFail;
			this.connection.Disconnected += this.connection_Disconnected;
			this.connection.ExceptionOccur += this.connection_ExceptionOccur;
			this.connection.MessageReceived += this.connection_MessageReceived;
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

		protected override IEntity MakeEntity(long id, string category)
		{
			IEntity entity = base.MakeEntity(id, category);
			entity.Tag = new CafeAuth(this, entity);
			entity.Using += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				IEntityAdapter value = e.Value;
				if (entity.Tag == null)
				{
					return;
				}
				if (value.RemoteCategory == "FrontendServiceCore.FrontendService")
				{
					CafeAuth cafeAuth = entity.Tag as CafeAuth;
					if (cafeAuth.FrontendConn != null)
					{
						cafeAuth.FrontendConn.Close();
					}
					cafeAuth.FrontendConn = this.Connect(entity, new Location(value.RemoteID, value.RemoteCategory));
				}
			};
			entity.Used += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				IEntityAdapter value = e.Value;
				if (value.RemoteCategory == "FrontendServiceCore.FrontendService" || entity.UseCount == 0)
				{
					entity.Close();
					Scheduler.Schedule(this.Thread, Job.Create(delegate
					{
						if (!entity.IsClosed)
						{
							entity.Close(true);
						}
					}), new TimeSpan(0, 0, 30));
				}
			};
			return entity;
		}

		public override void Initialize(JobProcessor thread)
		{
			base.Initialize(thread, MessageID.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterProcessor(typeof(Login), (Operation op) => new LoginProcessor(this, op as Login));
			if (!this.Valid)
			{
				Log<CafeAuthJPNVersionService>.Logger.Warn("CafeAuth service is not initialized");
				return;
			}
			this.connectToCafeAuthServer();
		}

		private void connectToCafeAuthServer()
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(ServiceCoreSettings.Default.CafeAuthAddress);
			int num = 0;
			if (num >= hostAddresses.Length)
			{
				return;
			}
			IPAddress address = hostAddresses[num];
			IPEndPoint ipendPoint = new IPEndPoint(address, (int)ServiceCoreSettings.Default.CafeAuthPort);
			Log<CafeAuthJPNVersionService>.Logger.InfoFormat("connect to CafeAuthService [{0} {1} {2}]", ipendPoint, ServiceCoreSettings.Default.CafeAuthDomainSN, ServiceCoreSettings.Default.CafeAuthDomainString);
			this.connection.Connect(base.Thread, ipendPoint, ServiceCoreSettings.Default.CafeAuthDomainSN, ServiceCoreSettings.Default.CafeAuthDomainString);
		}

		private void connection_ConnectionFail(object sender, EventArgs<Exception> e)
		{
			Log<CafeAuthJPNVersionService>.Logger.Fatal("connection failed CafeAuth service, and continued", e.Value);
		}

		private void connection_Disconnected(object sender, EventArgs e)
		{
			Log<CafeAuthJPNVersionService>.Logger.Fatal("Disconnected from CafeAuth service");
			Scheduler.Schedule(base.Thread, Job.Create(new Action(this.connectToCafeAuthServer)), 60000);
		}

		private void connection_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			Log<CafeAuthJPNVersionService>.Logger.Fatal("Exception occured in CafeAuth service", e.Value);
		}

        private void connection_MessageReceived(LoginResponse Response)
        {
            SystemMessage serializeObject = (SystemMessage)null;
            bool flag = false;
            switch (Response.Option)
            {
                case Option.AddressExpired:
                case Option.PrepaidExpired:
                case Option.PrepaidExhausted:
                    serializeObject = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_Expired");
                    flag = true;
                    break;
                case Option.WelcomePrepaid:
                    if (Response.Argument > 60)
                    {
                        serializeObject = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_TimeLeft", new object[1]
                        {
              (object) (Response.Argument / 60)
                        });
                        break;
                    }
                    serializeObject = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_TimeLeft_LessThanOneHour");
                    break;
            }
            if (serializeObject == null)
                return;
            Log<CafeAuthJPNVersionService>.Logger.InfoFormat("Response from CafeAuth service: {0}", (object)Response);
            CafeAuth cafeAuth;

            if (!this.NxIDToEntityDic.TryGetValue(Response.NexonID, out cafeAuth))
            {
                return;
            }

            SendPacket sendPacket = SendPacket.Create<SystemMessage>(serializeObject);
            cafeAuth.FrontendConn.RequestOperation((Operation)sendPacket);
            if (!flag)
            {
                return;
            }

            Scheduler.Schedule(this.Thread, Job.Create<CafeAuth>(new Action<CafeAuth>(this.Kick_User), cafeAuth), 5000);
        }


        public AsyncResultSync BeginLogin(string nexonID, string characterID, IPAddress loginAddress, IPAddress remoteAddress, bool canTry, bool isTrial, MachineID machineID, int gameRoomClient, object state)
		{
			if (!this.Valid)
			{
				return null;
			}
			AsyncResultSync asyncResultSync = new AsyncResultSync(base.Thread);
			this.connection.BeginLogin(nexonID, characterID, loginAddress, remoteAddress, canTry, isTrial, machineID, (gameRoomClient == 0) ? null : new int[]
			{
				gameRoomClient
			}, new AsyncCallback(asyncResultSync.AsyncCallback), state);
			return asyncResultSync;
		}

		public CafeAuthResult EndLogin(IAsyncResult asyncResult)
		{
			LoginResponse loginResponse = this.connection.EndLogin(asyncResult);
			if (loginResponse == null)
			{
				Log<CafeAuthJPNVersionService>.Logger.Warn("Failed connect to CafeAuth service. duplicated connection");
				return null;
			}
			Log<CafeAuthJPNVersionService>.Logger.InfoFormat("Responsed from CafeAuth service: {0}", loginResponse);
			if (loginResponse.Option == Option.NoOption && (loginResponse.Result == Result.Allowed || loginResponse.Result == Result.Terminate))
			{
				Log<CafeAuthJPNVersionService>.Logger.InfoFormat("Invalid respose from CafeAuth service: {0}", loginResponse);
			}
			return new CafeAuthResult
			{
				Result = loginResponse.Result,
				Option = loginResponse.Option,
				CafeNo = loginResponse.CafeNo
			};
		}

		public void Logout(string nexonID, string characterID, IPAddress remoteAddress, bool canTry)
		{
			if (this.Valid)
			{
				this.connection.Logout(nexonID, characterID, remoteAddress, canTry);
				Log<CafeAuthJPNVersionService>.Logger.DebugFormat("Logout from CafeAuth service: {0} {1}", characterID, remoteAddress);
			}
		}

		private void Kick_User(CafeAuth target)
		{
			target.FrontendConn.RequestOperation(new DisconnectClient());
		}

		public static Service StartService(string ip, string portstr)
		{
			CafeAuthJPNVersionService cafeAuthJPNVersionService = new CafeAuthJPNVersionService();
			ServiceInvoker.StartService(ip, portstr, cafeAuthJPNVersionService);
			return cafeAuthJPNVersionService;
		}

		private Connection connection = new Connection(GameSN.Heroes);

		internal Dictionary<string, CafeAuth> NxIDToEntityDic = new Dictionary<string, CafeAuth>();
	}
}
