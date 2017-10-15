using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;

namespace Devcat.Core.Net
{
	public class TcpClient : IPacketTransmitter, ITransmitter<Packet>, ITransmitter<IEnumerable<Packet>>
	{
		public event EventHandler<EventArgs> ConnectionSucceed;

		public event EventHandler<EventArgs<Exception>> ConnectionFail;

		public event EventHandler<EventArgs<ArraySegment<byte>>> PacketReceive;

		public event EventHandler<EventArgs> Disconnected;

		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		public long TotalReceived
		{
			get
			{
				if (this.asyncSocket == null)
				{
					return 0L;
				}
				return this.asyncSocket.TotalReceived;
			}
		}

		public long TotalSent
		{
			get
			{
				if (this.asyncSocket == null)
				{
					return 0L;
				}
				return this.asyncSocket.TotalSent;
			}
		}

		public int TotalReceivedCount
		{
			get
			{
				if (this.asyncSocket == null)
				{
					return 0;
				}
				return this.asyncSocket.TotalReceivedCount;
			}
		}

		public int TotalSentCount
		{
			get
			{
				if (this.asyncSocket == null)
				{
					return 0;
				}
				return this.asyncSocket.TotalSentCount;
			}
		}

		public object Tag { get; set; }

		public bool Connected
		{
			get
			{
				return this.asyncSocket != null && this.asyncSocket.Connected;
			}
		}

		public IPEndPoint LocalEndPoint { get; private set; }

		public IPEndPoint RemoteEndPoint { get; private set; }

		public IntPtr Handle { get; private set; }

		internal void Activate(Socket connectedSocket, JobProcessor jobProcessor, IPacketAnalyzer packetAnalyzer)
		{
			try
			{
				this.active = true;
				this.CreateAsyncSocket(connectedSocket, jobProcessor, packetAnalyzer);
				this.LocalEndPoint = (connectedSocket.LocalEndPoint as IPEndPoint);
				this.RemoteEndPoint = (connectedSocket.RemoteEndPoint as IPEndPoint);
				this.Handle = connectedSocket.Handle;
				this.OnConnectionSucceed(EventArgs.Empty);
				this.asyncSocket.Activate();
			}
			catch (Exception value)
			{
				this.active = false;
				this.OnConnectionFail(new EventArgs<Exception>(value));
			}
		}

		public void Connect(JobProcessor jobProcessor, IPEndPoint remoteEndPoint, IPacketAnalyzer packetAnalyzer)
		{
			if (this.active)
			{
				throw new InvalidOperationException("Can't use on connected instance");
			}
			this.active = true;
			this.RemoteEndPoint = remoteEndPoint;
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.BeginConnect(remoteEndPoint, new AsyncCallback(this.EndConnect), new object[]
			{
				socket,
				jobProcessor,
				packetAnalyzer
			});
			this.LocalEndPoint = (socket.LocalEndPoint as IPEndPoint);
			this.Handle = socket.Handle;
		}

		public void Connect(JobProcessor jobProcessor, IPAddress ipAddress, int port, IPacketAnalyzer packetAnalyzer)
		{
			this.Connect(jobProcessor, new IPEndPoint(ipAddress, port), packetAnalyzer);
		}

		public void Connect(JobProcessor jobProcessor, string hostNameOrAddress, int port, IPacketAnalyzer packetAnalyzer)
		{
			IPAddress ipAddress;
			if (IPAddress.TryParse(hostNameOrAddress, out ipAddress))
			{
				this.Connect(jobProcessor, ipAddress, port, packetAnalyzer);
				return;
			}
			if (this.active)
			{
				throw new InvalidOperationException("Can't use on connected instance");
			}
			this.active = true;
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.BeginConnect(hostNameOrAddress, port, new AsyncCallback(this.EndConnect), new object[]
			{
				socket,
				jobProcessor,
				packetAnalyzer
			});
		}

		public void Disconnect()
		{
			if (this.asyncSocket != null)
			{
				this.asyncSocket.Shutdown();
			}
		}

		public void Transmit(Packet packet)
		{
			this.Transmit<Packet>(packet);
		}

		public void Transmit(IEnumerable<Packet> packets)
		{
			this.Transmit<Packet>(packets);
		}

		public void Transmit<TPacket>(TPacket packet) where TPacket : IPacket
		{
			if (this.asyncSocket != null)
			{
				if (this.crypto != null)
				{
					packet.Encrypt(this.crypto);
				}
				this.asyncSocket.Send(packet.Bytes);
			}
		}

		public void Send(ArraySegment<byte> packet)
		{
			if (this.asyncSocket != null)
			{
				this.asyncSocket.Send(packet);
			}
		}

		public void Transmit<TPacket>(IEnumerable<TPacket> packets) where TPacket : IPacket
		{
			if (this.crypto != null)
			{
				foreach (TPacket tpacket in packets)
				{
					tpacket.Encrypt(this.crypto);
				}
			}
			this.asyncSocket.Send(from packet in packets
			select packet.Bytes);
		}

		protected virtual void OnConnectionSucceed(EventArgs e)
		{
			if (this.ConnectionSucceed != null)
			{
				this.ConnectionSucceed(this, e);
			}
		}

		protected virtual void OnConnectionFail(EventArgs<Exception> e)
		{
			if (this.ConnectionFail != null)
			{
				this.ConnectionFail(this, e);
			}
		}

		protected virtual void OnPacketReceive(EventArgs<ArraySegment<byte>> e)
		{
			if (this.PacketReceive != null)
			{
				this.PacketReceive(this, e);
			}
		}

		protected virtual void OnDisconnected(EventArgs e)
		{
			if (this.Disconnected != null)
			{
				this.Disconnected(this, e);
			}
		}

		protected virtual void OnExceptionOccur(EventArgs<Exception> e)
		{
			if (this.ExceptionOccur != null)
			{
				this.ExceptionOccur(this, e);
			}
		}

		private void EndConnect(IAsyncResult ar)
		{
			object[] array = (object[])ar.AsyncState;
			Socket socket = (Socket)array[0];
			JobProcessor jobProcessor = (JobProcessor)array[1];
			IPacketAnalyzer packetAnalyzer = (IPacketAnalyzer)array[2];
			if (jobProcessor != null && !jobProcessor.IsInThread())
			{
				jobProcessor.Enqueue(Job.Create<IAsyncResult>(new Action<IAsyncResult>(this.EndConnect), ar));
				return;
			}
			try
			{
				socket.EndConnect(ar);
				this.CreateAsyncSocket(socket, jobProcessor, packetAnalyzer);
				this.OnConnectionSucceed(EventArgs.Empty);
				this.asyncSocket.Activate();
			}
			catch (Exception value)
			{
				this.active = false;
				this.OnConnectionFail(new EventArgs<Exception>(value));
			}
		}

		private void CreateAsyncSocket(Socket socket, JobProcessor jobProcessor, IPacketAnalyzer packetAnalyzer)
		{
			this.asyncSocket = this.CreateAsyncSocket(socket, packetAnalyzer);
			this.crypto = ((packetAnalyzer == null) ? null : packetAnalyzer.CryptoTransform);
			if (jobProcessor == null)
			{
				this.asyncSocket.PacketReceive += delegate(object sender, EventArgs<ArraySegment<byte>> e)
				{
					this.OnPacketReceive(e);
				};
				this.asyncSocket.SocketException += delegate(object sender, EventArgs<Exception> e)
				{
					this.OnExceptionOccur(e);
				};
				this.asyncSocket.SocketClose += delegate(object sender, EventArgs e)
				{
					this.active = false;
					this.OnDisconnected(e);
				};
				return;
			}
			this.asyncSocket.PacketReceive += delegate(object sender, EventArgs<ArraySegment<byte>> e)
			{
				jobProcessor.Enqueue(Job.Create<EventArgs<ArraySegment<byte>>>(new Action<EventArgs<ArraySegment<byte>>>(this.OnPacketReceive), e));
			};
			this.asyncSocket.SocketException += delegate(object sender, EventArgs<Exception> e)
			{
				jobProcessor.Enqueue(Job.Create<EventArgs<Exception>>(new Action<EventArgs<Exception>>(this.OnExceptionOccur), e));
			};
			this.asyncSocket.SocketClose += delegate(object sender, EventArgs e)
			{
				this.active = false;
				jobProcessor.Enqueue(Job.Create<EventArgs>(new Action<EventArgs>(this.OnDisconnected), e));
			};
		}

		protected virtual IAsyncSocket CreateAsyncSocket(Socket socket, IPacketAnalyzer packetAnalyzer)
		{
			return new AsyncTcpWorker(socket, packetAnalyzer);
		}

		private bool active;

		private IAsyncSocket asyncSocket;

		private ICryptoTransform crypto;
	}
}
