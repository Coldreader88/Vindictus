using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Devcat.Core.Collections.Concurrent;

namespace Devcat.Core.Net
{
	internal class AsyncTcpWorker : IAsyncSocket
	{
		public event EventHandler<EventArgs<ArraySegment<byte>>> PacketReceive;

		public event EventHandler<EventArgs> SocketClose;

		public event EventHandler<EventArgs<Exception>> SocketException;

		public long TotalSent
		{
			get
			{
				return this.totalSent;
			}
		}

		public int TotalSentCount
		{
			get
			{
				return this.totalSentCount;
			}
		}

		public long TotalReceived
		{
			get
			{
				return this.totalReceived;
			}
		}

		public int TotalReceivedCount
		{
			get
			{
				return this.totalReceivedCount;
			}
		}

		public bool Connected
		{
			get
			{
				Socket socket = this.socket;
				return socket != null && socket.Connected;
			}
		}

		public byte[] RemoteAddress
		{
			get
			{
				return this.remoteAddress;
			}
		}

		public int RemotePort
		{
			get
			{
				return this.remortPort;
			}
		}

		public AsyncTcpWorker(Socket socket) : this(socket, null)
		{
		}

		public AsyncTcpWorker(Socket socket, IPacketAnalyzer packetAnalyzer)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			if (packetAnalyzer == null)
			{
				throw new ArgumentNullException("packetAnalyzer");
			}
			if (!socket.Connected)
			{
				throw new ArgumentException("Can't activate on closed socket.", "socket");
			}
			if (socket.SocketType != SocketType.Stream || socket.AddressFamily != AddressFamily.InterNetwork || socket.ProtocolType != ProtocolType.Tcp)
			{
				throw new ArgumentException("Only TCP/IPv4 socket available.", "socket");
			}
			LingerOption lingerOption = socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger) as LingerOption;
			if (lingerOption == null || lingerOption.Enabled)
			{
				throw new ArgumentException("Linger option must be disabled.", "socket");
			}
			if (socket.UseOnlyOverlappedIO)
			{
				throw new ArgumentException("Socket must be bind on completion port.", "socket");
			}
			socket.SendBufferSize = 0;
			socket.NoDelay = true;
			this.socket = socket;
			this.packetAnalyzer = packetAnalyzer;
			this.sendQueue = new ConcurrentQueue<ArraySegment<byte>>();
			IPEndPoint ipendPoint = (IPEndPoint)socket.RemoteEndPoint;
			this.remoteAddress = ipendPoint.Address.GetAddressBytes();
			this.remortPort = ipendPoint.Port;
			this.receiveBuffer = new byte[8192];
			this.sendBufferList = new List<ArraySegment<byte>>(64);
		}

		public void Activate()
		{
			if (Interlocked.CompareExchange(ref this.active, 1, 0) != 0)
			{
				throw new InvalidOperationException("Can't reactivate AsyncSocket instance!");
			}
			this.BeginReceive();
		}

		public void Shutdown()
		{
			this.Shutdown(SocketError.Success);
		}

		public void Shutdown(SocketError errorCode)
		{
			if (this.active == 0)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref this.closed, 1, 0) != 0)
			{
				return;
			}
			this.socket.Close();
			if (this.SocketClose != null)
			{
				this.SocketClose(this, new EventArgs<SocketError>(errorCode));
			}
		}

		public void Send(ArraySegment<byte> data)
		{
			if (this.closed == 0 && data.Count > 0)
			{
				this.sendQueue.Enqueue(data);
				this.BeginSend();
			}
		}

		public void Send(IEnumerable<ArraySegment<byte>> dataList)
		{
			if (this.closed == 0)
			{
				foreach (ArraySegment<byte> item in dataList)
				{
					this.sendQueue.Enqueue(item);
				}
				this.BeginSend();
			}
		}

		private void BeginReceive()
		{
			SocketError socketError;
			try
			{
				if (!this.socket.Connected)
				{
					return;
				}
				this.totalReceivedCount++;
				this.socket.BeginReceive(this.receiveBuffer, 0, this.receiveBuffer.Length, SocketFlags.None, out socketError, new AsyncCallback(this.EndReceive), null);
			}
			catch (ObjectDisposedException)
			{
				return;
			}
			if (socketError != SocketError.Success && socketError != SocketError.IOPending)
			{
				this.OnException(new SocketException((int)socketError));
			}
		}

		private void EndReceive(IAsyncResult ar)
		{
			SocketError socketError;
			int num;
			try
			{
				if (!this.socket.Connected)
				{
					return;
				}
				num = this.socket.EndReceive(ar, out socketError);
				this.totalReceived += (long)num;
			}
			catch (ObjectDisposedException)
			{
				return;
			}
			if (num == 0)
			{
				this.Shutdown(socketError);
				return;
			}
			if (socketError == SocketError.Success)
			{
				try
				{
					this.packetAnalyzer.Add(new ArraySegment<byte>(this.receiveBuffer, 0, num));
					foreach (ArraySegment<byte> value in this.packetAnalyzer)
					{
						if (this.PacketReceive != null)
						{
							this.PacketReceive(this, new EventArgs<ArraySegment<byte>>(value));
						}
					}
					this.BeginReceive();
					return;
				}
				catch (Exception exception)
				{
					this.OnException(exception);
					return;
				}
			}
			this.OnException(new SocketException((int)socketError));
		}

		private bool AcquireSendLock()
		{
			return Interlocked.Increment(ref this.sending) == 1;
		}

		private bool ReleaseSendLock()
		{
			return Interlocked.Exchange(ref this.sending, 0) == 1;
		}

		private void BeginSend()
		{
			while (this.AcquireSendLock())
			{
				int num = 0;
				for (int i = 0; i < this.sendBufferList.Count; i++)
				{
					num += this.sendBufferList[i].Count;
				}
				ArraySegment<byte> arraySegment;
				while (this.sendBufferList.Count <= 62 && num < 8192 && this.sendQueue.TryDequeue(out arraySegment))
				{
					if (arraySegment.Count > 0)
					{
						if (arraySegment.Array == null)
						{
							try
							{
								using (FileStream fileStream = new FileStream("Core.log", FileMode.Append, FileAccess.Write))
								{
									using (StreamWriter streamWriter = new StreamWriter(fileStream))
									{
										streamWriter.Write("[{0}]", DateTime.Now);
										streamWriter.WriteLine("Error in AsyncTcpWorker.BeginSend. packet.Count > 0 but packet.Array is null.");
									}
								}
								continue;
							}
							catch
							{
								continue;
							}
						}
						num += arraySegment.Count;
						this.sendBufferList.Add(new ArraySegment<byte>(arraySegment.Array, arraySegment.Offset, arraySegment.Count));
					}
				}
				if (this.sendBufferList.Count != 0)
				{
					SocketError socketError;
					try
					{
						if (!this.socket.Connected)
						{
							ConcurrentQueue<ArraySegment<byte>> value = new ConcurrentQueue<ArraySegment<byte>>();
							Interlocked.Exchange<ConcurrentQueue<ArraySegment<byte>>>(ref this.sendQueue, value);
							this.ReleaseSendLock();
							return;
						}
						this.totalSentCount++;
						this.socket.BeginSend(this.sendBufferList, SocketFlags.None, out socketError, new AsyncCallback(this.EndSend), null);
					}
					catch (ObjectDisposedException)
					{
						ConcurrentQueue<ArraySegment<byte>> value2 = new ConcurrentQueue<ArraySegment<byte>>();
						Interlocked.Exchange<ConcurrentQueue<ArraySegment<byte>>>(ref this.sendQueue, value2);
						this.ReleaseSendLock();
						return;
					}
					if (socketError != SocketError.Success && socketError != SocketError.IOPending)
					{
						this.OnException(new SocketException((int)socketError));
					}
					return;
				}
				if (this.ReleaseSendLock())
				{
					return;
				}
			}
		}

		private void EndSend(IAsyncResult ar)
		{
			SocketError socketError;
			int num;
			try
			{
				if (!this.socket.Connected)
				{
					ConcurrentQueue<ArraySegment<byte>> value = new ConcurrentQueue<ArraySegment<byte>>();
					Interlocked.Exchange<ConcurrentQueue<ArraySegment<byte>>>(ref this.sendQueue, value);
					this.ReleaseSendLock();
					return;
				}
				num = this.socket.EndSend(ar, out socketError);
				this.totalSent += (long)num;
				int num2 = num;
				int i;
				for (i = 0; i < this.sendBufferList.Count; i++)
				{
					if (num2 < this.sendBufferList[i].Count)
					{
						this.sendBufferList[i] = new ArraySegment<byte>(this.sendBufferList[i].Array, this.sendBufferList[i].Offset + num2, this.sendBufferList[i].Count - num2);
						break;
					}
					num2 -= this.sendBufferList[i].Count;
				}
				this.sendBufferList.RemoveRange(0, i);
			}
			catch (ObjectDisposedException)
			{
				ConcurrentQueue<ArraySegment<byte>> value2 = new ConcurrentQueue<ArraySegment<byte>>();
				Interlocked.Exchange<ConcurrentQueue<ArraySegment<byte>>>(ref this.sendQueue, value2);
				this.ReleaseSendLock();
				return;
			}
			if (num == 0)
			{
				this.Shutdown();
				return;
			}
			if (socketError == SocketError.Success)
			{
				bool flag = this.sendQueue.IsEmpty && this.sendBufferList.Count == 0;
				if (!this.ReleaseSendLock() || !flag)
				{
					this.BeginSend();
					return;
				}
			}
			else
			{
				this.OnException(new SocketException((int)socketError));
			}
		}

		private void OnException(Exception exception)
		{
			SocketException ex = exception as SocketException;
			if ((ex == null || (ex.SocketErrorCode != SocketError.ConnectionAborted && ex.SocketErrorCode != SocketError.ConnectionReset)) && this.SocketException != null)
			{
				try
				{
					this.SocketException(this, new EventArgs<Exception>(exception));
				}
				catch
				{
				}
			}
			this.Shutdown();
		}

		private const int MaxSendCount = 8192;

		private const int MaxReceiveCount = 8192;

		private const int MaxSendBufferCount = 64;

		private int active;

		private int closed;

		private Socket socket;

		private byte[] remoteAddress;

		private int remortPort;

		private int sending;

		private ConcurrentQueue<ArraySegment<byte>> sendQueue;

		private long totalSent;

		private long totalReceived;

		private int totalSentCount;

		private int totalReceivedCount;

		private IPacketAnalyzer packetAnalyzer;

		private List<ArraySegment<byte>> sendBufferList;

		private byte[] receiveBuffer;
	}
}
