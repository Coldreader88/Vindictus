using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using AdminClientServiceCore.Messages;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.DSServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.ItemServiceOperations;
using ServiceCore.PartyServiceOperations;
using ServiceCore.StoryServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;
using Utility;

namespace AdminClientServiceCore
{
	public class AdminClientServicePeer
	{
		public AdminClientServicePeer(AdminClientService p, TcpClient t)
		{
			this.parent = p;
			if (t != null)
			{
				this.peer = t;
				t.ConnectionSucceed += this.OnConnectionSucceed;
				t.ConnectionFail += this.OnConnectionFail;
				t.Disconnected += this.OnDisconnected;
				t.ExceptionOccur += this.OnException;
				t.PacketReceive += this.OnPacketReceive;
				return;
			}
			Scheduler.Schedule(this.parent.Thread, Job.Create(new Action(this.PeriodicQuery)), this.QueryUserCountMilisecond);
			Scheduler.Schedule(this.parent.Thread, Job.Create(new Action(this.PeriodicPartyQuery)), this.QueryUserCountMilisecond);
			Scheduler.Schedule(this.parent.Thread, Job.Create(new Action(this.PeriodicDSQuery)), this.QueryUserCountMilisecond);
			Scheduler.Schedule(this.parent.Thread, Job.Create(new Action(this.clearConnectedInfo)), 0);
		}

		private void clearConnectedInfo()
		{
			try
			{
				using (EventDataContext eventDataContext = new EventDataContext())
				{
					eventDataContext.InitConnectedInfo();
				}
			}
			catch (Exception ex)
			{
				Log<AdminClientService>.Logger.Error("exception in clearConnectedInfo", ex);
			}
		}

		private void OnConnectionFail(object sender, EventArgs<Exception> e)
		{
			Log<AdminClientServicePeer>.Logger.Error("AdminClient Connection Failed!");
			this.OnException(sender, e);
		}

		private void OnConnectionSucceed(object sender, EventArgs e)
		{
			Log<AdminClientServicePeer>.Logger.DebugFormat("AdminClient Connected from [{0}]", this.peer.RemoteEndPoint.Address.ToString());
			this.ReportAdminClientLog("Connect");
		}

		private void OnPacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			this.parent.MF.Handle(packet, this);
		}

		private void OnException(object sender, EventArgs<Exception> e)
		{
			Log<AdminClientServicePeer>.Logger.Error("AdminClient exception ", e.Value);
			this.Disconnect();
		}

		public void Transmit(Packet packet)
		{
			if (this.peer == null)
			{
				return;
			}
			this.peer.Transmit(packet);
		}

		public void Disconnect()
		{
			if (this.peer == null)
			{
				return;
			}
			this.peer.Disconnect();
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			Log<AdminClientServicePeer>.Logger.Debug("[Disconnect] AdminClient disconnected");
			this.ReportAdminClientLog("Disconnect");
		}

		public void ProcessMessage(object message)
		{
			if (message is AdminRequestClientCountMessage)
			{
				AdminRequestClientCountMessage adminRequestClientCountMessage = message as AdminRequestClientCountMessage;
				Log<AdminClientServicePeer>.Logger.Debug(adminRequestClientCountMessage.ToString());
				this.ReportAdminClientLog("UserCount");
				this.QueryUserCount();
			}
			if (message is AdminRequestClientCountMessage2)
			{
				AdminRequestClientCountMessage2 adminRequestClientCountMessage2 = message as AdminRequestClientCountMessage2;
				Log<AdminClientServicePeer>.Logger.Debug(adminRequestClientCountMessage2.ToString());
				this.ReportAdminClientLog("UserCount");
				AdminReportClientCountMessage2 adminReportClientCountMessage = new AdminReportClientCountMessage2();
				adminReportClientCountMessage.AddUserCount("User by Category", AdminClientServicePeer.userByCategory);
				adminReportClientCountMessage.AddUserCount("User by Server", AdminClientServicePeer.userByServer);
				if (this.peer != null)
				{
					this.peer.Transmit(SerializeWriter.ToBinary<AdminReportClientCountMessage2>(adminReportClientCountMessage));
					return;
				}
			}
			else
			{
				if (message is AdminRequestServerStartMessage)
				{
					AdminRequestServerStartMessage adminRequestServerStartMessage = message as AdminRequestServerStartMessage;
					Log<AdminClientServicePeer>.Logger.Debug(adminRequestServerStartMessage.ToString());
					return;
				}
				if (message is AdminRequestNotifyMessage)
				{
					AdminRequestNotifyMessage adminRequestNotifyMessage = message as AdminRequestNotifyMessage;
					Log<AdminClientServicePeer>.Logger.Debug(adminRequestNotifyMessage.ToString());
					this.ReportAdminClientLog("NotifyMessage : ...");
					char[] separator = new char[]
					{
						'\n',
						'\r'
					};
					string[] array = adminRequestNotifyMessage.NotifyText.Split(separator);
					int num = 0;
					foreach (string text in array)
					{
						if (text.Length != 0)
						{
							Scheduler.Schedule(this.parent.Thread, Job.Create<string>(new Action<string>(this.BroadcastMessage), text), num * this.NotifyDelayInMilisecond);
							num++;
						}
					}
					return;
				}
				if (message is AdminRequestFreeTokenMessage)
				{
					AdminRequestFreeTokenMessage adminRequestFreeTokenMessage = message as AdminRequestFreeTokenMessage;
					Log<AdminClientServicePeer>.Logger.Debug(adminRequestFreeTokenMessage.ToString());
					this.ReportAdminClientLog(string.Format("FreeToken : {0}", adminRequestFreeTokenMessage.On));
					this.parent.SetFreeTokenMode(adminRequestFreeTokenMessage.On, this);
					return;
				}
				if (message is AdminRequestConsoleCommandMessage)
				{
					AdminRequestConsoleCommandMessage adminRequestConsoleCommandMessage = message as AdminRequestConsoleCommandMessage;
					Log<AdminClientServicePeer>.Logger.Debug(adminRequestConsoleCommandMessage.ToString());
					this.ReportAdminClientLog(string.Format("ConsoleCommand : {0}", adminRequestConsoleCommandMessage.Command));
					this.parent.ProcessConsoleCommand(adminRequestConsoleCommandMessage.Command, adminRequestConsoleCommandMessage.Arguments, this);
					return;
				}
				if (message is AdminRequestShutDownMessage)
				{
					AdminRequestShutDownMessage adminRequestShutDownMessage = message as AdminRequestShutDownMessage;
					Log<AdminClientServicePeer>.Logger.Debug(adminRequestShutDownMessage.ToString());
					this.ReportAdminClientLog(string.Format("ShutDown : {0}", adminRequestShutDownMessage.Delay));
					int[] array3 = this.parent.FrontendServiceIDs();
					ShutDown[] array4 = new ShutDown[array3.Length];
					for (int j = 0; j < array3.Length; j++)
					{
						array4[j] = new ShutDown();
						array4[j].Delay = adminRequestShutDownMessage.Delay;
						array4[j].Announce = adminRequestShutDownMessage.Announce;
						this.parent.RequestOperation(array3[j], array4[j]);
					}
					return;
				}
				if (message is AdminRequestKickMessage)
				{
					AdminRequestKickMessage adminRequestKickMessage = message as AdminRequestKickMessage;
					Log<AdminClientServicePeer>.Logger.Debug(adminRequestKickMessage.ToString());
					this.ReportAdminClientLog(string.Format("Kick {0} [{1}]", adminRequestKickMessage.IsUID ? "User" : "Character", adminRequestKickMessage.ID));
					int[] array5 = this.parent.FrontendServiceIDs();
					if (adminRequestKickMessage.IsUID)
					{
						for (int k = 0; k < array5.Length; k++)
						{
							ExecCommand op = new ExecCommand("KickUserWithUID", adminRequestKickMessage.ID);
							this.parent.RequestOperation(array5[k], op);
						}
						return;
					}
					for (int l = 0; l < array5.Length; l++)
					{
						ExecCommand op2 = new ExecCommand("KickUserWithCID", adminRequestKickMessage.ID);
						this.parent.RequestOperation(array5[l], op2);
					}
					return;
				}
				else
				{
					if (message is AdminRequestEmergencyStopMessage)
					{
						AdminRequestEmergencyStopMessage adminRequestEmergencyStopMessage = message as AdminRequestEmergencyStopMessage;
						Log<AdminClientServicePeer>.Logger.Debug(adminRequestEmergencyStopMessage.ToString());
						if (adminRequestEmergencyStopMessage.TargetState)
						{
							if (this.parent.StoppedServices.Contains(adminRequestEmergencyStopMessage.TargetService))
							{
								Log<AdminClientServicePeer>.Logger.ErrorFormat("이미 정지된 서비스입니다 : [{0}]", adminRequestEmergencyStopMessage.TargetService);
								return;
							}
							this.ReportAdminClientLog(string.Format("Emergency stop {0}", adminRequestEmergencyStopMessage.TargetService));
						}
						else
						{
							if (!this.parent.StoppedServices.Contains(adminRequestEmergencyStopMessage.TargetService))
							{
								Log<AdminClientServicePeer>.Logger.ErrorFormat("정지되지 않은 서비스입니다 : [{0}]", adminRequestEmergencyStopMessage.TargetService);
								return;
							}
							this.ReportAdminClientLog(string.Format("Restart {0}", adminRequestEmergencyStopMessage.TargetService));
						}
						string targetService;
						if ((targetService = adminRequestEmergencyStopMessage.TargetService) != null)
						{
							if (!(targetService == "CraftItem"))
							{
								if (!(targetService == "Shipping"))
								{
									if (!(targetService == "CreateCharacter"))
									{
										if (!(targetService == "CashShop"))
										{
											if (targetService == "Storytelling")
											{
												int[] array6 = this.parent.StoryServiceIDs();
												StopStorytelling[] array7 = new StopStorytelling[array6.Length];
												for (int m = 0; m < array6.Length; m++)
												{
													array7[m] = new StopStorytelling
													{
														TargetState = adminRequestEmergencyStopMessage.TargetState
													};
													this.parent.RequestOperation(array6[m], array7[m]);
												}
											}
										}
										else
										{
											int[] array8 = this.parent.CashShopServiceIDs();
											StopCashShop[] array9 = new StopCashShop[array8.Length];
											for (int n = 0; n < array8.Length; n++)
											{
												array9[n] = new StopCashShop
												{
													TargetState = adminRequestEmergencyStopMessage.TargetState
												};
												this.parent.RequestOperation(array8[n], array9[n]);
											}
										}
									}
									else
									{
										int[] array10 = this.parent.CharacterServiceIDs();
										StopCreateCharacter[] array11 = new StopCreateCharacter[array10.Length];
										for (int num2 = 0; num2 < array10.Length; num2++)
										{
											array11[num2] = new StopCreateCharacter
											{
												TargetState = adminRequestEmergencyStopMessage.TargetState
											};
											this.parent.RequestOperation(array10[num2], array11[num2]);
										}
									}
								}
								else
								{
									int[] array12 = this.parent.PartyServiceIDs();
									StopShipping[] array13 = new StopShipping[array12.Length];
									for (int num3 = 0; num3 < array12.Length; num3++)
									{
										array13[num3] = new StopShipping
										{
											TargetState = adminRequestEmergencyStopMessage.TargetState
										};
										this.parent.RequestOperation(array12[num3], array13[num3]);
									}
								}
							}
							else
							{
								int[] array14 = this.parent.ItemServiceIDs();
								StopCraftItem[] array15 = new StopCraftItem[array14.Length];
								for (int num4 = 0; num4 < array14.Length; num4++)
								{
									array15[num4] = new StopCraftItem
									{
										TargetState = adminRequestEmergencyStopMessage.TargetState
									};
									this.parent.RequestOperation(array14[num4], array15[num4]);
								}
							}
						}
						if (adminRequestEmergencyStopMessage.TargetState)
						{
							this.parent.StoppedServices.Add(adminRequestEmergencyStopMessage.TargetService);
						}
						else
						{
							this.parent.StoppedServices.Remove(adminRequestEmergencyStopMessage.TargetService);
						}
						this.parent.NotifyStoppedServices(this.parent.StoppedServices);
						return;
					}
					if (message is AdminBroadcastConsoleCommandMessage)
					{
						AdminBroadcastConsoleCommandMessage adminBroadcastConsoleCommandMessage = message as AdminBroadcastConsoleCommandMessage;
						this.ReportAdminClientLog(string.Format("ConsoleCommand [{0}]{1}", adminBroadcastConsoleCommandMessage.isServerCommand ? "server" : "client", adminBroadcastConsoleCommandMessage.commandString));
						this.BroadCastConsoleCommand(adminBroadcastConsoleCommandMessage.isServerCommand, adminBroadcastConsoleCommandMessage.commandString);
						return;
					}
					if (message is AdminItemFestivalEventMessage)
					{
						AdminItemFestivalEventMessage adminItemFestivalEventMessage = message as AdminItemFestivalEventMessage;
						string itemClass = adminItemFestivalEventMessage.ItemClass;
						if (adminItemFestivalEventMessage.Amount != 0)
						{
							int amount = adminItemFestivalEventMessage.Amount;
						}
						string message2 = adminItemFestivalEventMessage.Message;
						bool flag = true;
						if (message2.Length == 0)
						{
							return;
						}
						if (flag)
						{
							int[] array16 = this.parent.ItemServiceIDs();
							ItemFestival[] array17 = new ItemFestival[array16.Length];
							for (int num5 = 0; num5 < array16.Length; num5++)
							{
								array17[num5] = new ItemFestival
								{
									Message = adminItemFestivalEventMessage.Message,
									IsCafeOnly = adminItemFestivalEventMessage.IsCafe
								};
								this.parent.RequestOperation(array16[num5], array17[num5]);
							}
							return;
						}
					}
					else
					{
						if (message is AdminItemFestivalEventMessage2)
						{
							AdminItemFestivalEventMessage2 adminItemFestivalEventMessage2 = message as AdminItemFestivalEventMessage2;
							this.ProcessAdminItemFestivalEventMessage(adminItemFestivalEventMessage2.ItemClass, adminItemFestivalEventMessage2.Amount, adminItemFestivalEventMessage2.Message, adminItemFestivalEventMessage2.IsCafe, null);
							return;
						}
						if (message is AdminItemFestivalEventMessage3)
						{
							AdminItemFestivalEventMessage3 adminItemFestivalEventMessage3 = message as AdminItemFestivalEventMessage3;
							if (adminItemFestivalEventMessage3.IsExprire && adminItemFestivalEventMessage3.ExpireTime != null)
							{
								DateTime value = TimeZoneInfo.ConvertTimeToUtc(adminItemFestivalEventMessage3.ExpireTime.Value);
								this.ProcessAdminItemFestivalEventMessage(adminItemFestivalEventMessage3.ItemClass, adminItemFestivalEventMessage3.Amount, adminItemFestivalEventMessage3.Message, adminItemFestivalEventMessage3.IsCafe, new DateTime?(value));
								return;
							}
							this.ProcessAdminItemFestivalEventMessage(adminItemFestivalEventMessage3.ItemClass, adminItemFestivalEventMessage3.Amount, adminItemFestivalEventMessage3.Message, adminItemFestivalEventMessage3.IsCafe, null);
							return;
						}
						else
						{
							if (message is AdminEntendCashItemExpire)
							{
								AdminEntendCashItemExpire adminEntendCashItemExpire = message as AdminEntendCashItemExpire;
								DateTime dateTime = TimeZoneInfo.ConvertTimeToUtc(adminEntendCashItemExpire.FromDate);
								using (EventDataContext eventDataContext = new EventDataContext())
								{
									try
									{
										eventDataContext.Connection.Open();
										eventDataContext.CommandTimeout = 3600;
										eventDataContext.ExtendCashItems(new DateTime?(dateTime), new int?(adminEntendCashItemExpire.Minutes));
									}
									catch (Exception ex)
									{
										Log<AdminClientServicePeer>.Logger.Error("ExtendCashItem", ex);
										this.peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("ExtendCashItem : UTC[{0}] {1} Failed", dateTime, adminEntendCashItemExpire.Minutes))));
										return;
									}
								}
								this.peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.INFO, string.Format("ExtendCashItem : UTC[{0}] {1} Success", dateTime, adminEntendCashItemExpire.Minutes))));
								this.ReportAdminClientLog(string.Format("ExtendCashItem UTC[{0}] {1} Success", dateTime, adminEntendCashItemExpire.Minutes));
								return;
							}
							if (message is AdminRequestDSChetToggleMessage)
							{
								AdminRequestDSChetToggleMessage adminRequestDSChetToggleMessage = message as AdminRequestDSChetToggleMessage;
								Log<AdminClientServicePeer>.Logger.Debug(adminRequestDSChetToggleMessage.ToString());
								this.ReportAdminClientLog(string.Format("DS Cheat : {0}", adminRequestDSChetToggleMessage.On));
								this.parent.SetDSCheat(adminRequestDSChetToggleMessage.On);
							}
						}
					}
				}
			}
		}

		private void ProcessAdminItemFestivalEventMessage(string itemClass, int amount, string message, bool isCafe, DateTime? expireTime)
		{
			if (message.Length == 0)
			{
				return;
			}
			if (amount > 0)
			{
				Func<string, int, DateTime?, Exception> func = new Func<string, int, DateTime?, Exception>(this.ItemFestivalEx);
				this.ReportAdminClientLog(string.Format("AdminItemFestivalEventMessage {0} {1}", itemClass, amount));
				this.peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.INFO, string.Format("\r\n========================================\r\n== Starting Item Festival : {0} {1}\r\n== This takes for a while", itemClass, amount))));
				Action<Exception> OnError = delegate(Exception e)
				{
					this.ReportAdminClientLog(string.Format("AdminItemFestivalEventMessage {0} {1} Failed", itemClass, amount));
					this.peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, "==Failed")));
					Log<AdminClientServicePeer>.Logger.Error("ItemFestival Exception", e);
				};
				Action OnSuccess = delegate
				{
					bool flag = true;
					if (flag)
					{
						int[] array = this.parent.ItemServiceIDs();
						ItemFestival[] array2 = new ItemFestival[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array2[i] = new ItemFestival
							{
								Message = message,
								IsCafeOnly = isCafe
							};
							this.parent.RequestOperation(array[i], array2[i]);
						}
					}
					this.ReportAdminClientLog(string.Format("AdminItemFestivalEventMessage {0} {1} Success", itemClass, amount));
					this.peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.INFO, "\r\n== Success\r\n========================================")));
				};
				AsyncCallback callback = delegate(IAsyncResult ar)
				{
					AsyncResult asyncResult = (AsyncResult)ar;
					Func<string, int, DateTime?, Exception> func2 = (Func<string, int, DateTime?, Exception>)asyncResult.AsyncDelegate;
					Exception ex = func2.EndInvoke(ar);
					if (ex != null)
					{
						this.parent.Thread.Enqueue(Job.Create<Exception>(OnError, ex));
					}
					else
					{
						this.parent.Thread.Enqueue(Job.Create(OnSuccess));
					}
					AdminClientServicePeer.isRunningItemFestival = false;
				};
				if (AdminClientServicePeer.isRunningItemFestival)
				{
					this.peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("\r\n== Other ItemFestival operation is running. This work is canceled.\r\n====================== Canceled Item Festival : {0} {1}\r\n", itemClass, amount))));
					return;
				}
				AdminClientServicePeer.isRunningItemFestival = true;
				func.BeginInvoke(itemClass, amount, expireTime, callback, null);
			}
		}

		private Exception ItemFestivalEx(string itemClass, int count, DateTime? expireTime)
		{
			Exception result2;
			using (EventDataContext eventDataContext = new EventDataContext())
			{
				try
				{
					eventDataContext.Connection.Open();
					eventDataContext.CommandTimeout = 3600;
					eventDataContext.AddQueuedItemCurrentConnections(itemClass, new int?(count), expireTime);
				}
				catch (Exception result)
				{
					return result;
				}
				result2 = null;
			}
			return result2;
		}

		public void BroadCastConsoleCommand(bool isServerCommand, string commandString)
		{
			BroadcastPacket op;
			if (isServerCommand)
			{
				op = BroadcastPacket.Create<ServerCmdMessage>(new ServerCmdMessage(commandString, true));
			}
			else
			{
				op = BroadcastPacket.Create<ClientCmdMessage>(new ClientCmdMessage(commandString));
			}
			Log<AdminClientServicePeer>.Logger.Info(commandString);
			foreach (int serviceID in this.parent.FrontendServiceIDs())
			{
				this.parent.RequestOperation(serviceID, op);
			}
		}

		public void BroadcastMessage(string s)
		{
			NotifyClient op = new NotifyClient
			{
				Category = SystemMessageCategory.Notice,
				Message = new HeroesString(s)
			};
			Log<AdminClientServicePeer>.Logger.Info(s);
			foreach (int serviceID in this.parent.FrontendServiceIDs())
			{
				this.parent.RequestOperation(serviceID, op);
			}
		}

        public void QueryDSInfo()
        {
            int[] array = this.parent.DSServiceIDs();
            if (array.Length == 0)
            {
                Log<AdminClientServicePeer>.Logger.Debug("No DsService!");
                this.Transmit(SerializeWriter.ToBinary<AdminReportClientcountMessage>(new AdminReportClientcountMessage()));
                return;
            }
            OperationAggregation operationAggregation = new OperationAggregation(this.parent);
            QueryDSInfo[] ops = new QueryDSInfo[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                ops[i] = new QueryDSInfo();
                ops[i].TimeOut = 3000;
                operationAggregation.AddElement("DSService.DSService", new int?(array[i]), ops[i]);
            }
            operationAggregation.OnFinished += delegate (ISynchronizable finish)
            {
                DSReportMessage dSReportMessage = new DSReportMessage();
                dSReportMessage.ProcessList = new Dictionary<string, List<Dictionary<string, string>>>();
                QueryDSInfo[] ops1 = ops;
                for (int j = 0; j < ops1.Length; j++)
                {
                    QueryDSInfo queryDSInfo = ops1[j];
                    if (queryDSInfo.WaitingQueueInfo.Count > 0)
                    {
                        dSReportMessage.CountInfo = queryDSInfo.WaitingQueueInfo;
                    }
                    dSReportMessage.ProcessList.Add(queryDSInfo.IP, queryDSInfo.ProcessInfo);
                }
                if (this.peer == null)
                {
                    this.parent.NotifyDSProcessInfo(dSReportMessage);
                    return;
                }
                this.NotifyDSProcessInfo(dSReportMessage);
            };
            operationAggregation.OnSync();
        }

        public void QueryUserCount()
        {
            int[] ids = this.parent.FrontendServiceIDs();
            if (ids.Length == 0)
            {
                Log<AdminClientServicePeer>.Logger.Debug((object)"No frontend!");
                this.Transmit(SerializeWriter.ToBinary<AdminReportClientcountMessage>(new AdminReportClientcountMessage()));
            }
            else
            {
                OperationAggregation operationAggregation = new OperationAggregation((UnifiedNetwork.OperationService.Service)this.parent);
                QueryClientCount[] ops = new QueryClientCount[ids.Length];
                int num1 = 5000;
                if (FeatureMatrix.IsEnable("SafetyClientCount_TimeOutValue"))
                    num1 = FeatureMatrix.GetInteger("SafetyClientCount_TimeOutValue");
                bool isSafetyClientCount = false;
                if (FeatureMatrix.IsEnable("SafetyClientCount"))
                    isSafetyClientCount = true;
                for (int index = 0; index < ids.Length; ++index)
                {
                    ops[index] = new QueryClientCount();
                    ops[index].TimeOut = num1;
                    operationAggregation.AddElement("FrontendServiceCore.FrontendService", new int?(ids[index]), (Operation)ops[index]);
                }
                operationAggregation.OnFinished += (Action<ISynchronizable>)(after =>
                {
                    Log<AdminClientServicePeer>.Logger.InfoFormat("Finish Query");
                    AdminClientServicePeer.userByCategory = new Dictionary<string, int>();
                    AdminClientServicePeer.userByServer = new Dictionary<string, int>();
                    int num3 = 0;
                    int index = 0;
                    Dictionary<int, Dictionary<string, int>> dictionary1 = new Dictionary<int, Dictionary<string, int>>();
                    Dictionary<byte, Dictionary<string, int>> dictionary2 = new Dictionary<byte, Dictionary<string, int>>();
                    foreach (QueryClientCount queryClientCount in ops)
                    {
                        bool flag = false;
                        if (!queryClientCount.Result)
                        {
                            if (isSafetyClientCount)
                            {
                                flag = true;
                                Log<AdminClientServicePeer>.Logger.ErrorFormat("QueryClientCount Operation Timeout [{0}]", (object)ids[index]);
                            }
                            else
                                continue;
                        }
                        else if (queryClientCount.Result && isSafetyClientCount)
                        {
                            if (this.beforeUserCountResult.ContainsKey(ids[index]))
                                this.beforeUserCountResult[ids[index]] = queryClientCount.StateCounts;
                            else
                                this.beforeUserCountResult.Add(ids[index], queryClientCount.StateCounts);
                        }
                        if (flag)
                        {
                            Log<AdminClientServicePeer>.Logger.ErrorFormat("SafetyClientCount");
                            Dictionary<int, Dictionary<string, int>> dictionary3 = this.beforeUserCountResult.TryGetValue<int, Dictionary<int, Dictionary<string, int>>>(ids[index]);
                            if (dictionary3 != null)
                            {
                                int num2 = 0;
                                foreach (KeyValuePair<int, Dictionary<string, int>> keyValuePair1 in dictionary3)
                                {
                                    Dictionary<string, int> dictionary4;
                                    if (!dictionary1.TryGetValue(keyValuePair1.Key, out dictionary4))
                                    {
                                        dictionary4 = new Dictionary<string, int>();
                                        dictionary1.Add(keyValuePair1.Key, dictionary4);
                                    }
                                    foreach (KeyValuePair<string, int> keyValuePair2 in keyValuePair1.Value)
                                    {
                                        if (dictionary4.ContainsKey(keyValuePair2.Key))
                                        {
                                            Dictionary<string, int> dictionary5;
                                            string key;
                                            (dictionary5 = dictionary4)[key = keyValuePair2.Key] = dictionary5[key] + keyValuePair2.Value;
                                        }
                                        else
                                            dictionary4.Add(keyValuePair2.Key, keyValuePair2.Value);
                                    }
                                    if (keyValuePair1.Key == 0 && keyValuePair1.Value.ContainsKey("Total"))
                                        num2 = keyValuePair1.Value["Total"];
                                }
                                AdminClientServicePeer.userByServer.Add(this.parent.LookUp.GetLocation(ids[num3++]).ToString(), num2);
                            }
                            else
                                continue;
                        }
                        else
                        {
                            int num2 = 0;
                            foreach (KeyValuePair<int, Dictionary<string, int>> stateCount in queryClientCount.StateCounts)
                            {
                                Dictionary<string, int> dictionary3;
                                if (!dictionary1.TryGetValue(stateCount.Key, out dictionary3))
                                {
                                    dictionary3 = new Dictionary<string, int>();
                                    dictionary1.Add(stateCount.Key, dictionary3);
                                }
                                foreach (KeyValuePair<string, int> keyValuePair in stateCount.Value)
                                {
                                    if (dictionary3.ContainsKey(keyValuePair.Key))
                                    {
                                        Dictionary<string, int> dictionary4;
                                        string key;
                                        (dictionary4 = dictionary3)[key = keyValuePair.Key] = dictionary4[key] + keyValuePair.Value;
                                    }
                                    else
                                        dictionary3.Add(keyValuePair.Key, keyValuePair.Value);
                                }
                                if (stateCount.Key == 0 && stateCount.Value.ContainsKey("Total"))
                                    num2 = stateCount.Value["Total"];
                            }
                            AdminClientServicePeer.userByServer.Add(this.parent.LookUp.GetLocation(ids[num3++]).ToString(), num2);
                        }
                        if (FeatureMatrix.IsEnable("Channeling_UserCount"))
                        {
                            foreach (KeyValuePair<byte, Dictionary<string, int>> stateChannelCount in queryClientCount.StateChannelCounts)
                            {
                                Dictionary<string, int> dictionary3;
                                if (!dictionary2.TryGetValue(stateChannelCount.Key, out dictionary3))
                                {
                                    dictionary3 = new Dictionary<string, int>();
                                    dictionary2.Add(stateChannelCount.Key, dictionary3);
                                }
                                foreach (KeyValuePair<string, int> keyValuePair in stateChannelCount.Value)
                                {
                                    if (dictionary3.ContainsKey(keyValuePair.Key))
                                    {
                                        Dictionary<string, int> dictionary4;
                                        string key;
                                        (dictionary4 = dictionary3)[key = keyValuePair.Key] = dictionary4[key] + keyValuePair.Value;
                                    }
                                    else
                                        dictionary3.Add(keyValuePair.Key, keyValuePair.Value);
                                }
                            }
                        }
                        ++index;
                    }
                    DateTime now = DateTime.Now;
                    foreach (KeyValuePair<int, Dictionary<string, int>> keyValuePair in dictionary1)
                    {
                        if (keyValuePair.Key == 0 || keyValuePair.Value.ContainsKey("Total") && keyValuePair.Value["Total"] != 0)
                        {
                            string str = (string)null;
                            if (keyValuePair.Key != 0)
                            {
                                str = new StringBuilder().Append((char)(keyValuePair.Key / 256)).Append((char)(keyValuePair.Key % 256)).ToString();
                                if (str.Length > 2)
                                {
                                    Log<AdminClientServicePeer>.Logger.Error((object)string.Format("opRow.Key 데이터 잘림 현상으로 NULL값으로 돌립니다. Key: {0}, string: {1}", (object)keyValuePair.Key, (object)str));
                                    str = (string)null;
                                }
                            }
                            Dictionary<string, int> dictionary3 = keyValuePair.Value;
                            UserCountLog entity = new UserCountLog()
                            {
                                TIMESTAMP = new DateTime?(now),
                                usercount = new int?(dictionary3.ContainsKey("Total") ? dictionary3["Total"] : 0),
                                Wait = dictionary3.ContainsKey("Waiting") ? dictionary3["Waiting"] : 0,
                                Quest = dictionary3.ContainsKey("Quest") ? dictionary3["Quest"] : 0,
                                PVP_PMatch = dictionary3.ContainsKey("PVP_PMatch") ? dictionary3["PVP_PMatch"] : 0,
                                PVP_MMatch = dictionary3.ContainsKey("PVP_MMatch") ? dictionary3["PVP_MMatch"] : 0,
                                PVP_DMatch = dictionary3.ContainsKey("PVP_DMatch") ? dictionary3["PVP_DMatch"] : 0,
                                PVP_GMatch = dictionary3.ContainsKey("PVP_GMatch") ? dictionary3["PVP_GMatch"] : 0,
                                PVP_Arena = dictionary3.ContainsKey("PVP_Arena") ? dictionary3["PVP_Arena"] : 0,
                                PVP_FMatch = dictionary3.ContainsKey("PVP_FMatch") ? dictionary3["PVP_FMatch"] : 0,
                                DS = dictionary3.ContainsKey("DS") ? dictionary3["DS"] : 0,
                                Fish = dictionary3.ContainsKey("Fish") ? dictionary3["Fish"] : 0,
                                Elite = dictionary3.ContainsKey("Elite") ? dictionary3["Elite"] : 0,
                                Tower = dictionary3.ContainsKey("Tower") ? dictionary3["Tower"] : 0,
                                QuestDS = new int?(dictionary3.ContainsKey("QuestDS") ? dictionary3["QuestDS"] : 0),
                                RegionCode = str
                            };
                            Log<AdminClientServicePeer>.Logger.InfoFormat("UserCount Log [Total : {0}] [Waiting : {1}] [Quest : {2}] [RegionCode : {3}]", (object)entity.usercount, (object)entity.Wait, (object)entity.Quest, (object)entity.RegionCode);
                            try
                            {
                                this.parent.logger.UserCountLog.InsertOnSubmit(entity);
                                this.parent.logger.SubmitChanges();
                            }
                            catch (Exception ex)
                            {
                                Log<AdminClientServicePeer>.Logger.Error((object)string.Format("로그를 남기지 못했습니다"), ex);
                            }
                        }
                    }
                    foreach (KeyValuePair<byte, Dictionary<string, int>> keyValuePair in dictionary2)
                    {
                        Dictionary<string, int> dictionary3 = keyValuePair.Value;
                        UserCountLogChanneling entity = new UserCountLogChanneling()
                        {
                            TIMESTAMP = new DateTime?(now),
                            usercount = new int?(dictionary3.ContainsKey("Total") ? dictionary3["Total"] : 0),
                            Wait = dictionary3.ContainsKey("Waiting") ? dictionary3["Waiting"] : 0,
                            Quest = dictionary3.ContainsKey("Quest") ? dictionary3["Quest"] : 0,
                            PVP_PMatch = dictionary3.ContainsKey("PVP_PMatch") ? dictionary3["PVP_PMatch"] : 0,
                            PVP_MMatch = dictionary3.ContainsKey("PVP_MMatch") ? dictionary3["PVP_MMatch"] : 0,
                            PVP_DMatch = dictionary3.ContainsKey("PVP_DMatch") ? dictionary3["PVP_DMatch"] : 0,
                            PVP_GMatch = dictionary3.ContainsKey("PVP_GMatch") ? dictionary3["PVP_GMatch"] : 0,
                            PVP_Arena = dictionary3.ContainsKey("PVP_Arena") ? dictionary3["PVP_Arena"] : 0,
                            PVP_FMatch = dictionary3.ContainsKey("PVP_FMatch") ? dictionary3["PVP_FMatch"] : 0,
                            DS = dictionary3.ContainsKey("DS") ? dictionary3["DS"] : 0,
                            Fish = dictionary3.ContainsKey("Fish") ? dictionary3["Fish"] : 0,
                            Elite = dictionary3.ContainsKey("Elite") ? dictionary3["Elite"] : 0,
                            Tower = dictionary3.ContainsKey("Tower") ? dictionary3["Tower"] : 0,
                            QuestDS = dictionary3.ContainsKey("QuestDS") ? dictionary3["QuestDS"] : 0,
                            RegionCode = "",
                            ChannelingCode = keyValuePair.Key
                        };
                        Log<AdminClientServicePeer>.Logger.InfoFormat("Channel UserCount Log [Total : {0}] [Waiting : {1}] [Quest : {2}] [ChannelCode : {3}]", (object)entity.usercount, (object)entity.Wait, (object)entity.Quest, (object)entity.ChannelingCode);
                        try
                        {
                            this.parent.logger.UserCountLogChanneling.InsertOnSubmit(entity);
                            this.parent.logger.SubmitChanges();
                        }
                        catch (Exception ex)
                        {
                            Log<AdminClientServicePeer>.Logger.Error((object)string.Format("로그를 남기지 못했습니다"), ex);
                        }
                    }
                    Dictionary<string, int> states = dictionary1.TryGetValue<int, Dictionary<string, int>>(0) ?? new Dictionary<string, int>();
                    int sum = states.ContainsKey("Count") ? states["Count"] : 0;
                    int total = states.ContainsKey("Total") ? states["Total"] : 0;
                    int waiting = states.ContainsKey("Waiting") ? states["Waiting"] : 0;
                    foreach (KeyValuePair<string, int> keyValuePair in states)
                        AdminClientServicePeer.userByCategory.Add(keyValuePair.Key, keyValuePair.Value);
                    if (states.ContainsKey("Count"))
                        states.Remove("Count");
                    if (states.ContainsKey("Total"))
                        states.Remove("Total");
                    if (states.ContainsKey("Waiting"))
                        states.Remove("Waiting");
                    if (FeatureMatrix.IsEnable("Channeling_UserCount"))
                    {
                        foreach (KeyValuePair<byte, Dictionary<string, int>> keyValuePair in dictionary2)
                        {
                            int num2 = keyValuePair.Value.ContainsKey("Total") ? keyValuePair.Value["Total"] : 0;
                            switch (keyValuePair.Key)
                            {
                                case 0:
                                    AdminClientServicePeer.userByCategory.Add("Nexon", num2);
                                    continue;
                                case 6:
                                    AdminClientServicePeer.userByCategory.Add("Naver", num2);
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                    if (this.peer == null)
                        this.parent.NotifyClientCount(sum, total, waiting, states);
                    else
                        this.NotifyClientCount(sum, total, waiting, states);
                });
                operationAggregation.OnSync();
            }
        }

        public void NotifyClientCount(int sum, int total, int waiting, Dictionary<string, int> states)
		{
			if (this.peer != null)
			{
				this.peer.Transmit(SerializeWriter.ToBinary<AdminReportClientcountMessage>(new AdminReportClientcountMessage
				{
					Value = sum,
					Total = total,
					Waiting = waiting,
					States = states
				}));
			}
		}

		public void NotifyDSProcessInfo(DSReportMessage message)
		{
			if (this.peer != null)
			{
				this.peer.Transmit(SerializeWriter.ToBinary<DSReportMessage>(message));
			}
		}

		public void PeriodicQuery()
		{
			Scheduler.Schedule(this.parent.Thread, Job.Create(new Action(this.PeriodicQuery)), this.QueryUserCountMilisecond);
			this.QueryUserCount();
		}

		public void PeriodicDSQuery()
		{
			if (!FeatureMatrix.IsEnable("QueryDSInfo"))
			{
				return;
			}
			Scheduler.Schedule(this.parent.Thread, Job.Create(new Action(this.PeriodicDSQuery)), this.QueryDSCountMilisecond);
			this.QueryDSInfo();
		}

        public void PeriodicPartyQuery()
        {
            Scheduler.Schedule(this.parent.Thread, Job.Create(new Action(this.PeriodicPartyQuery)), this.QueryPartyCountMilisecond);
            int[] array = this.parent.PartyServiceIDs();
            if (array.Length == 0)
            {
                return;
            }
            Log<AdminClientServicePeer>.Logger.InfoFormat("Requesting {0} party services.", array.Length);
            OperationAggregation operationAggregation = new OperationAggregation(this.parent);
            QueryPartyCount[] ops = new QueryPartyCount[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                ops[i] = new QueryPartyCount();
                operationAggregation.AddElement("MicroPlayServiceCore.MicroPlayService", new int?(array[i]), ops[i]);
            }
            operationAggregation.OnFinished += delegate (ISynchronizable after)
            {
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                QueryPartyCount[] ops3 = ops;
                for (int j = 0; j < ops.Length; j++)
                {
                    QueryPartyCount queryPartyCount = ops3[j];
                    foreach (KeyValuePair<string, int> current in queryPartyCount.PartyCount)
                    {
                        if (dictionary.ContainsKey(current.Key))
                        {
                            Dictionary<string, int> dictionary2;
                            string key;
                            (dictionary2 = dictionary)[key = current.Key] = dictionary2[key] + current.Value;
                        }
                        else
                        {
                            dictionary.Add(current.Key, current.Value);
                        }
                    }
                }
                this.parent.RequestOperation("MicroPlayServiceCore.MicroPlayService", new LeavePartyCountLog
                {
                    PartyCount = dictionary
                });
            };
            operationAggregation.OnSync();
        }

        public void ReportAdminClientLog(string logstring)
		{
			try
			{
				this.parent.logger.AddAdminClientAccessLog(this.peer.RemoteEndPoint.Address.ToString(), logstring);
			}
			catch (Exception ex)
			{
				Log<AdminClientServicePeer>.Logger.Error(ex);
			}
		}

		public void NotifyStoppedServices(List<string> target)
		{
			if (this.peer != null)
			{
				this.peer.Transmit(SerializeWriter.ToBinary<AdminReportEmergencyStopMessage>(new AdminReportEmergencyStopMessage
				{
					ServiceList = target
				}));
			}
		}

		private readonly int NotifyDelayInMilisecond = 1000;

		private readonly int QueryUserCountMilisecond = 30000;

		private readonly int QueryDSCountMilisecond = 30000;

		private readonly int QueryPartyCountMilisecond = 900000;

		private TcpClient peer;

		private AdminClientService parent;

		private static Dictionary<string, int> userByCategory = new Dictionary<string, int>();

		private static Dictionary<string, int> userByServer = new Dictionary<string, int>();

		private Dictionary<int, Dictionary<int, Dictionary<string, int>>> beforeUserCountResult = new Dictionary<int, Dictionary<int, Dictionary<string, int>>>();

		private static bool isRunningItemFestival = false;
	}
}
