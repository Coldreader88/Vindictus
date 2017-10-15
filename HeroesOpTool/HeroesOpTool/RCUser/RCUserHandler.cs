using System;
using System.Collections.Generic;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using RemoteControlSystem;
using RemoteControlSystem.ClientMessage;
using RemoteControlSystem.ControlMessage;
using RemoteControlSystem.ServerMessage;

namespace HeroesOpTool.RCUser
{
	public class RCUserHandler
	{
		public event EventHandler<EventArgs<RCClient>> ClientAdd;

		public event EventHandler<EventArgs<RCClient>> ClientRemove;

		public event EventHandler<EventArgs<IEnumerable<Member>>> ReceivedUserListReply;

		public event RCUserHandler.WorkGroupStructureEventHandler WorkGroupStructureChange;

		public event RCUserHandler.WorkGroupStructureEventHandler ServerGroupStructureChange;

		public event EventHandler<EventArgs<ControlEnterReply>> ControlReply;

		public event EventHandler<EventArgs<RCUserHandler.ConnectionResult>> ConnectionResulted;

		public event EventHandler Closed;

		public event EventHandler<EventArgs<NotifyMessage>> Notify;

		public event EventHandler<EventArgs<List<string>>> EmergencyCallInfo;

		public event EventHandler<EventArgs<string>> UserCountLogged;

		public event EventHandler<EventArgs<ChildProcessLogListReplyMessage>> ChildProcessListed;

		public event EventHandler<EventArgs<ChildProcessLogReplyMessage>> ChildProcessLogOpened;

		public event EventHandler<EventArgs<ChildProcessLogMessage>> ChildProcessLogged;

		public event EventHandler<EventArgs<ExeInfoReplyMessage>> ExeInfo;

		public bool IsConnected
		{
			get
			{
				return this.Authority > Authority.None;
			}
		}

		public string ID
		{
			get
			{
				return this.config.ID;
			}
		}

		public int ClientID { get; private set; }

		public Authority Authority { get; private set; }

		public IEnumerable<string> ServerGroups
		{
			get
			{
				this.RebuildServerGroupCache();
				return this.serverGroupCache.Keys;
			}
		}

		public IEnumerable<KeyValuePair<RCProcess, RCClient>> CommandBridges
		{
			get
			{
				return this.commandBridges;
			}
		}

		public IList<RCClient> ClientList
		{
			get
			{
				return this.clientList.Values;
			}
		}

		public RCUserHandler(Configuration _config)
		{
			this.config = _config;
			this.cleaning = false;
			this.active = false;
			this.MF = new MessageHandlerFactory();
			this.MF.Register<RCUserHandler>(OpToolMessages.TypeConverters, "ProcessMessage");
			this.MF.Register<RCUserHandler>(ControlMessages.TypeConverters, "ProcessMessage");
			this.MF.Register<RCClient>(RCClientMessages.TypeConverters, "ProcessMessage");
			this.tcpClient = new TcpClient();
			this.tcpClient.ConnectionSucceed += this.OnConnect;
			this.tcpClient.ConnectionFail += delegate(object sender, EventArgs<Exception> e)
			{
				if (this.ConnectionResulted != null)
				{
					this.ConnectionResulted(this, new EventArgs<RCUserHandler.ConnectionResult>(RCUserHandler.ConnectionResult.Fail));
				}
				if (this.firstConnected)
				{
					this.OnClose(this, EventArgs.Empty);
				}
			};
			this.tcpClient.PacketReceive += delegate(object sender, EventArgs<ArraySegment<byte>> e)
			{
				this.MF.Handle(new Packet(e.Value), this);
			};
			this.tcpClient.Disconnected += this.OnClose;
			this.commandBridgeModified = true;
			this.commandBridges = new Dictionary<RCProcess, RCClient>();
			this.userCounts = new Dictionary<RCProcess, RCClient>();
			this.clientList = new SortedList<int, RCClient>();
			this.ClientAdd += this.RCUserHandler_ClientAdd;
			this.ClientRemove += this.RCUserHandler_ClientRemove;
		}

		public void Start()
		{
			this.Connect();
			this.active = true;
			this.cleaning = false;
		}

		private void Connect()
		{
			if (!this.tcpClient.Connected)
			{
				this.tcpClient.Connect(HeroesOpTool.Thread, this.config.RcsIP, this.config.RcsPort, new MessageAnalyzer());
			}
		}

		public void Reconnect()
		{
			Thread.Sleep(1000);
			this.Connect();
		}

		public void Stop()
		{
			if (this.active)
			{
				this.cleaning = true;
				this.Authority = Authority.None;
				this.tcpClient.Disconnect();
				this.active = false;
				return;
			}
			throw new InvalidOperationException("Already stopped");
		}

		private void OnConnect(object sender, EventArgs arg)
		{
			this.firstConnected = true;
			this.SendMessage<LoginMessage>(new LoginMessage(this.config.ID, this.config.Password));
		}

		private void OnClose(object sender, EventArgs arg)
		{
			if (!this.cleaning)
			{
				if (this.Closed != null)
				{
					this.Closed(this, null);
				}
				if (this.firstConnected)
				{
					HeroesOpTool.Thread.Enqueue(Job.Create(new Action(this.Reconnect)));
					return;
				}
			}
			else
			{
				this.cleaning = false;
			}
		}

		public void SendMessage<T>(T message)
		{
			this.tcpClient.Transmit(SerializeWriter.ToBinary<T>(message));
		}

		private void RebuildServerGroupCache()
		{
			if (this.commandBridgeModified)
			{
				this.serverGroupCache = new Dictionary<string, RCProcess>();
				foreach (RCProcess rcprocess in this.commandBridges.Keys)
				{
					IEnumerable<string> commandBridgeServer = RCUserHandler.GetCommandBridgeServer(rcprocess);
					foreach (string key in commandBridgeServer)
					{
						if (!this.serverGroupCache.ContainsKey(key))
						{
							this.serverGroupCache.Add(key, rcprocess);
						}
					}
				}
				this.commandBridgeModified = false;
			}
		}

		public static IEnumerable<string> GetCommandBridgeServer(RCProcess process)
		{
			if (process.Properties.ContainsKey("commandbridge"))
			{
				string text = process.Properties["commandbridge"];
				return text.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
			}
			return new string[0];
		}

		private void RCUserHandler_ClientAdd(object sender, EventArgs<RCClient> args)
		{
			foreach (RCProcess rcprocess in args.Value.ProcessList)
			{
				if (rcprocess.Properties.ContainsKey("commandbridge"))
				{
					this.commandBridges.Add(rcprocess, args.Value);
					this.commandBridgeModified = true;
				}
				if (rcprocess.Properties.ContainsKey("UserCount"))
				{
					this.CheckAndAddProcessLog(args.Value, rcprocess);
				}
			}
			args.Value.ProcessAdd += this.RCUserHandler_ProcessAdd;
			args.Value.ProcessModify += this.RCUserHandler_ProcessModify;
			args.Value.ProcessRemove += this.RCUserHandler_ProcessRemove;
		}

		private void RCUserHandler_ClientRemove(object sender, EventArgs<RCClient> args)
		{
			foreach (RCProcess key in args.Value.ProcessList)
			{
				if (this.commandBridges.ContainsKey(key))
				{
					this.commandBridges.Remove(key);
					this.commandBridgeModified = true;
				}
			}
			args.Value.ProcessAdd -= this.RCUserHandler_ProcessAdd;
			args.Value.ProcessModify -= this.RCUserHandler_ProcessModify;
			args.Value.ProcessRemove -= this.RCUserHandler_ProcessRemove;
		}

		private void RCUserHandler_ProcessAdd(RCClient sender, RCProcess process)
		{
			if (process.Properties.ContainsKey("commandbridge"))
			{
				this.commandBridges.Add(process, sender);
				this.commandBridgeModified = true;
			}
			if (process.Properties.ContainsKey("UserCount"))
			{
				this.CheckAndAddProcessLog(sender, process);
			}
		}

		private void RCUserHandler_ProcessModify(RCClient sender, RCProcess process)
		{
			if (!process.Properties.ContainsKey("commandbridge"))
			{
				if (this.commandBridges.ContainsKey(process))
				{
					this.commandBridges.Remove(process);
					this.commandBridgeModified = true;
				}
			}
			else if (!this.commandBridges.ContainsKey(process))
			{
				this.commandBridges.Add(process, sender);
				this.commandBridgeModified = true;
			}
			if (!process.Properties.ContainsKey("UserCount"))
			{
				this.CheckAndRemoveProcessLog(sender, process);
				return;
			}
			this.CheckAndAddProcessLog(sender, process);
		}

		private void RCUserHandler_ProcessRemove(RCClient sender, RCProcess process)
		{
			if (this.commandBridges.ContainsKey(process))
			{
				this.commandBridgeModified = true;
				this.commandBridges.Remove(process);
			}
			this.CheckAndRemoveProcessLog(sender, process);
		}

		private bool CheckAndAddProcessLog(RCClient client, RCProcess process)
		{
			if (!this.userCounts.ContainsKey(process))
			{
				if (!this.userCounts.ContainsValue(client))
				{
					client.ProcessLog += this.RCUserHandler_ProcessLog;
				}
				this.userCounts.Add(process, client);
				return true;
			}
			return false;
		}

		private bool CheckAndRemoveProcessLog(RCClient client, RCProcess process)
		{
			if (this.userCounts.ContainsKey(process))
			{
				this.userCounts.Remove(process);
				if (!this.userCounts.ContainsValue(client))
				{
					client.ProcessLog -= this.RCUserHandler_ProcessLog;
					return true;
				}
			}
			return false;
		}

		private void RCUserHandler_ProcessLog(RCClient sender, RCClient.ProcessLogEventArgs args)
		{
			if (this.userCounts.ContainsKey(args.Process) && args.Process.PerformanceString.Length > 0 && RCProcess.IsStandardOutputLog(args.Message) && args.Process.IsPerformanceLog(args.Message) && this.UserCountLogged != null)
			{
				this.UserCountLogged(args.Process, new EventArgs<string>(args.Process.GetPerformanceLog(args.Message)));
			}
		}

		public void GetUserList()
		{
			this.SendMessage<GetUserListMessage>(new GetUserListMessage());
		}

		public void ChangePassword(byte[] oldPassword, byte[] newPassword)
		{
			this.SendMessage<ChangeMyPasswordMessage>(new ChangeMyPasswordMessage(oldPassword, newPassword));
		}

		public void ChangeOtherPassword(string id, byte[] password)
		{
			this.SendMessage<ChangePasswordMessage>(new ChangePasswordMessage(id, password));
		}

		public void ModifyUser(string id, Authority authority)
		{
			this.SendMessage<ChangeAuthorityMessage>(new ChangeAuthorityMessage(id, authority));
		}

		public void RegisterUser(string id, byte[] password, Authority authority)
		{
			this.SendMessage<AddUserMessage>(new AddUserMessage(id, password, authority));
		}

		public void RemoveUser(string id)
		{
			this.SendMessage<RemoveUserMessage>(new RemoveUserMessage(id));
		}

		public void ProcessRCClientMessage(RCClient rcClient, ArraySegment<byte> value)
		{
			try
			{
				this.MF.Handle(new Packet(value), rcClient);
			}
			catch (Exception ex)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(221) + ex.ToString());
				this.Stop();
			}
		}

		private void ProcessMessage(object rawMessage)
		{
			if (rawMessage is LoginReply)
			{
				this.clientList.Clear();
				LoginReply loginReply = rawMessage as LoginReply;
				if (BaseConfiguration.ServerVersion != loginReply.ServerVersion)
				{
					if (this.ConnectionResulted != null)
					{
						this.ConnectionResulted(this, new EventArgs<RCUserHandler.ConnectionResult>(RCUserHandler.ConnectionResult.VersionMismatch));
					}
					this.Stop();
					return;
				}
				this.Authority = loginReply.Authority;
				if (this.ConnectionResulted != null)
				{
					this.ConnectionResulted(this, new EventArgs<RCUserHandler.ConnectionResult>(RCUserHandler.ConnectionResult.Success));
					return;
				}
			}
			else
			{
				if (rawMessage is EmergencyCallMessage)
				{
					EmergencyCallMessage emergencyCallMessage = rawMessage as EmergencyCallMessage;
					List<string> value = new List<string>(emergencyCallMessage.Emergencies);
					this.EmergencyCallInfo(this, new EventArgs<List<string>>(value));
					return;
				}
				if (rawMessage is ClientInfoMessage)
				{
					ClientInfoMessage clientInfoMessage = rawMessage as ClientInfoMessage;
					if (this.WorkGroupStructureChange != null)
					{
						this.WorkGroupStructureChange(this, new RCUserHandler.WorkGroupStructureEventArgs(WorkGroupStructureNode.GetWorkGroup(clientInfoMessage.WorkGroup)));
					}
					if (this.ServerGroupStructureChange != null)
					{
						this.ServerGroupStructureChange(this, new RCUserHandler.WorkGroupStructureEventArgs(ServerGroupStructureNode.GetServerGroup(clientInfoMessage.ServerGroup)));
					}
					foreach (KeyValuePair<int, RCClient> keyValuePair in clientInfoMessage.Clients)
					{
						RCClient rcclient = new RCClient(keyValuePair.Key, keyValuePair.Value.ClientIP);
						rcclient.AssignFrom(keyValuePair.Value);
						this.clientList.Add(keyValuePair.Key, rcclient);
						if (this.ClientAdd != null)
						{
							this.ClientAdd(this, new EventArgs<RCClient>(rcclient));
						}
					}
					foreach (NotifyMessage value2 in clientInfoMessage.Logs)
					{
						this.Notify(this, new EventArgs<NotifyMessage>(value2));
					}
					this.firstUpdated = true;
					return;
				}
				if (rawMessage is GetUserListReply)
				{
					GetUserListReply getUserListReply = rawMessage as GetUserListReply;
					List<Member> list = new List<Member>();
					foreach (KeyValuePair<string, Authority> keyValuePair2 in getUserListReply.Users)
					{
						list.Add(new Member(keyValuePair2.Key, keyValuePair2.Value));
					}
					this.ReceivedUserListReply(this, new EventArgs<IEnumerable<Member>>(list));
					return;
				}
				if (rawMessage is WorkGroupChangeMessage)
				{
					if (this.WorkGroupStructureChange != null)
					{
						WorkGroupChangeMessage workGroupChangeMessage = rawMessage as WorkGroupChangeMessage;
						this.WorkGroupStructureChange(this, new RCUserHandler.WorkGroupStructureEventArgs(WorkGroupStructureNode.GetWorkGroup(workGroupChangeMessage.WorkGroup)));
						return;
					}
				}
				else if (rawMessage is ServerGroupChangeMessage)
				{
					if (this.ServerGroupStructureChange != null)
					{
						ServerGroupChangeMessage serverGroupChangeMessage = rawMessage as ServerGroupChangeMessage;
						this.ServerGroupStructureChange(this, new RCUserHandler.WorkGroupStructureEventArgs(ServerGroupStructureNode.GetServerGroup(serverGroupChangeMessage.ServerGroup)));
						return;
					}
				}
				else if (rawMessage is ClientAddedMessage)
				{
					ClientAddedMessage clientAddedMessage = rawMessage as ClientAddedMessage;
					RCClient rcclient2 = new RCClient(clientAddedMessage.ID, clientAddedMessage.Client.ClientIP);
					rcclient2.AssignFrom(clientAddedMessage.Client);
					if (this.clientList.ContainsKey(clientAddedMessage.ID))
					{
						Utility.ShowErrorMessage(LocalizeText.Get(217) + clientAddedMessage.ID);
						return;
					}
					this.clientList.Add(rcclient2.ID, rcclient2);
					this.ClientAdd(this, new EventArgs<RCClient>(rcclient2));
					return;
				}
				else if (rawMessage is ClientRemovedMessage)
				{
					ClientRemovedMessage clientRemovedMessage = rawMessage as ClientRemovedMessage;
					if (!this.clientList.ContainsKey(clientRemovedMessage.ID))
					{
						return;
					}
					RCClient value3 = this.clientList[clientRemovedMessage.ID];
					this.clientList.Remove(clientRemovedMessage.ID);
					if (this.ClientRemove != null)
					{
						this.ClientRemove(this, new EventArgs<RCClient>(value3));
						return;
					}
				}
				else if (rawMessage is ControlEnterReply)
				{
					if (this.ControlReply != null)
					{
						ControlEnterReply value4 = rawMessage as ControlEnterReply;
						this.ControlReply(this, new EventArgs<ControlEnterReply>(value4));
						return;
					}
				}
				else if (rawMessage is ControlReplyMessage)
				{
					ControlReplyMessage controlReplyMessage = rawMessage as ControlReplyMessage;
					if (this.firstUpdated)
					{
						if (this.clientList.Count == 0 || (this.clientList.Count > 0 && !this.clientList.ContainsKey(controlReplyMessage.ID)))
						{
							Type type = this.MF.GetType(new Packet(controlReplyMessage.Packet));
							Utility.ShowErrorMessage(string.Concat(new object[]
							{
								LocalizeText.Get(220),
								controlReplyMessage.ID,
								":",
								type.ToString()
							}));
							return;
						}
						this.ProcessRCClientMessage(this.clientList[controlReplyMessage.ID], controlReplyMessage.Packet);
						return;
					}
				}
				else if (rawMessage is NotifyMessage)
				{
					if (this.Notify != null)
					{
						NotifyMessage value5 = rawMessage as NotifyMessage;
						this.Notify(this, new EventArgs<NotifyMessage>(value5));
						return;
					}
				}
				else if (rawMessage is ChildProcessLogListReplyMessage)
				{
					if (this.ChildProcessListed != null)
					{
						ChildProcessLogListReplyMessage childProcessLogListReplyMessage = rawMessage as ChildProcessLogListReplyMessage;
						if (this.clientList.ContainsKey(childProcessLogListReplyMessage.ClientID))
						{
							RCClient sender = this.clientList[childProcessLogListReplyMessage.ClientID];
							this.ChildProcessListed(sender, new EventArgs<ChildProcessLogListReplyMessage>(childProcessLogListReplyMessage));
							return;
						}
					}
				}
				else if (rawMessage is ChildProcessLogReplyMessage)
				{
					if (this.ChildProcessLogOpened != null)
					{
						ChildProcessLogReplyMessage value6 = rawMessage as ChildProcessLogReplyMessage;
						this.ChildProcessLogOpened(this, new EventArgs<ChildProcessLogReplyMessage>(value6));
						return;
					}
				}
				else if (rawMessage is ChildProcessLogMessage)
				{
					if (this.ChildProcessLogged != null)
					{
						ChildProcessLogMessage value7 = rawMessage as ChildProcessLogMessage;
						this.ChildProcessLogged(this, new EventArgs<ChildProcessLogMessage>(value7));
						return;
					}
				}
				else if (rawMessage is ExeInfoReplyMessage && this.ChildProcessListed != null)
				{
					ExeInfoReplyMessage exeInfoReplyMessage = rawMessage as ExeInfoReplyMessage;
					if (this.clientList.ContainsKey(exeInfoReplyMessage.ClientID))
					{
						RCClient sender2 = this.clientList[exeInfoReplyMessage.ClientID];
						this.ExeInfo(sender2, new EventArgs<ExeInfoReplyMessage>(exeInfoReplyMessage));
					}
				}
			}
		}

		public const string CommandBridgeProperty = "commandbridge";

		public const string UserCountProperty = "UserCount";

		private TcpClient tcpClient;

		private MessageHandlerFactory MF;

		private bool cleaning;

		private bool active;

		private bool firstConnected;

		private bool firstUpdated;

		private Dictionary<RCProcess, RCClient> commandBridges;

		private bool commandBridgeModified = true;

		private Dictionary<string, RCProcess> serverGroupCache;

		private Dictionary<RCProcess, RCClient> userCounts;

		private SortedList<int, RCClient> clientList;

		private Configuration config;

		public enum ConnectionResult
		{
			Success,
			Fail,
			VersionMismatch
		}

		public delegate void WorkGroupStructureEventHandler(object sender, RCUserHandler.WorkGroupStructureEventArgs args);

		public class WorkGroupStructureEventArgs
		{
			public IWorkGroupStructureNode[] RootNodes { get; private set; }

			public WorkGroupStructureEventArgs(IWorkGroupStructureNode[] rootNodes)
			{
				this.RootNodes = rootNodes;
			}
		}
	}
}
