using System;
using System.Collections.Generic;
using System.Net;
using CafeAuthServiceCore.Processor;
using CafeAuthServiceCore.Properties;
using Devcat.Core;
using Devcat.Core.Threading;
using Nexon.CafeAuth;
using Nexon.CafeAuth.Packets;
using ServiceCore;
using ServiceCore.CafeAuthServiceOperations;
using ServiceCore.Configuration;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CafeAuthServiceCore
{
	public class CafeAuthService : Service
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

		private CafeAuthService()
		{
			if (FeatureMatrix.IsEnable("jaJP"))
			{
				this.connection = new Connection(GameSN.HeroesJPN);
			}
			else if (FeatureMatrix.IsEnable("zhCN"))
			{
				this.connection = new Connection(GameSN.HeroesCHN);
			}
			else
			{
				this.connection = new Connection(GameSN.Heroes);
			}
			this.journalID = 0L;
			this.connection.ConnectionSucceeded += this.connection_ConnectionSucceeded;
			this.connection.ConnectionFail += this.connection_ConnectionFail;
			this.connection.Disconnected += this.connection_Disconnected;
			this.connection.ExceptionOccur += this.connection_ExceptionOccur;
			this.connection.MessageReceived += this.connection_MessageReceived;
			this.connection.InitializeSent += this.connection_InitializeSent;
			this.connection.InitializeResponsed += this.connection_InitializeResponsed;
			this.connection.TerminateRequested += this.connection_TerminateRequested;
			this.connection.SynchronizeRequested += this.connection_SynchronizeRequested;
			this.connection.LoginRecoveryRequested += this.connection_LoginRecoveryRequested;
			this.connection.RetryLoginRequested += this.connection_RetryLoginRequested;
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
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			base.Initialize(thread, MessageID.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterProcessor(typeof(Login), (Operation op) => new LoginProcessor(this, op as Login));
			this.cafeauthProcessing = true;
			if (!this.Valid)
			{
				Log<CafeAuthService>.Logger.Warn("CafeAuth service is in fake mode. (CafeAuthDomainSN==255)");
				return;
			}
			this.connectToCafeAuthServer();
		}

		private void connectToCafeAuthServer()
		{
			if (this.journalID != 0L)
			{
				CafeAuthService.AddCafeAuthServiceLog(this.journalID, "CloseJournal", "by reconnecting", "");
			}
			this.journalID = CafeAuthService.AddLoginJournal();
			if (this.journalID == -1L)
			{
				CafeAuthService.AddCafeAuthServiceLog(this.journalID, "OpenJournal", "failed", "");
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(ServiceCoreSettings.Default.CafeAuthAddress);
			int num = 0;
			if (num >= hostAddresses.Length)
			{
				return;
			}
			IPAddress address = hostAddresses[num];
			IPEndPoint ipendPoint = new IPEndPoint(address, (int)ServiceCoreSettings.Default.CafeAuthPort);
			Log<CafeAuthService>.Logger.WarnFormat("Connecting to Session server : [{0} {1} {2}]", ipendPoint, ServiceCoreSettings.Default.CafeAuthDomainSN, ServiceCoreSettings.Default.CafeAuthDomainString);
			CafeAuthService.AddCafeAuthServiceLog(this.journalID, "Connecting", ipendPoint.Address.ToString(), string.Format("Port:{0}/DomainSN:{1}", ipendPoint.Port, ServiceCoreSettings.Default.CafeAuthDomainSN));
			this.connection.Connect(base.Thread, ipendPoint, ServiceCoreSettings.Default.CafeAuthDomainSN, ServiceCoreSettings.Default.CafeAuthDomainString);
		}

		private void connection_ConnectionSucceeded(object sender, EventArgs e)
		{
			Log<CafeAuthService>.Logger.Warn("Successfully connected to Session server.");
			CafeAuthService.AddCafeAuthServiceLog(this.journalID, "Connected", this.connection.RemoteEndPoint.Address.ToString(), string.Format("Port:{0}/DomainSN:{1}", this.connection.RemoteEndPoint.Port, ServiceCoreSettings.Default.CafeAuthDomainSN));
			this.cafeauthProcessing = true;
		}

		private void connection_ConnectionFail(object sender, EventArgs<Exception> e)
		{
			Log<CafeAuthService>.Logger.Fatal(string.Format("Failed to connect to Session server. Retry after {0} sec...", 10), e.Value);
			CafeAuthService.AddCafeAuthServiceLog(this.journalID, "Not connected", this.connection.RemoteEndPoint.Address.ToString(), string.Format("Port:{0}/DomainSN:{1}", this.connection.RemoteEndPoint.Port, ServiceCoreSettings.Default.CafeAuthDomainSN));
			this.journalID = 0L;
			if (this.cafeauthProcessing)
			{
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.connectToCafeAuthServer)), 10000);
			}
		}

		private void connection_Disconnected(object sender, EventArgs e)
		{
			Log<CafeAuthService>.Logger.Fatal("Disconnected from Session server.");
			CafeAuthService.AddCafeAuthServiceLog(this.journalID, "Disconnected", "", "");
			this.journalID = 0L;
			Scheduler.Schedule(base.Thread, Job.Create(new Action(this.connectToCafeAuthServer)), 10000);
		}

		private void connection_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			Log<CafeAuthService>.Logger.Fatal("Exception occurred in Connection between Session server", e.Value);
			CafeAuthService.AddCafeAuthServiceLog(this.journalID, "Exception occurred", "", e.Value.Message.Substring(0, Math.Min(e.Value.Message.Length, 29)));
		}

		private void connection_MessageReceived(MessageRequest request)
		{
			Log<CafeAuthService>.Logger.InfoFormat("Message from Session server: [{0}]", request);
			Option option = request.Option;
			bool kick = false;
			if (request.ExtendInfos.IsNeedShutDown)
			{
				kick = true;
			}
			this.processMessage(option, request.ExtendInfos, request.Argument, request.NexonID, kick);
		}

		private void connection_InitializeSent(Initialize request)
		{
			Log<CafeAuthService>.Logger.WarnFormat("Initialize request sent to Session server: [{0}]", request);
			CafeAuthService.AddCafeAuthServiceLog(this.journalID, "InitializeSent", string.Format("GameSN:{0}", request.GameSN), string.Format("DomainSN:{0}", request.DomainSN));
		}

		private void connection_InitializeResponsed(InitializeResponse response)
		{
			Log<CafeAuthService>.Logger.InfoFormat("Initialize response from Session server: [{0}]", response);
			InitializeResult result = response.Result;
			if (result == InitializeResult.Success)
			{
				Log<CafeAuthService>.Logger.WarnFormat("Successfully initialized with DoaminSN {0}", response.DomainSN);
				CafeAuthService.AddCafeAuthServiceLog(this.journalID, "InitializeResponse", "Success", "");
				return;
			}
			Log<CafeAuthService>.Logger.FatalFormat("Initialization failed ({0}) with DoaminSN {1}: {2}", response.Result, response.DomainSN, response.Message);
			CafeAuthService.AddCafeAuthServiceLog(this.journalID, "InitializeResponse", response.Result.ToString().Substring(0, Math.Min(response.Result.ToString().Length, 19)), response.Message.Substring(0, Math.Min(response.Message.Length, 29)));
			Scheduler.Schedule(base.Thread, Job.Create(new Action(this.connection.RequestInitialize)), 10000);
		}

		private void connection_TerminateRequested(TerminateRequest request)
		{
			Log<CafeAuthService>.Logger.WarnFormat("Response stop request from Session server: [{0}]", request);
			Option option = request.Option;
			this.processMessage(option, request.ExtendInfos, 0, request.NexonID, true);
		}

		private void connection_SynchronizeRequested(SynchronizeRequest request)
		{
			Dictionary<long, byte> dictionary = new Dictionary<long, byte>();
			Log<CafeAuthService>.Logger.InfoFormat("Processing [{0}] sync request...", request.SessionList.Count);
			if (request.SessionList.Count == 0)
			{
				this.Relogin();
				return;
			}
			foreach (long key in request.SessionList)
			{
				if (!dictionary.ContainsKey(key))
				{
					dictionary.Add(key, this.SessionDic.ContainsKey(key) ?(byte) 1 :(byte) 0);
				}
			}
			this.connection.SynchronizeReply(request.IsMonitering, dictionary);
		}

		private void Relogin()
		{
			if (this.WaitQueue.Count != 0)
			{
				foreach (CafeAuth arg in this.WaitQueue)
				{
					Scheduler.Schedule(base.Thread, Job.Create<CafeAuth>(new Action<CafeAuth>(this.TryLogin), arg), 0);
				}
			}
		}

		private void TryLogin(CafeAuth entity)
		{
			AsyncResultSync sync = new AsyncResultSync(base.Thread);
			sync.OnFinished += delegate(ISynchronizable __)
			{
				if (sync != null && this.WaitQueue.Contains(entity))
				{
					LoginResponse loginResponse = this.connection.EndLogin(sync.AsyncResult);
					if (loginResponse != null)
					{
						if (loginResponse.Result == AuthorizeResult.Allowed)
						{
							entity.FrontendConn.RequestOperation(new NotifyCafeAuthResult());
						}
						if (loginResponse.Result == AuthorizeResult.Forbidden || loginResponse.Result == AuthorizeResult.Terminate)
						{
							SendPacket op = SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_Expired"));
							entity.FrontendConn.RequestOperation(op);
							Scheduler.Schedule(this.Thread, Job.Create<CafeAuth>(new Action<CafeAuth>(this.Kick_User), entity), 5000);
						}
						entity.SessionNo = loginResponse.SessionNo;
						this.WaitQueue.Remove(entity);
						entity.SetValid();
					}
				}
			};
			this.connection.BeginLogin(entity.NexonID, entity.CharacterID, entity.LocalAddress, entity.RemoteAddress, entity.CanTry, entity.IsTrial, entity.MID, (entity.GameRoomClient == 0) ? null : new int[]
			{
				entity.GameRoomClient
			}, new AsyncCallback(sync.AsyncCallback), entity);
		}

		private void connection_LoginRecoveryRequested(LoginResponse response)
		{
			if (response != null)
			{
				CafeAuth cafeAuth = null;
				if (this.NxIDToEntityDic.TryGetValue(response.NexonID, out cafeAuth))
				{
					if (response.Result == AuthorizeResult.Allowed)
					{
						cafeAuth.FrontendConn.RequestOperation(new NotifyCafeAuthResult());
					}
					if (response.Result == AuthorizeResult.Forbidden || response.Result == AuthorizeResult.Terminate)
					{
						SendPacket op;
						if (response.Option == Option.AccountShutdowned || (response.ExtendInfos != null && response.ExtendInfos.IsShutDownEnabled))
						{
							op = SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_Shutdown"));
						}
						else if (response.ExtendInfos != null && response.ExtendInfos.IsShutDownEnabled)
						{
							op = SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_IPBlocked"));
						}
						else
						{
							op = SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_Expired"));
						}
						cafeAuth.FrontendConn.RequestOperation(op);
						Scheduler.Schedule(base.Thread, Job.Create<CafeAuth>(new Action<CafeAuth>(this.Kick_User), cafeAuth), 5000);
					}
					cafeAuth.SessionNo = response.SessionNo;
					this.WaitQueue.Remove(cafeAuth);
					cafeAuth.SetValid();
				}
			}
		}

		private void connection_RetryLoginRequested(string nxID)
		{
			CafeAuth entity = null;
			if (this.NxIDToEntityDic.TryGetValue(nxID, out entity))
			{
				this.TryLogin(entity);
			}
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
				Log<CafeAuthService>.Logger.Warn("Failed to connect to Session server while processing AsyncResult.");
				return null;
			}
			Log<CafeAuthService>.Logger.InfoFormat("Initial response received from Session server: [{0}]", loginResponse);
			if (loginResponse.Option == Option.NoOption && (loginResponse.Result == AuthorizeResult.Allowed || loginResponse.Result == AuthorizeResult.Terminate))
			{
				Log<CafeAuthService>.Logger.ErrorFormat("Unexpected response from Session server: [{0}]", loginResponse);
			}
			AuthorizeResult result = loginResponse.Result;
			Option option = loginResponse.Option;
			bool reloginRequired = false;
			bool isShutDownEnabled = false;
			int pcroomNo = 0;
			if (loginResponse.ExtendInfos != null)
			{
				if (loginResponse.ExtendInfos.IsShutDownEnabled)
				{
					result = AuthorizeResult.Terminate;
					option = Option.AccountShutdowned;
				}
				if (loginResponse.ExtendInfos.IsNeedShutDown)
				{
					if (loginResponse.ExtendInfos.ShutDownErrorCode == PolicyInfoErrorCode.CannotAutholizing || loginResponse.ExtendInfos.ShutDownErrorCode == PolicyInfoErrorCode.CannotFindAgeInfo)
					{
						reloginRequired = true;
					}
					else if (loginResponse.ExtendInfos.ShutDownErrorCode != PolicyInfoErrorCode.Success)
					{
						isShutDownEnabled = true;
					}
				}
				else if (loginResponse.ExtendInfos.ShutDownTime != DateTime.MinValue)
				{
					double totalMinutes = (loginResponse.ExtendInfos.ShutDownTime - DateTime.Now).TotalMinutes;
					int num;
					if (totalMinutes <= 30.0)
					{
						num = 0;
					}
					else
					{
						num = (int)(totalMinutes - 30.0);
					}
					if (!this.ScheduleShutdownMessageNexonID.Contains(loginResponse.NexonID))
					{
						this.ScheduleShutdownMessageNexonID.Add(loginResponse.NexonID);
						Scheduler.Schedule(base.Thread, Job.Create<Option, ExtendInformation, int, string>(new Action<Option, ExtendInformation, int, string>(this.NotifyMessage), Option.NoOption, loginResponse.ExtendInfos, 0, loginResponse.NexonID), num * 60 * 1000);
					}
					Scheduler.Schedule(base.Thread, Job.Create<string>(new Action<string>(this.Kick_User), loginResponse.NexonID), (int)totalMinutes * 60 * 1000 + 300000);
				}
				if (loginResponse.ExtendInfos.PCRoomNo > 0)
				{
					pcroomNo = loginResponse.ExtendInfos.PCRoomNo;
				}
			}
			return new CafeAuthResult
			{
				Result = result,
				Option = option,
				SessionNo = loginResponse.SessionNo,
				IsShutDownEnabled = isShutDownEnabled,
				CafeLevel = 1,
				ReloginRequired = reloginRequired,
				PCRoomNo = pcroomNo
			};
		}

		public void Logout(long sessionNo, string nexonID, string characterID, IPAddress remoteAddress, bool canTry)
		{
			if (this.Valid)
			{
				this.connection.Logout(sessionNo, nexonID, characterID, remoteAddress, canTry);
				Log<CafeAuthService>.Logger.DebugFormat("Logout from Session server: [{0} {1}]", characterID, remoteAddress);
			}
		}

		private void Kick_User(string nexonID)
		{
			CafeAuth target;
			if (this.NxIDToEntityDic.TryGetValue(nexonID, out target))
			{
				this.Kick_User(target);
			}
		}

		private void Kick_User(CafeAuth target)
		{
			if (target == null || target.FrontendConn == null)
			{
				return;
			}
			target.FrontendConn.RequestOperation(new DisconnectClient());
		}

        private void NotifyMessage(Option option, ExtendInformation extendInfo, int arument, string nxID)
        {
            CafeAuth cafeAuth;
            SendPacket sendPacket;
            bool flag;
            SystemMessage systemMessage = null;
            Option option1 = option;
            switch (option1)
            {
                case Option.NoOption:
                    {
                        if (extendInfo == null)
                        {
                            break;
                        }
                        switch (extendInfo.ShutDownErrorCode)
                        {
                            case PolicyInfoErrorCode.Success:
                                {
                                    if (systemMessage != null)
                                    {
                                        if (this.NxIDToEntityDic.TryGetValue(nxID, out cafeAuth) && systemMessage != null)
                                        {
                                            sendPacket = SendPacket.Create<SystemMessage>(systemMessage);
                                            cafeAuth.FrontendConn.RequestOperation(sendPacket);
                                        }
                                        if (this.ScheduleShutdownMessageNexonID.Contains(nxID))
                                        {
                                            flag = this.ScheduleShutdownMessageNexonID.Remove(nxID);
                                        }
                                        return;
                                    }
                                    DateTime shutDownTime = extendInfo.ShutDownTime;
                                    if (extendInfo.ShutDownTime == DateTime.MinValue)
                                    {
                                        if (this.NxIDToEntityDic.TryGetValue(nxID, out cafeAuth) && systemMessage != null)
                                        {
                                            sendPacket = SendPacket.Create<SystemMessage>(systemMessage);
                                            cafeAuth.FrontendConn.RequestOperation(sendPacket);
                                        }
                                        if (this.ScheduleShutdownMessageNexonID.Contains(nxID))
                                        {
                                            flag = this.ScheduleShutdownMessageNexonID.Remove(nxID);
                                        }
                                        return;
                                    }
                                    object[] month = new object[] { extendInfo.ShutDownTime.Month, extendInfo.ShutDownTime.Day, extendInfo.ShutDownTime.Hour };
                                    systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_Shutdown_Notify_EndTime", month);
                                    break;
                                }
                            case PolicyInfoErrorCode.CannotFindAgeInfo:
                                {
                                    systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_CannotFindAgeInfo");
                                    goto case PolicyInfoErrorCode.Success;
                                }
                            case PolicyInfoErrorCode.CannotAutholizing:
                                {
                                    systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_CannotAutholizing");
                                    goto case PolicyInfoErrorCode.Success;
                                }
                            case PolicyInfoErrorCode.BlockFromShutdownSystem:
                                {
                                    systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_Shutdown");
                                    goto case PolicyInfoErrorCode.Success;
                                }
                            case PolicyInfoErrorCode.BlockFromSelectiveShutdownSystem:
                                {
                                    systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_SelectiveShutdown");
                                    goto case PolicyInfoErrorCode.Success;
                                }
                            default:
                                {
                                    systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_CannotPlayGame");
                                    goto case PolicyInfoErrorCode.Success;
                                }
                        }
                        break;
                    }
                case Option.AddressNotAllowed:
                    {
                        systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_IPBlocked");
                        break;
                    }
                case Option.AddressMaxConnected:
                    {
                        systemMessage = new SystemMessage(SystemMessageCategory.Dialog, "GameUI_Login_Fail_Cafe_MaxConnected");
                        break;
                    }
                case Option.AddressExpired:
                    {
                        systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_Expired");
                        break;
                    }
                default:
                    {
                        switch (option1)
                        {
                            case Option.WelcomePrepaid:
                                {
                                    if (arument <= 60)
                                    {
                                        systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_TimeLeft_LessThanOneHour");
                                    }
                                    else
                                    {
                                        object[] objArray = new object[] { arument / 60 };
                                        systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_TimeLeft", objArray);
                                    }
                                    break;
                                }
                            case Option.PrepaidExpired:
                                break;
                            case Option.PrepaidExhausted:
                                {
                                    systemMessage = new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Cafe_Expired");
                                    break;
                                }
                            default:
                                {
                                    systemMessage = (option1 == Option.AccountShutdowned ? new SystemMessage(SystemMessageCategory.Notice, "GameUI_Heroes_Policy_Shutdown") : new SystemMessage(SystemMessageCategory.Dialog, "GameUI_Heroes_Policy_CannotPlayGame"));
                                    break;
                                }
                        }
                        break;
                    }
            }
            if (this.NxIDToEntityDic.TryGetValue(nxID, out cafeAuth) && systemMessage != null)
            {
                sendPacket = SendPacket.Create<SystemMessage>(systemMessage);
                cafeAuth.FrontendConn.RequestOperation(sendPacket);
            }
            if (this.ScheduleShutdownMessageNexonID.Contains(nxID))
            {
                flag = this.ScheduleShutdownMessageNexonID.Remove(nxID);
            }
        }

        private void processMessage(Option option, ExtendInformation extendInfo, int kickRemainMin, string nxID, bool kick)
		{
			this.NotifyMessage(option, extendInfo, kickRemainMin, nxID);
			CafeAuth cafeAuth;
			if (this.NxIDToEntityDic.TryGetValue(nxID, out cafeAuth))
			{
				cafeAuth.ReportCafeAuthMessage(option.ToString());
				if (kick)
				{
					Scheduler.Schedule(base.Thread, Job.Create<CafeAuth>(new Action<CafeAuth>(this.Kick_User), cafeAuth), 5000);
				}
			}
		}

		public static Service StartService(string ip, string portstr)
		{
			CafeAuthService cafeAuthService = new CafeAuthService();
			ServiceInvoker.StartService(ip, portstr, cafeAuthService);
			return cafeAuthService;
		}

		internal void AddToWaitQueue(CafeAuth entity)
		{
			this.WaitQueue.Add(entity);
		}

		private static void AddCafeAuthServiceLog(long journalID, string str1, string str2, string str3)
		{
			string text = "";
			if (str1.Length > 30)
			{
				if (text.Length > 0)
				{
					text += ", ";
				}
				text += "str1";
			}
			if (str2.Length > 20)
			{
				if (text.Length > 0)
				{
					text += ", ";
				}
				text += "str2";
			}
			if (str3.Length > 30)
			{
				if (text.Length > 0)
				{
					text += ", ";
				}
				text += "str3";
			}
			if (text.Length > 0)
			{
				throw new ArgumentException("Too long argument(s) is specified to AddCafeAuthServiceLog.", text);
			}
			using (LogDBDataContext logDBDataContext = new LogDBDataContext())
			{
				logDBDataContext.AddLoginLedger(new long?(journalID), str1, str2, str3);
			}
		}

		private static long AddLoginJournal()
		{
			long result = -1L;
			try
			{
				using (LogDBDataContext logDBDataContext = new LogDBDataContext())
				{
					long? num = new long?(-1L);
					logDBDataContext.AddLoginJournal(ref num);
					result = num.Value;
				}
			}
			catch (Exception ex)
			{
				Log<CafeAuth>.Logger.Error(ex);
			}
			return result;
		}

		private const int RECONNECT_TRY_INTERVAL_IN_SEC = 10;

		private const int RECONNECT_TRY_INTERVAL_IN_MILLISEC = 10000;

		private Connection connection;

		private long journalID;

		internal Dictionary<string, CafeAuth> NxIDToEntityDic = new Dictionary<string, CafeAuth>();

		internal Dictionary<long, CafeAuth> SessionDic = new Dictionary<long, CafeAuth>();

		internal List<CafeAuth> WaitQueue = new List<CafeAuth>();

		internal List<string> ScheduleShutdownMessageNexonID = new List<string>();

		internal PCRoomNoManager PCRoomManager = new PCRoomNoManager();

		private bool cafeauthProcessing;
	}
}
