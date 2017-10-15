using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using UnifiedNetwork.Entity.Operations;
using Utility;

namespace UnifiedNetwork.PipedNetwork
{
	public class Peer
	{
		public static Peer CurrentPeer
		{
			get
			{
				return Peer.currentPeer;
			}
		}

		public object Tag { get; set; }

		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.client.LocalEndPoint;
			}
		}

		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.client.RemoteEndPoint;
			}
		}

		public IntPtr Handle
		{
			get
			{
				return this.client.Handle;
			}
		}

		public event Action<Peer, Pipe> PipeOpen;

		public event Action<Peer> Connected;

		public event Action<Peer> ConnectionFailed;

		public event Action<Peer> Disconnected;

		public event Action<Peer, object> NonPipeMessageReceived;

		public bool IsConnected
		{
			get
			{
				return this.connected;
			}
		}

		private Peer()
		{
		}

		public static Peer CreateClient(Acceptor parent, TcpClient client)
		{
			Peer peer = new Peer();
			peer.parent = parent;
			peer.client = client;
			peer.client.ConnectionSucceed += peer.client_ConnectionSucceed;
			peer.client.ConnectionFail += peer.client_ConnectionFail;
			peer.client.PacketReceive += peer.client_PacketReceive;
			peer.client.ExceptionOccur += peer.client_ExceptionOccur;
			peer.client.Disconnected += peer.client_Disconnected;
			peer.issuance = new IDIssuance(1, 1073741823);
			return peer;
		}

		public static Peer CreateFromServer(Acceptor parent, TcpClient client)
		{
			Peer peer = new Peer();
			peer.parent = parent;
			peer.client = client;
			peer.client.PacketReceive += peer.client_PacketReceive;
			peer.client.ExceptionOccur += peer.client_ExceptionOccur;
			peer.client.Disconnected += peer.client_Disconnected;
			peer.issuance = new IDIssuance(1073741824, 2147483646);
			peer.connected = true;
			return peer;
		}

		public void Connect(IPEndPoint location)
		{
			if (this.connected)
			{
				throw new InvalidOperationException("이미 연결되어있는 Peer를 또 연결하려고 했습니다.");
			}
			this.client.Connect(this.parent.Thread, location, new MessageAnalyzer());
		}

		public Pipe InitPipe()
		{
			if (!this.connected)
			{
				return null;
			}
			Pipe pipe2 = new Pipe(this.issuance.ReserveNext());
			pipe2.PacketSending += delegate(Packet packet)
			{
				this.client.Transmit(packet);
			};
			pipe2.Closed += delegate(Pipe pipe)
			{
				this.pipes.Remove(pipe.ID);
			};
			Packet packet2 = SerializeWriter.ToBinary<OpenPipe>(new OpenPipe(pipe2.ID));
			packet2.InstanceId = 0L;
			this.client.Transmit(packet2);
			this.pipes[pipe2.ID] = pipe2;
			return pipe2;
		}

		public void Disconnect()
		{
			this.connected = false;
			this.client.Disconnect();
		}

		public Pipe GetPipe(int pipeid)
		{
			Pipe result;
			this.pipes.TryGetValue(pipeid, out result);
			return result;
		}

		public void SendNonPipeMessage<T>(T message)
		{
			Packet packet = SerializeWriter.ToBinary<T>(message);
			packet.InstanceId = 0L;
			this.client.Transmit(packet);
		}

		private void client_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			try
			{
				Packet packet = new Packet(e.Value);
				Peer.currentPeer = this;
				Pipe pipe;
				if (packet.InstanceId == 0L)
				{
					this.parent.MessageHandler.Handle(packet, this);
				}
				else if (this.pipes.TryGetValue((int)packet.InstanceId, out pipe))
				{
					pipe.ProcessPacket(packet);
					this.parent.PipeMessageHandler.Handle(packet, pipe);
				}
				else
				{
					Type type = this.parent.PipeMessageHandler.GetType(packet);
					if (type != typeof(RequestClose))
					{
						Log<Peer>.Logger.InfoFormat("No pipe for the request message. {{ type = {0}, packet = {1} }}", type, packet.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				Log<Peer>.Logger.Error("client_PacketReceive", ex);
			}
			finally
			{
				Peer.currentPeer = null;
			}
		}

		private void client_ConnectionFail(object sender, EventArgs<Exception> e)
		{
			this.connected = false;
			Log<Peer>.Logger.Error("PipedNetworkPeer connection failed!", e.Value);
			if (this.ConnectionFailed != null)
			{
				this.ConnectionFailed(this);
			}
		}

		private void client_ConnectionSucceed(object sender, EventArgs e)
		{
			this.connected = true;
			if (this.Connected != null)
			{
				this.Connected(this);
			}
			this.Connected = null;
			this.ConnectionFailed = null;
		}

		private void client_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			Log<Peer>.Logger.Error("Exception Occured in Peer", e.Value);
			FileLog.Log("Peer.log", string.Format("[{0}] {1}\n{2}", this.LocalEndPoint, e.Value.Message, e.Value.StackTrace));
		}

		private void client_Disconnected(object sender, EventArgs e)
		{
			Log<Peer>.Logger.FatalFormat("{0} Disconnected to {1}", this.LocalEndPoint, this.RemoteEndPoint);
			FileLog.Log("Peer.log", string.Format("{0} Disconnected to {1}", this.LocalEndPoint, this.RemoteEndPoint));
			this.connected = false;
			while (this.pipes.Count > 0)
			{
				foreach (Pipe pipe in new LinkedList<Pipe>(this.pipes.Values))
				{
					pipe.Close();
				}
			}
			if (this.Disconnected != null)
			{
				this.Disconnected(this);
			}
		}

		private void ProcessNonPipeMessage(object message)
		{
			if (message is OpenPipe)
			{
				this.ProcessOpenPipe(message as OpenPipe);
				return;
			}
			if (message is ClosePipe)
			{
				this.ProcessClosePipe(message as ClosePipe);
				return;
			}
			if (this.NonPipeMessageReceived != null)
			{
				this.NonPipeMessageReceived(this, message);
			}
		}

		private void ProcessOpenPipe(OpenPipe message)
		{
			if (this.pipes.ContainsKey(message.PipeID))
			{
				throw new InvalidOperationException(string.Format("같은 PipeID의 파이프를 또 열려고 합니다 : {0}", message.PipeID));
			}
			Pipe pipe = new Pipe(message.PipeID);
			pipe.PacketSending += delegate(Packet packet)
			{
				this.client.Transmit(packet);
			};
			pipe.Closed += this.newPipe_Closed;
			this.pipes[message.PipeID] = pipe;
			if (this.PipeOpen != null)
			{
				this.PipeOpen(this, pipe);
			}
		}

		private void newPipe_Closed(Pipe pipe)
		{
			this.pipes.Remove(pipe.ID);
		}

		private void ProcessClosePipe(ClosePipe message)
		{
			if (this.pipes.ContainsKey(message.PipeID))
			{
				Pipe pipe = this.pipes[message.PipeID];
				Log<Peer>.Logger.DebugFormat("파이프가 아직 안 닫혔는데 ClosePipe가 날아왔습니다. : {0}", pipe.Tag);
				pipe.Close();
			}
		}

		[ThreadStatic]
		private static Peer currentPeer;

		private TcpClient client;

		private Acceptor parent;

		private bool connected;

		private IDictionary<int, Pipe> pipes = new Dictionary<int, Pipe>();

		private IDIssuance issuance;
	}
}
