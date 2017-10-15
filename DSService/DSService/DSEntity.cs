using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.DSServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.EndPointNetwork.DS;
using ServiceCore.EndPointNetwork.MicroPlay;
using ServiceCore.EndPointNetwork.Pvp;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.MicroPlayServiceOperations;
using ServiceCore.PartyServiceOperations;
using ServiceCore.PvpServiceOperations;
using ServiceCore.QuestOwnershipServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace DSService
{
	public class DSEntity
	{
		public DSEntity.DSEntityType DSType { get; set; }

		public int DSID { get; set; }

		public int PortIndex { get; set; }

		public IEntity Entity { get; set; }

		public Process Process { get; set; }

		public Devcat.Core.Net.TcpClient TcpClient { get; set; }

		public IEntityProxy PartyConn { get; set; }

		public IEntityProxy MicroPlayConn { get; set; }

		public IEntityProxy FrontendConn { get; set; }

		public string QuestID { get; set; }

		public QuestDigest QuestDigest { get; set; }

		public string ShipName { get; set; }

		public ShipOptionInfo ShipOptionInfo { get; set; }

		public long PartyID { get; set; }

		public long MicroPlayID { get; set; }

		public long FrontendID { get; set; }

		public GameInfo GameInfo { get; set; }

		public bool BlockEntering { get; set; }

		public DateTime GameStartTime { get; set; }

		public bool GameStartComplete { get; set; }

		public bool IsGiantRaid { get; set; }

		public bool IsAdultMode { get; set; }

		public int FPS { get; set; }

		public int QuestStarted { get; set; }

		public int QuestClosed { get; set; }

		public int QuestCrashed { get; set; }

		private string FailReason { get; set; }

		private void CalculateDSEndCount()
		{
			this.DSEndCount++;
			if (this.DSEndCount >= FeatureMatrix.GetInteger("DSMaxEndCount"))
			{
				this.DSEndCount = 0;
			}
		}

		private bool IsDSProcessRestart()
		{
			bool result = this.DSEndCount == 0;
			if (FeatureMatrix.IsEnable("DSForceRestartMode"))
			{
				result = true;
			}
			return result;
		}

		private void TcpClient_Disconnected(object sender, EventArgs e)
		{
			EventArgs<SocketError> eventArgs = e as EventArgs<SocketError>;
			string text = "";
			if (eventArgs != null)
			{
				text += eventArgs.Value;
			}
			if (DSService.Instance.DSWaitingSystem != null)
			{
				text += DSService.Instance.DSWaitingSystem.GetDebugInfo();
			}
			DSLog.AddLog(this, "TcpClientDisconnected", text);
			Log<DSEntity>.Logger.WarnFormat("[DS{0}] Disconnected", this.DSID);
			Scheduler.Schedule(DSService.Instance.Thread, Job.Create(delegate
			{
				if (this.TcpClient != null)
				{
					this.TerminateDS();
				}
			}), 5000);
		}

		private void MicroPlayConn_Closed(object sender, EventArgs e)
		{
			if (this.Process != null)
			{
				this.QuestClosed++;
			}
			DSLog.AddLog(this, "MicroPlayConnClosed", "");
			this.StopDS();
		}

		public void OnOperationFail(Operation op)
		{
			Log<DSEntity>.Logger.ErrorFormat("[DS{0}] OnOperationFail : [{1}]", this.DSID, op);
			DSLog.AddLog(this, "OnOperationFail", op.ToString());
			this.FailReason = op.ToString();
			this.TerminateDS();
		}

		public DSEntity()
		{
			this.BlockEntering = false;
			this.StartPing();
			this.QuestStarted = 0;
			this.QuestClosed = 0;
			this.QuestCrashed = 0;
		}

		public void SendMessage<T>(T message)
		{
			if (this.TcpClient != null)
			{
				Log<DSEntity>.Logger.InfoFormat("[To DS : {0}] {1}", this.DSID, message);
				this.TcpClient.Transmit(SerializeWriter.ToBinary<T>(message));
			}
		}

		public void TerminateDS()
		{
			this.DSEndCount = 0;
			this.FPS = 0;
			switch (this.DSType)
			{
			case DSEntity.DSEntityType.DSQuest:
				this.StopQuestDS();
				return;
			case DSEntity.DSEntityType.DSPvp:
				this.StopPvpDS();
				return;
			default:
				if (this.Process != null)
				{
					this.Process.Kill();
				}
				return;
			}
		}

		public void StopDS()
		{
			this.CalculateDSEndCount();
			this.FPS = 0;
			switch (this.DSType)
			{
			case DSEntity.DSEntityType.DSQuest:
				this.StopQuestDS();
				return;
			case DSEntity.DSEntityType.DSPvp:
				this.StopPvpDS();
				return;
			default:
				if (this.Process != null)
				{
					this.Process.Kill();
				}
				return;
			}
		}

		public void StartQuestDS(string questID, long microPlayID, long partyID, long frontendID, bool isGiantRaid, bool isAdultMode)
		{
			Log<DSEntity>.Logger.InfoFormat("[DS{0}] StartDS : {1}", this.DSID, questID);
			this.DSType = DSEntity.DSEntityType.DSQuest;
			this.QuestID = questID;
			DateTime gameStartTime = DateTime.UtcNow;
			this.GameStartTime = gameStartTime;
			this.GameStartComplete = false;
			this.MicroPlayID = microPlayID;
			this.PartyID = partyID;
			this.FrontendID = frontendID;
			this.IsGiantRaid = isGiantRaid;
			this.IsAdultMode = isAdultMode;
			this.FPS = 0;
			Scheduler.Schedule(DSService.Instance.Thread, Job.Create<DateTime>(delegate(DateTime time)
			{
				if (this.GameStartTime == gameStartTime && !this.GameStartComplete)
				{
					DSLog.AddLog(this, "ForcedStop", "");
					this.TerminateDS();
				}
			}, gameStartTime), 300000);
			DSLog.AddLog(this, "StartDS", "");
			if (this.IsDSProcessRestart())
			{
				if (this.Process != null)
				{
					Log<DSService>.Logger.Error("중복실행입니다. 기존 프로세스를 죽입니다.");
					try
					{
						this.Process.Kill();
						DSLog.AddLog(this, "KillProcess (StartQuestDS)", "");
					}
					catch (Exception ex)
					{
						Log<DSEntity>.Logger.FatalFormat("KillProcess Failed!!\n - {0}", ex);
						DSLog.AddLog(this, "KillProcess Failed", "");
					}
				}
				if (this.TcpClient != null)
				{
					Log<DSService>.Logger.Error("중복접속입니다. 기존 TcpClient를 닫습니다.");
					try
					{
						this.TcpClient.Disconnect();
						DSLog.AddLog(this, "DisconnectTcp", "");
					}
					catch
					{
					}
					this.TcpClient = null;
				}
				try
				{
					this.Process = new Process();
					this.Process.StartInfo.FileName = ServiceCoreSettings.Default.DSExecPath;
					this.Process.StartInfo.Arguments = string.Format("-console -connectip {0} -connectport {1} -port {2} {3} +is_giant_raid {4} -nohltv -owner_pid {5} -owner_dsid {6}", new object[]
					{
						"127.0.0.1",
						DSService.Instance.Port,
						ServiceCoreSettings.Default.DSPort + this.PortIndex,
						ServiceCoreSettings.Default.DSAuxParams,
						this.IsGiantRaid ? 1 : 0,
						Process.GetCurrentProcess().Id,
						this.DSID
					});
					string @string = FeatureMatrix.GetString("DSAuxParamsEx");
					if (@string.Length > 0)
					{
						ProcessStartInfo startInfo = this.Process.StartInfo;
						startInfo.Arguments = startInfo.Arguments + " " + @string;
					}
					if (ServiceCoreSettings.Default.DSRedirectConsole)
					{
						this.Process.StartInfo.RedirectStandardOutput = true;
						this.Process.StartInfo.UseShellExecute = false;
						this.StdOutBuffer = new byte[1024];
						this.IsStdOutRedirected = true;
					}
					this.Process.EnableRaisingEvents = true;
					this.Process.Exited += delegate(object s, EventArgs e)
					{
						this.CompleteStdOut();
						DSService.Instance.Thread.Enqueue(Job.Create(delegate
						{
							Process process = s as Process;
							string arg = process.ExitCode.ToString();
							DSLog.AddLog(this, "ProcessExited", arg);
							if (process.ExitCode >= 100 && process.ExitCode <= 110)
							{
								this.QuestCrashed++;
							}
							if (FeatureMatrix.IsEnable("DSDynamicLoad"))
							{
								UpdateDSShipInfo op = new UpdateDSShipInfo(DSService.Instance.ID, this.DSID, 0L, UpdateDSShipInfo.CommandEnum.DSClosed, this.FailReason);
								DSService.RequestDSBossOperation(op);
							}
							else
							{
								DSService.RequestDSBossOperation(new UpdateDSShipInfo(this.DSID, 0L, UpdateDSShipInfo.CommandEnum.DSFinished, this.FailReason));
							}
							this.Process = null;
							this.TerminateDS();
							Log<DSService>.Logger.Info("프로세스 종료됐음");
							this.FailReason = null;
						}));
					};
					this.Process.Start();
					if (this.IsStdOutRedirected)
					{
						try
						{
							this.StdOutAsyncResult = this.Process.StandardOutput.BaseStream.BeginRead(this.StdOutBuffer, 0, this.StdOutBuffer.Length, new AsyncCallback(this.StdOutAsync), null);
						}
						catch (Exception ex2)
						{
							Log<DSEntity>.Logger.Error("Exception occurred in StartQuestDS(BeginRead): ", ex2);
						}
					}
					this.Process.PriorityClass = ProcessPriorityClass.AboveNormal;
					this.ResetPing();
					this.DSEndCount = 0;
					DSLog.AddLog(this, "StartProcess", "");
					this.QuestStarted++;
					return;
				}
				catch (Exception ex3)
				{
					Log<DSService>.Logger.Error("에러발생!", ex3);
					return;
				}
			}
			try
			{
				this.InitializeServer();
			}
			catch (Exception ex4)
			{
				Log<DSEntity>.Logger.FatalFormat("InitializeServer Failed!!\n - {0}", ex4);
				DSLog.AddLog(this, "InitializeServer Failed", "");
			}
		}

		private void StdOutAsync(IAsyncResult ar)
		{
			int num = 0;
			try
			{
				if (this.StdOutAsyncResult != null)
				{
					num = this.Process.StandardOutput.BaseStream.EndRead(this.StdOutAsyncResult);
				}
			}
			catch (InvalidOperationException)
			{
				return;
			}
			catch (Exception ex)
			{
				Log<DSService>.Logger.Info("StdOutAsync", ex);
				this.StdOutBufferIdx = 0;
			}
			this.StdOutAsyncResult = null;
			if (num != 0)
			{
				this.ProcessStdOut(num + this.StdOutBufferIdx);
				try
				{
					this.StdOutAsyncResult = this.Process.StandardOutput.BaseStream.BeginRead(this.StdOutBuffer, this.StdOutBufferIdx, this.StdOutBuffer.Length - this.StdOutBufferIdx, new AsyncCallback(this.StdOutAsync), null);
				}
				catch (Exception ex2)
				{
					this.StdOutAsyncResult = null;
					Log<DSService>.Logger.Info("StdOutAsync", ex2);
				}
				return;
			}
		}

		private void ProcessStdOut(int totalRead)
		{
			if (totalRead == 0)
			{
				return;
			}
			try
			{
				char[] array = new char[this.StdOutBuffer.Length];
				int num = 0;
				for (int i = 0; i < totalRead; i++)
				{
					if (this.StdOutBuffer[i] == 10)
					{
						int num2;
						if (i > 0 && this.StdOutBuffer[i - 1] == 13)
						{
							num2 = i - 1;
						}
						else
						{
							num2 = i;
						}
						int chars = Encoding.ASCII.GetChars(this.StdOutBuffer, num, num2 - num, array, 0);
						string arg = new string(array, 0, chars);
						Console.WriteLine("DSLog [{0}] {1}", this.Process.Id, arg);
						num = i + 1;
					}
				}
				if (num == 0 && totalRead == this.StdOutBuffer.Length)
				{
					string @string = Encoding.ASCII.GetString(this.StdOutBuffer);
					Console.WriteLine("DSLog [{0}] {1}", this.Process.Id, @string);
				}
				this.StdOutBufferIdx = totalRead - num;
				if (this.StdOutBufferIdx > 0 && num > 0)
				{
					Buffer.BlockCopy(this.StdOutBuffer, num, this.StdOutBuffer, 0, this.StdOutBufferIdx);
				}
			}
			catch (Exception ex)
			{
				Log<DSService>.Logger.Info("ProcessStdOut", ex);
				this.StdOutBufferIdx = 0;
			}
		}

		private void CompleteStdOut()
		{
			if (!this.IsStdOutRedirected)
			{
				return;
			}
			for (;;)
			{
				int num = 0;
				try
				{
					if (this.StdOutAsyncResult != null && !this.StdOutAsyncResult.IsCompleted)
					{
						num = this.Process.StandardOutput.BaseStream.EndRead(this.StdOutAsyncResult);
					}
				}
				catch (InvalidOperationException)
				{
					return;
				}
				catch
				{
					this.StdOutBufferIdx = 0;
				}
				if (num == 0)
				{
					break;
				}
				this.ProcessStdOut(num + this.StdOutBufferIdx);
			}
		}

		private void StopQuestDS()
		{
			DSLog.AddLog(this, "StopQuestDS", "");
			this.FPS = 0;
			if (this.IsDSProcessRestart())
			{
				if (this.Process == null)
				{
					goto IL_AB;
				}
				try
				{
					if (!this.Process.HasExited)
					{
						Process process = this.Process;
						this.Process = null;
						process.Kill();
						DSLog.AddLog(this, "KillProcess (StopQuestDS)", "");
					}
					goto IL_AB;
				}
				catch (Exception ex)
				{
					Log<DSEntity>.Logger.FatalFormat("KillProcess Failed!!\n - {0}", ex);
					DSLog.AddLog(this, "KillProcess Failed", "");
					goto IL_AB;
				}
			}
			DSService.Instance.Thread.Enqueue(Job.Create(delegate
			{
				if (this.Process != null && !this.Process.HasExited)
				{
					if (FeatureMatrix.IsEnable("DSDynamicLoad"))
					{
						UpdateDSShipInfo op = new UpdateDSShipInfo(DSService.Instance.ID, this.DSID, 0L, UpdateDSShipInfo.CommandEnum.DSFinished, this.FailReason);
						DSService.RequestDSBossOperation(op);
					}
					else
					{
						DSService.RequestDSBossOperation(new UpdateDSShipInfo(this.DSID, 0L, UpdateDSShipInfo.CommandEnum.DSFinished, this.FailReason));
					}
				}
				this.FailReason = null;
			}));
			IL_AB:
			if (this.PartyConn != null)
			{
				IEntityProxy partyConn = this.PartyConn;
				this.PartyConn = null;
				partyConn.Close(true);
				DSLog.AddLog(this, "ClosePartyConn", "");
			}
			if (this.MicroPlayConn != null)
			{
				IEntityProxy microPlayConn = this.MicroPlayConn;
				this.MicroPlayConn = null;
				microPlayConn.Close(true);
				DSLog.AddLog(this, "CloseMicroPlayConn", "");
			}
			if (this.FrontendConn != null)
			{
				IEntityProxy frontendConn = this.FrontendConn;
				this.FrontendConn = null;
				frontendConn.Close(true);
				DSLog.AddLog(this, "CloseFrontendConn", "");
			}
			if (this.TcpClient != null && this.IsDSProcessRestart())
			{
				this.TcpClient.Disconnected -= this.TcpClient_Disconnected;
				this.TcpClient = null;
			}
			this.QuestID = null;
			this.PartyID = -1L;
			this.MicroPlayID = -1L;
			this.FrontendID = -1L;
			this.BlockEntering = false;
			this.GameStartTime = DateTime.UtcNow;
			this.GameStartComplete = false;
		}

		public void RegisterConnection(Devcat.Core.Net.TcpClient tcpClient)
		{
			Log<DSEntity>.Logger.WarnFormat("[DS{0}] RegisterConnection", this.DSID);
			this.TcpClient = tcpClient;
			DSLog.AddLog(this, "Connected", "");
			Log<DSEntity>.Logger.WarnFormat("[DS{0}] ConnectionSucceed", this.DSID);
			object typeConverter = DSService.Instance.MessageHandlerFactory.GetTypeConverter();
			this.TcpClient.Transmit(SerializeWriter.ToBinary(typeConverter));
			this.SyncFeatureMatrix();
			this.InitializeServer();
			this.TcpClient.Disconnected += this.TcpClient_Disconnected;
			this.TcpClient.ExceptionOccur += delegate(object s, EventArgs<Exception> e)
			{
				DSLog.AddLog(this, "Exception", e.Value.Message.Substring(0, 128));
				Log<DSEntity>.Logger.ErrorFormat("[DS{0}] ExceptionOccur", this.DSID);
				Log<DSEntity>.Logger.Error("ExceptionOccur", e.Value);
			};
			this.TcpClient.ConnectionFail += delegate(object s, EventArgs<Exception> e)
			{
				DSLog.AddLog(this, "Disconnected", "");
				Log<DSEntity>.Logger.ErrorFormat("[DS{0}] ConnectionFail", this.DSID);
			};
			this.TcpClient.PacketReceive += delegate(object s, EventArgs<ArraySegment<byte>> e)
			{
				Packet packet = new Packet(e.Value);
				DSService.Instance.MessageHandlerFactory.Handle(packet, this);
			};
		}

		public void ConnectToMicroPlay()
		{
			Log<DSEntity>.Logger.WarnFormat("[DS{0}] ConnectToMicroPlay : [{0}]", this.DSID, this.MicroPlayID);
			this.MicroPlayConn = this.PartyConn;
			this.MicroPlayConn.Closed += new EventHandler<EventArgs<IEntityProxy>>(this.MicroPlayConn_Closed);
		}

		public void ConnectToParty()
		{
			Log<DSEntity>.Logger.WarnFormat("[DS{0}] ConnectToParty : [{0}]", this.DSID, this.PartyID);
			this.PartyConn = DSService.Instance.Connect(this.Entity, new Location(this.PartyID, "MicroPlayServiceCore.MicroPlayService"));
		}

		public void ConnectToFrontend()
		{
			Log<DSEntity>.Logger.WarnFormat("[DS{0}] ConnectToFrontend : [{0}]", this.DSID, this.FrontendID);
			this.FrontendConn = DSService.Instance.Connect(this.Entity, new Location(this.FrontendID, "FrontendServiceCore.FrontendService"));
		}

		public void SyncFeatureMatrix()
		{
			if (FeatureMatrix.GetFeatureMatrixDic().Count > 0)
			{
				SyncFeatureMatrixMessage message = new SyncFeatureMatrixMessage(FeatureMatrix.GetFeatureMatrixDic());
				this.SendMessage<SyncFeatureMatrixMessage>(message);
			}
		}

		public void InitializeServer()
		{
			if (this.QuestID == null)
			{
				Log<DSEntity>.Logger.WarnFormat("[DS{0}] change questID To quest_dragon_ancient_elculous", this.DSID);
				this.QuestID = "quest_dragon_ancient_elculous";
			}
			DSLog.AddLog(this, "InitializeServer", "");
			if (this.IsGiantRaid)
			{
				this.QueryQuestDigest();
				return;
			}
			this.ConnectToParty();
			this.ConnectToMicroPlay();
			this.ConnectToFrontend();
			this.SendMessage<LaunchShipGrantedMessage>(new LaunchShipGrantedMessage(this.QuestID, this.IsAdultMode ? (byte)1 : (byte)0, 0, null));
			DSLog.AddLog(this, "LaunchShipGranted", "");
			Log<DSEntity>.Logger.WarnFormat("Waiting RegisterServerMessage....", new object[0]);
		}

		private void QueryQuestDigest()
		{
			DSLog.AddLog(this, "QueryQuestDigest", "");
			QueryQuestDigestDS digest_op = new QueryQuestDigestDS(this.QuestID);
			digest_op.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("[DS{0}] QueryQuestDigestDS Complete [ {1} ]", this.DSID, 0, this.QuestID);
				this.QuestDigest = digest_op.QuestDigest;
				this.OpenParty();
			};
			digest_op.OnFail += this.OnOperationFail;
			DSService.Instance.RequestOperation("PlayerService.PlayerService", digest_op);
		}

		private void OpenParty()
		{
			DSLog.AddLog(this, "OpenParty", "");
			this.ShipName = this.QuestID;
			this.ShipOptionInfo = new ShipOptionInfo(30, 30, -1, 0, 100, 0, 0, 2);
			OpenParty open_op = new OpenParty(this.ShipName, this.QuestDigest, this.ShipOptionInfo, false, null);
			open_op.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("[DS{0}] OpenParty Complete [ {1} ]", this.DSID, open_op.PartyID);
				this.PartyID = open_op.PartyID;
				this.ConnectToParty();
				this.ShipLaunching();
			};
			open_op.OnFail += this.OnOperationFail;
			DSService.Instance.RequestOperation("MicroPlayServiceCore.MicroPlayService", open_op);
		}

		private void ShipLaunching()
		{
			DSLog.AddLog(this, "ShipLaunching", "");
			ShipLaunching shipLaunching = new ShipLaunching
			{
				HostCID = -1L,
				MasterCID = -1L,
				ShipName = this.ShipName,
				IsGiantRaid = true,
				QuestDigest = this.QuestDigest,
				Option = this.ShipOptionInfo
			};
			shipLaunching.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("[DS{0}] ShipLaunching Complete", this.DSID);
				this.StartMicroPlay();
			};
			shipLaunching.OnFail += this.OnOperationFail;
			this.PartyConn.RequestOperation(shipLaunching);
		}

		private void StartMicroPlay()
		{
			DSLog.AddLog(this, "StartMicroPlay", "");
			StartMicroPlay2 startMicroPlay = new StartMicroPlay2
			{
				QuestDigest = this.QuestDigest,
				IsDS = true
			};
			startMicroPlay.OnComplete += delegate(Operation __)
			{
				Log<DSEntity>.Logger.WarnFormat("[DS{0}] StartMicroPlay2 Complete ", this.DSID);
				Log<DSEntity>.Logger.WarnFormat("BindTarget Complete", new object[0]);
				this.MicroPlayID = this.PartyID;
				this.ConnectToMicroPlay();
				this.SendMessage<LaunchShipGrantedMessage>(new LaunchShipGrantedMessage(this.QuestID, this.IsAdultMode ? (byte)1 : (byte)0, 0, null));
				Log<DSEntity>.Logger.WarnFormat("Waiting RegisterServerMessage....", new object[0]);
				DSLog.AddLog(this, "LaunchShipGranted", "");
			};
			startMicroPlay.OnFail += this.OnOperationFail;
			this.PartyConn.RequestOperation(startMicroPlay);
		}

		public void BindTarget()
		{
			DSLog.AddLog(this, "BindTarget", "");
			BindTarget bindTarget = new BindTarget
			{
				Target = this.QuestDigest
			};
			bindTarget.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("BindTarget Complete", new object[0]);
				this.SendMessage<LaunchShipGrantedMessage>(new LaunchShipGrantedMessage(this.QuestID, this.IsAdultMode ? (byte)1 : (byte)0, 0, null));
				Log<DSEntity>.Logger.WarnFormat("Waiting RegisterServerMessage....", new object[0]);
				DSLog.AddLog(this, "LaunchShipGranted", "");
			};
			bindTarget.OnFail += this.OnOperationFail;
			this.MicroPlayConn.RequestOperation(bindTarget);
		}

		public void RegisterServer(GameInfo gameInfo)
		{
			DSLog.AddLog(this, "RegisterServer", "");
			gameInfo.DSIP = DSService.MyIP;
			this.GameInfo = gameInfo;
			if (this.IsGiantRaid)
			{
				this.ShipLaunched();
				return;
			}
			this.TransferHostToDS();
		}

		private void ShipLaunched()
		{
			ShipLaunched shipLaunched = new ShipLaunched
			{
				HostCID = -1L,
				MicroPlayID = this.MicroPlayConn.RemoteID,
				QuestLevel = this.QuestDigest.QuestLevel
			};
			shipLaunched.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("ShipLaunched Complete", new object[0]);
				this.HostStarted();
			};
			shipLaunched.OnFail += this.OnOperationFail;
			this.PartyConn.RequestOperation(shipLaunched);
		}

		private void TransferHostToDS()
		{
			TransferHostToDS transferHostToDS = new TransferHostToDS
			{
				HostID = this.Entity.ID,
				DSID = this.DSID,
				HostInfo = this.GameInfo
			};
			transferHostToDS.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("TransferHostToDS Complete", new object[0]);
				this.QueryMicroPlayInfo();
			};
			transferHostToDS.OnFail += this.OnOperationFail;
			this.MicroPlayConn.RequestOperation(transferHostToDS);
		}

		private void HostStarted()
		{
			HostStarted hostStarted = new HostStarted
			{
				DSID = this.DSID,
				HostID = this.Entity.ID,
				HostInfo = this.GameInfo
			};
			hostStarted.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("HostStarted Complete", new object[0]);
				this.GameStarting();
			};
			hostStarted.OnFail += this.OnOperationFail;
			this.MicroPlayConn.RequestOperation(hostStarted);
		}

		private void GameStarting()
		{
			GameStarting gameStarting = new GameStarting();
			gameStarting.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("GameStarting Complete", new object[0]);
				this.QueryMicroPlayInfo();
			};
			gameStarting.OnFail += this.OnOperationFail;
			this.PartyConn.RequestOperation(gameStarting);
		}

		private void QueryMicroPlayInfo()
		{
			QueryMicroPlayInfo microplayInfo_op = new QueryMicroPlayInfo
			{
				InitItemDropEntities = true
			};
			microplayInfo_op.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("QueryMicroPlayInfo Complete", new object[0]);
				GameStartGrantedMessage gameStartGrantedMessage = new GameStartGrantedMessage
				{
					QuestSectorInfos = microplayInfo_op.QuestSectorInfo,
					QuestLevel = microplayInfo_op.QuestLevel,
					QuestTime = microplayInfo_op.TimeLimit,
					HardMode = (microplayInfo_op.HardMode ? 1 : 0),
					SoloSector = microplayInfo_op.SoloSector,
					IsHuntingQuest = microplayInfo_op.IsHuntingQuest,
					InitGameTime = microplayInfo_op.InitGameTime,
					SectorMoveGameTime = microplayInfo_op.SectorMoveGameTime,
					Difficulty = microplayInfo_op.Difficulty,
					IsTimerDecreasing = microplayInfo_op.IsTimerDecreasing,
					QuestStartedPlayerCount = microplayInfo_op.QuestStartedPlayerCount,
					NewSlot = -1,
					NewKey = -1,
					IsUserDedicated = false
				};
				this.SendMessage<GameStartGrantedMessage>(gameStartGrantedMessage);
				if (this.FrontendConn != null && !this.IsGiantRaid)
				{
					this.FrontendConn.RequestOperation(SendPacket.Create<GameStartGrantedMessage>(gameStartGrantedMessage));
				}
				DSLog.AddLog(this, "GameStartGranted", "");
				this.GameStarted();
			};
			microplayInfo_op.OnFail += this.OnOperationFail;
			this.MicroPlayConn.RequestOperation(microplayInfo_op);
		}

		private void GameStarted()
		{
			GameStarted gameStarted = new GameStarted();
			gameStarted.OnComplete += delegate(Operation _)
			{
				Log<DSEntity>.Logger.WarnFormat("GameStarted Complete", new object[0]);
				DSLog.AddLog(this, "GameStarted", "");
				this.GameStartComplete = true;
				DSService.RequestDSBossOperation(new UpdateDSShipInfo(this.DSID, this.PartyID, UpdateDSShipInfo.CommandEnum.LaunchComplete, ""));
			};
			gameStarted.OnFail += this.OnOperationFail;
			this.PartyConn.RequestOperation(gameStarted);
		}

		public int PingID { get; set; }

		public int LastPongID { get; set; }

		public void StartPing()
		{
			this.ResetPing();
			Scheduler.Schedule(JobProcessor.Current, Job.Create(new Action(this.SendPing)), 60000);
		}

		public void ResetPing()
		{
			this.LastPongID = 0;
			this.PingID = 0;
		}

		public void SendPing()
		{
			if (this.Process != null)
			{
				if (this.LastPongID + 10 < this.PingID)
				{
					Log<DSEntity>.Logger.ErrorFormat("Ping {0} received no respond. terminate : LastPong {1}", this.PingID, this.LastPongID);
					DSLog.AddLog(this, string.Format("Ping {0} received no respond. terminate : LastPong {1}", this.PingID, this.LastPongID), "");
					this.TerminateDS();
				}
				else
				{
					this.SendMessage<PingMessage>(new PingMessage(++this.PingID));
				}
			}
			Scheduler.Schedule(JobProcessor.Current, Job.Create(new Action(this.SendPing)), 30000);
		}

		public void PongReceived(int pongID)
		{
			this.LastPongID = pongID;
		}

		public void ProcessCommand(DSCommandType type, string command)
		{
			DSLog.AddLog(this, "ProcessCommand", type.ToString());
			if (type == DSCommandType.StopDS)
			{
				this.StopDS();
				return;
			}
			if (type == DSCommandType.ServerStarted)
			{
				return;
			}
			int fps;
			if (type == DSCommandType.StartFailTimer)
			{
				if (this.MicroPlayConn != null)
				{
					StartFailTimer startFailTimer = new StartFailTimer();
					startFailTimer.OnComplete += delegate(Operation _)
					{
						DSService.RequestDSBossOperation(new UpdateDSShipInfo(this.DSID, this.PartyID, UpdateDSShipInfo.CommandEnum.StartGame, ""));
					};
					this.MicroPlayConn.RequestOperation(startFailTimer);
					return;
				}
			}
			else if (type == DSCommandType.BlockEntering)
			{
				if (this.PartyConn != null)
				{
					this.PartyConn.RequestOperation(new BlockEntering());
					Log<DSService>.Logger.Warn("입장 막음");
					DSService.RequestDSBossOperation(new UpdateDSShipInfo(this.DSID, this.PartyID, UpdateDSShipInfo.CommandEnum.BlockEntering, ""));
					this.BlockEntering = true;
					return;
				}
			}
			else if (type == DSCommandType.ReportFps && int.TryParse(command, out fps))
			{
				this.FPS = fps;
			}
		}

		public void ProcessMessage(object message)
		{
			Log<DSEntity>.Logger.InfoFormat("[From DS {0}] {1}", this.DSID, message);
			if (this.PvpConfig != null)
			{
				this.ProcessMessage_Pvp(message);
				return;
			}
			this.ProcessMessage_Raid(message);
		}

		public void ProcessMessage_Raid(object message)
		{
			if (message is RegisterServerMessage)
			{
				RegisterServerMessage registerServerMessage = message as RegisterServerMessage;
				this.RegisterServer(registerServerMessage.TheInfo);
				return;
			}
			if (message is DSCommandMessage)
			{
				DSCommandMessage dscommandMessage = message as DSCommandMessage;
				this.ProcessCommand(dscommandMessage.DSCommandType, dscommandMessage.Command);
				return;
			}
			if (message is PongMessage)
			{
				PongMessage pongMessage = message as PongMessage;
				this.PongReceived(pongMessage.Tag);
				return;
			}
			if (message is UseConsumableMessage)
			{
				UseConsumableMessage useConsumableMessage = message as UseConsumableMessage;
				if (this.MicroPlayConn != null)
				{
					UseConsumable op19 = new UseConsumable(this.Entity.ID, useConsumableMessage.Tag, useConsumableMessage.Part, useConsumableMessage.Slot);
					this.MicroPlayConn.RequestOperation(op19);
					return;
				}
			}
			else if (message is QueryAddBattleInventoryMessage)
			{
				QueryAddBattleInventoryMessage queryAddBattleInventoryMessage = message as QueryAddBattleInventoryMessage;
				if (this.MicroPlayConn != null)
				{
					AddBattleInventory op2 = new AddBattleInventory
					{
						HostCID = this.Entity.ID,
						Owner = queryAddBattleInventoryMessage.Tag,
						SlotNum = queryAddBattleInventoryMessage.SlotNum,
						IsFree = queryAddBattleInventoryMessage.IsFree
					};
					this.MicroPlayConn.RequestOperation(op2);
					return;
				}
			}
			else if (message is MoveToNextSectorMessage)
			{
				MoveToNextSectorMessage moveToNextSectorMessage = message as MoveToNextSectorMessage;
				if (this.MicroPlayConn != null)
				{
					MoveSector op3 = new MoveSector(moveToNextSectorMessage.TriggerName, moveToNextSectorMessage.TargetGroup, false);
					this.MicroPlayConn.RequestOperation(op3);
					return;
				}
			}
			else
			{
				if (message is PetKilledMessage)
				{
					PetKilledMessage petKilledMessage = message as PetKilledMessage;
					PetKilled op4 = new PetKilled(this.Entity.ID, (long)petKilledMessage.Tag, petKilledMessage.PetID);
					this.MicroPlayConn.RequestOperation(op4);
					return;
				}
				if (message is PetRevivedMessage)
				{
					PetRevivedMessage petRevivedMessage = message as PetRevivedMessage;
					PetRevived op5 = new PetRevived(this.Entity.ID, petRevivedMessage.CasterTag, petRevivedMessage.ReviverTag, petRevivedMessage.PetID, petRevivedMessage.Method);
					this.MicroPlayConn.RequestOperation(op5);
					return;
				}
				if (message is PlayerKilledMessage)
				{
					PlayerKilledMessage playerKilledMessage = message as PlayerKilledMessage;
					if (this.MicroPlayConn != null)
					{
						PlayerKilled op6 = new PlayerKilled(this.Entity.ID, (long)playerKilledMessage.Tag);
						this.MicroPlayConn.RequestOperation(op6);
						return;
					}
				}
				else if (message is PlayerRevivedMessage)
				{
					PlayerRevivedMessage playerRevivedMessage = message as PlayerRevivedMessage;
					if (this.MicroPlayConn != null)
					{
						PlayerRevived op7 = new PlayerRevived(this.Entity.ID, playerRevivedMessage.CasterTag, playerRevivedMessage.ReviverTag, playerRevivedMessage.Method);
						this.MicroPlayConn.RequestOperation(op7);
						return;
					}
				}
				else if (message is MonsterKilledMessage)
				{
					MonsterKilledMessage monsterKilledMessage = message as MonsterKilledMessage;
					if (this.MicroPlayConn != null)
					{
						MonsterKilled monsterKilled = new MonsterKilled
						{
							MasterCID = this.Entity.ID,
							Attacker = monsterKilledMessage.Attacker,
							Target = monsterKilledMessage.Target,
							Action = monsterKilledMessage.ActionType,
							HasEvilCore = monsterKilledMessage.HasEvilCore,
							Damage = monsterKilledMessage.Damage,
							PositionX = monsterKilledMessage.DamagePositionX,
							PositionY = monsterKilledMessage.DamagePositionY,
							Distance = monsterKilledMessage.Distance,
							IgnoreCheatCheck = !FeatureMatrix.IsEnable("MDRSystem_DS")
						};
						monsterKilled.OnFail += delegate(Operation _)
						{
							Log<DSEntity>.Logger.ErrorFormat("MonsterKilled 오퍼레이션 실패. 경험치를 받지 못합니다", new object[0]);
						};
						this.MicroPlayConn.RequestOperation(monsterKilled);
						return;
					}
				}
				else if (message is RagdollKickedMessage)
				{
					RagdollKickedMessage ragdollKickedMessage = message as RagdollKickedMessage;
					if (this.MicroPlayConn != null)
					{
						RagdollKicked op8 = new RagdollKicked(ragdollKickedMessage.Tag, ragdollKickedMessage.TargetEntityName, ragdollKickedMessage.EvilCoreType, ragdollKickedMessage.IsRareCore);
						this.MicroPlayConn.RequestOperation(op8);
						return;
					}
				}
				else if (message is SectorPropListMessage)
				{
					if (this.MicroPlayConn != null)
					{
						SectorPropListMessage sectorPropListMessage = message as SectorPropListMessage;
						ReportSectorProps op9 = new ReportSectorProps(this.Entity.ID, sectorPropListMessage.Props);
						this.MicroPlayConn.RequestOperation(op9);
						return;
					}
				}
				else if (message is PropBrokenMessage)
				{
					if (this.MicroPlayConn != null)
					{
						PropBrokenMessage propBrokenMessage = message as PropBrokenMessage;
						PropBroken op10 = new PropBroken(this.Entity.ID, propBrokenMessage.BrokenProp, propBrokenMessage.EntityName, propBrokenMessage.Attacker);
						this.MicroPlayConn.RequestOperation(op10);
						return;
					}
				}
				else if (message is PickErgMessage)
				{
					if (this.MicroPlayConn != null)
					{
						PickErgMessage pickMessage = message as PickErgMessage;
						PickErg pickErg = new PickErg(this.Entity.ID, pickMessage.ParentProp);
						pickErg.OnComplete += delegate(Operation op)
						{
							PutErgMessage message2 = new PutErgMessage(pickMessage.ParentProp, (op as PickErg).PickedPlayerTag);
							this.SendMessage<PutErgMessage>(message2);
						};
						this.MicroPlayConn.RequestOperation(pickErg);
						return;
					}
				}
				else if (message is ArmorBrokenMessage)
				{
					if (this.MicroPlayConn != null)
					{
						ArmorBrokenMessage armorBrokenMessage = message as ArmorBrokenMessage;
						Log<DSEntity>.Logger.Info(armorBrokenMessage.ToString());
						this.MicroPlayConn.RequestOperation(new ArmorBroken
						{
							HostCID = this.Entity.ID,
							Owner = armorBrokenMessage.Owner,
							CostumePart = armorBrokenMessage.Part
						});
						return;
					}
				}
				else if (message is DestroyMicroPlayContentsMessage)
				{
					if (this.MicroPlayConn != null)
					{
						DestroyMicroPlayContentsMessage destroyMicroPlayContentsMessage = message as DestroyMicroPlayContentsMessage;
						Log<DSEntity>.Logger.Info(destroyMicroPlayContentsMessage.ToString());
						this.MicroPlayConn.RequestOperation(new DestroyMicroPlayContent
						{
							EntityID = destroyMicroPlayContentsMessage.EntityID
						});
						return;
					}
				}
				else if (message is StartAutoFishingMessage)
				{
					if (this.MicroPlayConn != null)
					{
						StartAutoFishingMessage startAutoFishingMessage = message as StartAutoFishingMessage;
						Log<DSEntity>.Logger.Info(startAutoFishingMessage.ToString());
						this.MicroPlayConn.RequestOperation(new StartAutoFishing
						{
							SerialNumber = startAutoFishingMessage.SerialNumber,
							Tag = startAutoFishingMessage.Tag,
							Argument = startAutoFishingMessage.Argument
						});
						return;
					}
				}
				else if (message is CancelAutoFishingMessage)
				{
					if (this.MicroPlayConn != null)
					{
						CancelAutoFishingMessage cancelAutoFishingMessage = message as CancelAutoFishingMessage;
						Log<DSEntity>.Logger.Info(cancelAutoFishingMessage.ToString());
						this.MicroPlayConn.RequestOperation(new CancelAutoFishing
						{
							SerialNumber = cancelAutoFishingMessage.SerialNumber,
							Tag = cancelAutoFishingMessage.Tag
						});
						return;
					}
				}
				else if (message is CatchAutoFishMessage)
				{
					if (this.MicroPlayConn != null)
					{
						CatchAutoFishMessage catchAutoFishMessage = message as CatchAutoFishMessage;
						Log<DSEntity>.Logger.Info(catchAutoFishMessage.ToString());
						this.MicroPlayConn.RequestOperation(new CatchAutoFish
						{
							SerialNumber = catchAutoFishMessage.SerialNumber,
							Tag = catchAutoFishMessage.Tag,
							Time = catchAutoFishMessage.CatchTimeInSeconds
						});
						return;
					}
				}
				else if (message is MicroPlayEventMessage)
				{
					if (this.MicroPlayConn != null)
					{
						MicroPlayEventMessage microPlayEventMessage = message as MicroPlayEventMessage;
						InformMicroPlayEvent op11 = new InformMicroPlayEvent
						{
							HostCID = this.Entity.ID,
							Slot = microPlayEventMessage.Slot,
							EventString = microPlayEventMessage.EventString
						};
						this.MicroPlayConn.RequestOperation(op11);
						return;
					}
				}
				else if (message is MonsterDamageReportMessage)
				{
					if (FeatureMatrix.IsEnable("MDRSystem_V1"))
					{
						MonsterDamageReportMessage monsterDamageReportMessage = message as MonsterDamageReportMessage;
						MonsterDamageReport monsterDamageReport = new MonsterDamageReport
						{
							Target = monsterDamageReportMessage.Target,
							TakeDamageList = monsterDamageReportMessage.TakeDamageList,
							Time = DateTime.Now
						};
						monsterDamageReport.OnFail += delegate(Operation _)
						{
							Log<DSEntity>.Logger.InfoFormat("<MonsterDamageReportMessage> MonsterDamageReport 오퍼레이션 실패.", new object[0]);
						};
						this.MicroPlayConn.RequestOperation(monsterDamageReport);
						return;
					}
				}
				else if (message is CombatRecordMessage)
				{
					if (this.MicroPlayConn != null)
					{
						CombatRecordMessage combatRecordMessage = message as CombatRecordMessage;
						InformCombatRecord op12 = new InformCombatRecord
						{
							PlayerNum = combatRecordMessage.PlayerNumber,
							ComboMax = combatRecordMessage.ComboMax,
							HitMax = combatRecordMessage.HitMax,
							StyleMax = combatRecordMessage.StyleMax,
							Death = combatRecordMessage.Death,
							Kill = combatRecordMessage.Kill,
							BattleAchieve = combatRecordMessage.BattleAchieve,
							HitTake = combatRecordMessage.HitTake,
							StyleCount = combatRecordMessage.StyleCount,
							RankStyle = combatRecordMessage.RankStyle,
							RankBattle = combatRecordMessage.RankBattle,
							RankTotal = combatRecordMessage.RankTotal
						};
						this.MicroPlayConn.RequestOperation(op12);
						return;
					}
				}
				else if (message is AddStatusEffect)
				{
					if (this.MicroPlayConn != null)
					{
						AddStatusEffect addStatusEffect = message as AddStatusEffect;
						AddMemberStatusEffect op13 = new AddMemberStatusEffect
						{
							Tag = addStatusEffect.Tag,
							Type = addStatusEffect.Type,
							Level = addStatusEffect.Level,
							RemainTimeSec = addStatusEffect.TimeSec
						};
						this.MicroPlayConn.RequestOperation(op13);
						return;
					}
				}
				else if (message is VocationTransformMessage)
				{
					if (this.MicroPlayConn != null)
					{
						VocationTransformMessage vocationTransformMessage = message as VocationTransformMessage;
						MicroPlayVocationTransform op14 = new MicroPlayVocationTransform(vocationTransformMessage.SlotNum, vocationTransformMessage.TransformLevel);
						this.MicroPlayConn.RequestOperation(op14);
						return;
					}
				}
				else if (message is VocationTransformFinishedMessage)
				{
					if (this.MicroPlayConn != null)
					{
						VocationTransformFinishedMessage vocationTransformFinishedMessage = message as VocationTransformFinishedMessage;
						MicroPlayVocationTransformFinished op15 = new MicroPlayVocationTransformFinished(vocationTransformFinishedMessage.SlotNum, vocationTransformFinishedMessage.TotalDamage);
						this.MicroPlayConn.RequestOperation(op15);
						return;
					}
				}
				else if (message is UseConsumableMessage)
				{
					if (this.MicroPlayConn != null)
					{
						UseConsumableMessage useConsumableMessage2 = message as UseConsumableMessage;
						UseConsumable op16 = new UseConsumable(this.Entity.ID, useConsumableMessage2.Tag, useConsumableMessage2.Part, useConsumableMessage2.Slot);
						this.MicroPlayConn.RequestOperation(op16);
						return;
					}
				}
				else if (message is FreeMatchReportMessage)
				{
					if (this.MicroPlayConn != null)
					{
						FreeMatchReportMessage freeMatchReportMessage = message as FreeMatchReportMessage;
						FreeMatchReport op17 = new FreeMatchReport(freeMatchReportMessage.WinnerTag, freeMatchReportMessage.LoserTag);
						this.MicroPlayConn.RequestOperation(op17);
						return;
					}
				}
				else
				{
					if (message is EquipBundleMessage)
					{
						return;
					}
					if (message is DestroySlotItemMessage)
					{
						return;
					}
					if (message is UseInventoryItemMessage)
					{
						return;
					}
					if (message is RequestGoddessProtectionMessage)
					{
						return;
					}
					if (message is KickMessage)
					{
						KickMessage kickMessage = message as KickMessage;
						this.PartyConn.RequestOperation(new KickMember
						{
							MasterCID = -1L,
							MemberSlot = kickMessage.Slot,
							NexonSN = kickMessage.NexonSN
						});
						return;
					}
					if (message is AllUserJoinCompleteMessage)
					{
						this.MicroPlayConn.RequestOperation(new AllUserJoinComplete());
						return;
					}
					if (message is SkillEnhanceUseDurabilityMessage)
					{
						SkillEnhanceUseDurabilityMessage skillEnhanceUseDurabilityMessage = message as SkillEnhanceUseDurabilityMessage;
						SkillEnhanceUseDurabilityFromHost op18 = new SkillEnhanceUseDurabilityFromHost(skillEnhanceUseDurabilityMessage.Slot, skillEnhanceUseDurabilityMessage.UseDurability);
						this.MicroPlayConn.RequestOperation(op18);
						return;
					}
					Log<DSEntity>.Logger.ErrorFormat("[Invalid Message inDS PartyID {0}] {1}", this.PartyID, message);
				}
			}
		}

		public void ProcessMessage_Pvp(object message)
		{
			if (message is RegisterServerMessage)
			{
				RegisterServerMessage registerServerMessage = message as RegisterServerMessage;
				registerServerMessage.TheInfo.DSIP = DSService.MyIP;
				RegisterHostGameInfo op = new RegisterHostGameInfo(this.Entity.ID, registerServerMessage.TheInfo);
				this.PvpConn.RequestOperation(op);
				return;
			}
			if (message is PongMessage)
			{
				PongMessage pongMessage = message as PongMessage;
				this.PongReceived(pongMessage.Tag);
				return;
			}
			if (message is PvpReportMessage)
			{
				PvpReportMessage pvpReportMessage = message as PvpReportMessage;
				PvpReport op2 = new PvpReport(this.Entity.ID, pvpReportMessage.Event, pvpReportMessage.Subject, pvpReportMessage.Object, pvpReportMessage.Arg);
				this.PvpConn.RequestOperation(op2);
				return;
			}
			if (message is PvpPenaltyMessage)
			{
				PvpPenaltyMessage pvpPenaltyMessage = message as PvpPenaltyMessage;
				PvpGivePanalty op3 = new PvpGivePanalty(pvpPenaltyMessage.PlayerIndex, this.Entity.ID);
				this.PvpConn.RequestOperation(op3);
				return;
			}
			if (message is KickMessage)
			{
				KickMessage kickMessage = message as KickMessage;
				PvpKick op4 = new PvpKick(this.Entity.ID, kickMessage.Slot);
				this.PvpConn.RequestOperation(op4);
			}
		}

		public Dictionary<string, string> PvpConfig { get; set; }

		public PvpGameInfo PvpGameInfo { get; set; }

		public IEntityProxy PvpConn { get; set; }

		public void StartPvpDS(long pvpID, string pvpBSP, Dictionary<string, string> config, PvpGameInfo gameInfo)
		{
			Log<DSEntity>.Logger.InfoFormat("[DS{0}] StartPVPDS", this.DSID);
			this.DSType = DSEntity.DSEntityType.DSPvp;
			this.PvpConfig = config;
			this.PvpGameInfo = gameInfo;
			this.QuestID = pvpBSP;
			this.PvpConn = DSService.Instance.Connect(this.Entity, new Location(pvpID, "PvpService.PvpService"));
			this.IsGiantRaid = true;
			this.FPS = 0;
			DSLog.AddLog(this, "StartPvpDS", "");
			if (this.IsDSProcessRestart())
			{
				if (this.Process != null)
				{
					Log<DSService>.Logger.Error("중복실행입니다. 기존 프로세스를 죽입니다.");
					try
					{
						this.Process.Kill();
						DSLog.AddLog(this, "KillProcess (StartPvpDS)", "");
					}
					catch (Exception ex)
					{
						Log<DSEntity>.Logger.FatalFormat("KillProcess Failed!!\n - {0}", ex);
						DSLog.AddLog(this, "KillProcess Failed", "");
					}
				}
				if (this.TcpClient != null)
				{
					Log<DSService>.Logger.Error("중복접속입니다. 기존 TcpClient를 닫습니다.");
					try
					{
						this.TcpClient.Disconnect();
						DSLog.AddLog(this, "DisconnectTcp", "");
					}
					catch
					{
					}
					this.TcpClient = null;
				}
				try
				{
					this.Process = new Process();
					this.Process.StartInfo.FileName = ServiceCoreSettings.Default.DSExecPath;
					this.Process.StartInfo.Arguments = string.Format("-console -connectip {0} -connectport {1} -port {2} {3} +is_giant_raid {4} -nohltv -owner_pid {5} -owner_dsid {6}", new object[]
					{
						"127.0.0.1",
						DSService.Instance.Port,
						ServiceCoreSettings.Default.DSPort + this.PortIndex,
						ServiceCoreSettings.Default.DSAuxParams,
						this.IsGiantRaid ? 1 : 0,
						Process.GetCurrentProcess().Id,
						this.DSID
					});
					string @string = FeatureMatrix.GetString("DSAuxParamsEx");
					if (@string.Length > 0)
					{
						ProcessStartInfo startInfo = this.Process.StartInfo;
						startInfo.Arguments = startInfo.Arguments + " " + @string;
					}
					if (ServiceCoreSettings.Default.DSRedirectConsole)
					{
						this.Process.StartInfo.RedirectStandardOutput = true;
						this.Process.StartInfo.UseShellExecute = false;
						this.StdOutBuffer = new byte[1024];
						this.IsStdOutRedirected = true;
					}
					this.Process.EnableRaisingEvents = true;
					this.Process.Exited += delegate(object s, EventArgs e)
					{
						this.CompleteStdOut();
						DSService.Instance.Thread.Enqueue(Job.Create(delegate
						{
							DSLog.AddLog(this, "ProcessExited", "");
							if (FeatureMatrix.IsEnable("DSDynamicLoad"))
							{
								UpdateDSShipInfo updateDSShipInfo = new UpdateDSShipInfo(DSService.Instance.ID, this.DSID, 0L, UpdateDSShipInfo.CommandEnum.PVPClosed, this.FailReason);
								updateDSShipInfo.OnComplete += delegate(Operation _)
								{
									DSService.Instance.DSEntities.Remove(this.DSID);
								};
								DSService.RequestDSBossOperation(updateDSShipInfo);
							}
							this.Process = null;
							this.TerminateDS();
							Log<DSService>.Logger.Info("프로세스 종료됐음");
						}));
					};
					this.Process.Start();
					if (this.IsStdOutRedirected)
					{
						try
						{
							this.StdOutAsyncResult = this.Process.StandardOutput.BaseStream.BeginRead(this.StdOutBuffer, 0, this.StdOutBuffer.Length, new AsyncCallback(this.StdOutAsync), null);
						}
						catch (Exception ex2)
						{
							Log<DSEntity>.Logger.Error("Exception occurred in StartPvpDS(BeginRead): ", ex2);
						}
					}
					this.Process.PriorityClass = ProcessPriorityClass.AboveNormal;
					this.ResetPing();
					DSLog.AddLog(this, "StartProcess", "");
					return;
				}
				catch (Exception ex3)
				{
					Log<DSService>.Logger.Error("에러발생!", ex3);
					return;
				}
			}
			try
			{
				this.InitializeServerPvp();
			}
			catch (Exception ex4)
			{
				Log<DSEntity>.Logger.FatalFormat("InitializeServerPvp Failed!!\n - {0}", ex4);
				DSLog.AddLog(this, "InitializeServerPvp Failed", "");
			}
		}

		private void StopPvpDS()
		{
			DSLog.AddLog(this, "StopPvpDS", "");
			this.FPS = 0;
			if (this.IsDSProcessRestart())
			{
				if (this.Process == null)
				{
					goto IL_B7;
				}
				try
				{
					if (!this.Process.HasExited)
					{
						Process process = this.Process;
						this.Process = null;
						process.Kill();
						DSLog.AddLog(this, "KillProcess (StopPvpDS)", "");
					}
					goto IL_B7;
				}
				catch (Exception ex)
				{
					Log<DSEntity>.Logger.FatalFormat("KillProcess Failed!!\n - {0}", ex);
					DSLog.AddLog(this, "KillProcess Failed", "");
					goto IL_B7;
				}
			}
			if (FeatureMatrix.IsEnable("DSDynamicLoad"))
			{
				DSService.Instance.Thread.Enqueue(Job.Create(delegate
				{
					if (this.Process != null && !this.Process.HasExited)
					{
						UpdateDSShipInfo op = new UpdateDSShipInfo(DSService.Instance.ID, this.DSID, 0L, UpdateDSShipInfo.CommandEnum.PVPFinished, this.FailReason);
						DSService.RequestDSBossOperation(op);
						this.FailReason = null;
					}
				}));
			}
			IL_B7:
			if (this.PartyConn != null)
			{
				IEntityProxy partyConn = this.PartyConn;
				this.PartyConn = null;
				partyConn.Close(true);
				DSLog.AddLog(this, "ClosePartyConn", "");
			}
			if (this.MicroPlayConn != null)
			{
				IEntityProxy microPlayConn = this.MicroPlayConn;
				this.MicroPlayConn = null;
				microPlayConn.Close(true);
				DSLog.AddLog(this, "CloseMicroPlayConn", "");
			}
			if (this.FrontendConn != null)
			{
				IEntityProxy frontendConn = this.FrontendConn;
				this.FrontendConn = null;
				frontendConn.Close(true);
				DSLog.AddLog(this, "CloseFrontendConn", "");
			}
			if (this.PvpConn != null)
			{
				IEntityProxy pvpConn = this.PvpConn;
				this.PvpConn = null;
				pvpConn.Close(true);
				DSLog.AddLog(this, "ClosePvpConn", "");
			}
			if (this.TcpClient != null && this.IsDSProcessRestart())
			{
				this.TcpClient.Disconnected -= this.TcpClient_Disconnected;
				this.TcpClient = null;
			}
			this.QuestID = null;
			this.PartyID = -1L;
			this.MicroPlayID = -1L;
			this.FrontendID = -1L;
			this.BlockEntering = false;
			this.GameStartTime = DateTime.UtcNow;
			this.GameStartComplete = false;
		}

		public void RegisterConnectionPvp(Devcat.Core.Net.TcpClient tcpClient)
		{
			Log<DSEntity>.Logger.WarnFormat("[DS{0}] RegisterConnection", this.DSID);
			this.TcpClient = tcpClient;
			DSLog.AddLog(this, "ConnectedPvp", "");
			Log<DSEntity>.Logger.WarnFormat("[DS{0}] ConnectionSucceed", this.DSID);
			object typeConverter = DSService.Instance.MessageHandlerFactory.GetTypeConverter();
			this.TcpClient.Transmit(SerializeWriter.ToBinary(typeConverter));
			this.SyncFeatureMatrix();
			this.InitializeServerPvp();
			this.TcpClient.Disconnected += this.TcpClient_Disconnected;
			this.TcpClient.ExceptionOccur += delegate(object s, EventArgs<Exception> e)
			{
				DSLog.AddLog(this, "ExceptionPvp", e.Value.Message.Substring(0, 128));
				Log<DSEntity>.Logger.ErrorFormat("[DS{0}] ExceptionOccur", this.DSID);
				Log<DSEntity>.Logger.Error("ExceptionOccur", e.Value);
			};
			this.TcpClient.ConnectionFail += delegate(object s, EventArgs<Exception> e)
			{
				DSLog.AddLog(this, "DisconnectedPvp", "");
				Log<DSEntity>.Logger.ErrorFormat("[DS{0}] ConnectionFail", this.DSID);
			};
			this.TcpClient.PacketReceive += delegate(object s, EventArgs<ArraySegment<byte>> e)
			{
				Packet packet = new Packet(e.Value);
				DSService.Instance.MessageHandlerFactory.Handle(packet, this);
			};
		}

		public void InitializeServerPvp()
		{
			DSLog.AddLog(this, "InitializeServerPvp", "");
			this.SendMessage<PvpGameHostedMessage>(new PvpGameHostedMessage(null, PvpTeamID.Invalid, -1, this.PvpConfig, this.PvpGameInfo));
			DSLog.AddLog(this, "PvpGameHosted", "");
			Log<DSEntity>.Logger.WarnFormat("Waiting RegisterServerMessage....", new object[0]);
		}

		private const int StdOutBufferSize = 1024;

		private bool IsStdOutRedirected;

		private byte[] StdOutBuffer;

		private IAsyncResult StdOutAsyncResult;

		private int StdOutBufferIdx;

		private int DSEndCount;

		public enum DSEntityType
		{
			DSQuest,
			DSPvp
		}
	}
}
