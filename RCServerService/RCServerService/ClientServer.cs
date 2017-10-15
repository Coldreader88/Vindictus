using System;
using System.Collections.Generic;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using RemoteControlSystem.ClientMessage;
using RemoteControlSystem.ControlMessage;
using RemoteControlSystem.ServerMessage;

namespace RemoteControlSystem.Server
{
	internal class ClientServer
	{
		public event EventHandler<EventArgs<ChildProcessLogMessage>> OnChildProcessLogAdded;

		public IEnumerable<int> GetCliendIDList()
		{
			IEnumerable<int> keys;
			lock (Base.SyncRoot)
			{
				keys = this.RCClientInfo.Keys;
			}
			return keys;
		}

		public RCClient GetClient(int id)
		{
			RCClient rcclient;
			lock (Base.SyncRoot)
			{
				rcclient = this.RCClientInfo[id].RCClient;
			}
			return rcclient;
		}

		public ClientServer()
		{
			this.RCClientInfo = new Dictionary<int, ClientServer.Client>();
			this.tcpServer = new TcpServer2();
			this.tcpServer.ClientAccept += this.ClientAccepted;
			this.jobProcessor = new JobProcessor();
			this.MF = new MessageHandlerFactory();
			this.MF.Register<ClientServer>(ControlMessages.TypeConverters, "ProcessMessage");
			this.MF.Register<RCClient>(RCClientMessages.TypeConverters, "ProcessMessage");
		}

		public void Start(int port)
		{
			this.jobProcessor.Start();
			this.tcpServer.Start(this.jobProcessor, port);
		}

		public void Stop()
		{
			this.tcpServer.Stop();
			this.jobProcessor.Stop();
		}

		private int NewClientID()
		{
			return Interlocked.Increment(ref this.clientIDProvider);
		}

		private void ClientAccepted(object sender, AcceptEventArgs e)
		{
			e.Client.ConnectionSucceed += this.ClientConnected;
			e.PacketAnalyzer = new MessageAnalyzer();
		}

		private void ClientConnected(object sender, EventArgs arg)
		{
			TcpClient tcpClient = sender as TcpClient;
			if (tcpClient != null)
			{
				lock (Base.SyncRoot)
				{
					string clientIP = tcpClient.RemoteEndPoint.Address.ToString();
					ClientServer.Client client = new ClientServer.Client(this.NewClientID(), clientIP, tcpClient);
					client.Disconnected += this.ClientDisconnected;
					client.PacketReceived += this.PacketReceived;
					client.RCClient.ClientInit += this.ClientInitialized;
					this.RCClientInfo.Add(client.ID, client);
					Log<RCServerService>.Logger.DebugFormat("RC Client {0} connected", client.ID);
				}
			}
		}

		private void ClientDisconnected(object sender, EventArgs arg)
		{
			lock (Base.SyncRoot)
			{
				ClientServer.Client client = sender as ClientServer.Client;
				if (client != null)
				{
					this.RemoveClient(client.ID);
					Log<RCServerService>.Logger.DebugFormat("RC Client {0} disconnected", client.ID);
				}
			}
		}

		private void PacketReceived(object sender, EventArgs<ArraySegment<byte>> e)
		{
			try
			{
				lock (Base.SyncRoot)
				{
					ClientServer.Client client = sender as ClientServer.Client;
					if (client != null)
					{
						Packet packet = new Packet(e.Value);
						try
						{
							if (!this.PreProcess(client.RCClient, packet))
							{
								this.MF.Handle(packet, client.RCClient);
							}
						}
						catch (Exception ex)
						{
							Log<RCServerService>.Logger.Error("Packet error", ex);
							Base.ControlServer.NotifyMessage(MessageType.Message, "[{0}/{1}({2})] - {3}", new object[]
							{
								client.ID,
								client.RCClient.Name,
								client.RCClient.ClientIP,
								ex.ToString()
							});
							client.Connection.Disconnect();
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Log<RCClient>.Logger.Error(ex2);
			}
		}

		public bool PreProcess(RCClient client, Packet packet)
		{
			Type packetType = this.GetPacketType(packet);
			if (packetType == typeof(PerformanceUpdateMessage))
			{
				Base.ControlServer.SendToUser<ControlReplyMessage>(new ControlReplyMessage(client.ID, packet.Bytes));
				return false;
			}
			return false;
		}

		private static void ProcessMessage(object rawMessage, object tag)
		{
			if (rawMessage is FunctionReplyMessage)
			{
				FunctionReplyMessage functionReplyMessage = rawMessage as FunctionReplyMessage;
				Base.ControlServer.SendToUser(functionReplyMessage.ClientID, new Packet(functionReplyMessage.Packet));
				return;
			}
			if (rawMessage is ChildProcessLogReplyMessage)
			{
				return;
			}
			if (rawMessage is ChildProcessLogMessage)
			{
				if (Base.ClientServer.OnChildProcessLogAdded != null)
				{
					RCClient sender = tag as RCClient;
					Base.ClientServer.OnChildProcessLogAdded(sender, new EventArgs<ChildProcessLogMessage>(rawMessage as ChildProcessLogMessage));
					return;
				}
			}
			else if (rawMessage is CheckPatchProcessResultMessage)
			{
				CheckPatchProcessResultMessage checkPatchProcessResultMessage = rawMessage as CheckPatchProcessResultMessage;
				foreach (string message in checkPatchProcessResultMessage.CheckPatchLog)
				{
					Base.ControlServer.NotifyMessage(MessageType.Message, message, new object[0]);
				}
			}
		}

		public Type GetPacketType(Packet packet)
		{
			return this.MF.GetType(packet);
		}

		public void SendMessage<T>(int ClientID, T message)
		{
			this.SendMessage(ClientID, SerializeWriter.ToBinary<T>(message));
		}

		public void SendMessage(int ClientID, Packet packet)
		{
			if (this.RCClientInfo.ContainsKey(ClientID))
			{
				ClientServer.Client client = this.RCClientInfo[ClientID];
				if (client.RCClient.Initialized)
				{
					client.Connection.Transmit(packet);
				}
			}
		}

		public ClientInfoMessage GetClientInfo()
		{
			ClientInfoMessage clientInfoMessage = new ClientInfoMessage(Base.WorkGroupString, Base.ServerGroupString);
			foreach (ClientServer.Client client in this.RCClientInfo.Values)
			{
				if (client.RCClient.Initialized)
				{
					clientInfoMessage.AddClient(client.ID, client.RCClient);
				}
			}
			return clientInfoMessage;
		}

		private void ClientInitialized(RCClient sender, object args)
		{
			Base.ControlServer.SendToUser<ClientAddedMessage>(new ClientAddedMessage(sender.ID, sender));
			sender.ServerInfoChange += this.Client_ServerInfoChange;
			sender.ProcessAdd += this.Client_ProcessAdd;
			sender.ProcessModify += this.Client_ProcessModify;
			sender.ProcessRemove += this.Client_ProcessRemove;
			sender.ProcessStateChange += this.Client_ProcessStateChange;
			sender.ProcessLog += this.Client_ProcessLog;
		}

		private void RemoveClient(int clientId)
		{
			this.RCClientInfo.Remove(clientId);
			Base.ControlServer.SendToUser<ClientRemovedMessage>(new ClientRemovedMessage(clientId));
		}

		public void SendPingAll()
		{
			lock (Base.SyncRoot)
			{
				RemoteControlSystem.ServerMessage.PingMessage value = new RemoteControlSystem.ServerMessage.PingMessage();
				Packet packet = SerializeWriter.ToBinary<RemoteControlSystem.ServerMessage.PingMessage>(value);
				foreach (ClientServer.Client client in this.RCClientInfo.Values)
				{
					client.Connection.Transmit(packet);
				}
			}
		}

		private void SendControlReplyMessageToUser<T>(int id, T message)
		{
			Packet packet = SerializeWriter.ToBinary<T>(message);
			Base.ControlServer.SendToUser<ControlReplyMessage>(new ControlReplyMessage(id, packet.Bytes));
		}

		private void Client_ServerInfoChange(RCClient sender, object args)
		{
			this.SendControlReplyMessageToUser<ServerInfoMessage>(sender.ID, new ServerInfoMessage(sender.ServerIP, sender.ServerPort));
		}

		private void Client_ProcessAdd(RCClient sender, RCProcess process)
		{
			this.SendControlReplyMessageToUser<AddProcessMessage>(sender.ID, new AddProcessMessage(process));
		}

		private void Client_ProcessModify(RCClient sender, RCProcess process)
		{
			this.SendControlReplyMessageToUser<ModifyProcessMessage>(sender.ID, new ModifyProcessMessage(process));
		}

		private void Client_ProcessRemove(RCClient sender, RCProcess process)
		{
			this.SendControlReplyMessageToUser<RemoveProcessMessage>(sender.ID, new RemoveProcessMessage(process.Name));
		}

		private void Client_ProcessStateChange(RCClient sender, RCClient.ProcessStateEventArgs args)
		{
			this.SendControlReplyMessageToUser<StateChangeProcessMessage>(sender.ID, new StateChangeProcessMessage(args.TargetProcess.Name, args.TargetProcess.State, args.ChangedTime));
		}

		private void Client_ProcessLog(RCClient sender, RCClient.ProcessLogEventArgs args)
		{
			if (RCProcess.IsNotifyLog(args.Message))
			{
				Base.ControlServer.NotifyMessage(MessageType.Message, RCProcess.GetOriginalLog(args.Message), new object[0]);
			}
			this.SendControlReplyMessageToUser<LogProcessMessage>(sender.ID, new LogProcessMessage(args.Process.Name, args.Message));
		}

		private ITcpServer tcpServer;

		private JobProcessor jobProcessor;

		private MessageHandlerFactory MF;

		private Dictionary<int, ClientServer.Client> RCClientInfo;

		private int clientIDProvider;

		private class Client
		{
			public event EventHandler<EventArgs> Disconnected;

			public event EventHandler<EventArgs<ArraySegment<byte>>> PacketReceived;

			public TcpClient Connection { get; private set; }

			public RCClient RCClient { get; private set; }

			public int ID
			{
				get
				{
					return this.RCClient.ID;
				}
			}

			public Client(int id, string clientIP, TcpClient tcpClient)
			{
				this.RCClient = new RCClient(id, clientIP);
				this.Connection = tcpClient;
				this.Connection.Disconnected += delegate(object sender, EventArgs e)
				{
					if (this.Disconnected != null)
					{
						this.Disconnected(this, new EventArgs());
					}
				};
				this.Connection.PacketReceive += delegate(object sender, EventArgs<ArraySegment<byte>> e)
				{
					if (this.PacketReceived != null)
					{
						this.PacketReceived(this, e);
					}
				};
			}
		}
	}
}
