using System;
using System.Net;
using System.Net.Sockets;
using Devcat.Core.Threading;

namespace Devcat.Core.Net
{
	public abstract class TcpServerBase<TClient> : ITcpServer where TClient : TcpClient, new()
	{
		public event EventHandler<AcceptEventArgs> ClientAccept;

		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		public Action<int, string> DoOnHugeMessage { get; set; }

		public Action<string> WriteLogFunc { get; set; }

		public object Tag { get; set; }

		public IPEndPoint LocalEndPoint { get; private set; }

		public TcpServerBase()
		{
		}

		public void Start(JobProcessor jobProcessor, int port)
		{
			this.Start(jobProcessor, IPAddress.Any, port);
		}

		public void Start(JobProcessor jobProcessor, IPAddress bindAddress, int port)
		{
			if (this.active)
			{
				throw new InvalidOperationException("Already activated.");
			}
			this.active = true;
			this.jobProcessor = jobProcessor;
			this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.socket.Bind(new IPEndPoint(bindAddress, port));
			this.socket.Listen(200);
			this.socket.BeginAccept(0, new AsyncCallback(this.EndAccept), this.socket);
			this.LocalEndPoint = new IPEndPoint((bindAddress == IPAddress.Any) ? TcpServerBase<TClient>.GetLocalIP(ServerBindType.PublicPrivate) : bindAddress, (this.socket.LocalEndPoint as IPEndPoint).Port);
		}

		public void Start(JobProcessor jobProcessor, ServerBindType bindType, int port)
		{
			this.Start(jobProcessor, TcpServerBase<TClient>.GetLocalIP(bindType), port);
		}

		public void Stop()
		{
			if (this.socket != null)
			{
				this.socket.Close();
				this.socket = null;
			}
		}

		protected virtual void OnClientAccept(AcceptEventArgs e)
		{
			if (this.ClientAccept != null)
			{
				this.ClientAccept(this, e);
			}
		}

		protected virtual void OnExceptionOccur(EventArgs<Exception> e)
		{
			if (this.ExceptionOccur != null)
			{
				this.ExceptionOccur(this, e);
			}
		}

		private void EndAccept(IAsyncResult ar)
		{
			Socket socket = null;
			try
			{
				socket = (ar.AsyncState as Socket);
				if (this.socket != socket)
				{
					try
					{
						socket.EndAccept(ar).Close();
					}
					catch
					{
					}
				}
				else
				{
					Socket arg = null;
					try
					{
						arg = socket.EndAccept(ar);
					}
					catch (ObjectDisposedException)
					{
					}
					catch (Exception value)
					{
						if (this.jobProcessor == null)
						{
							this.OnExceptionOccur(new EventArgs<Exception>(value));
						}
						else
						{
							this.jobProcessor.Enqueue(Job.Create<EventArgs<Exception>>(new Action<EventArgs<Exception>>(this.OnExceptionOccur), new EventArgs<Exception>(value)));
						}
					}
					if (socket != null)
					{
						socket.BeginAccept(0, new AsyncCallback(this.EndAccept), socket);
					}
					if (this.jobProcessor == null)
					{
						this.ProcessAccept(arg);
					}
					else
					{
						this.jobProcessor.Enqueue(Job.Create<Socket>(new Action<Socket>(this.ProcessAccept), arg));
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception value2)
			{
				if (this.jobProcessor == null)
				{
					this.OnExceptionOccur(new EventArgs<Exception>(value2));
				}
				else
				{
					this.jobProcessor.Enqueue(Job.Create<EventArgs<Exception>>(new Action<EventArgs<Exception>>(this.OnExceptionOccur), new EventArgs<Exception>(value2)));
				}
			}
		}

		private void ProcessAccept(Socket socket)
		{
			TcpClient tcpClient = Activator.CreateInstance<TClient>();
			AcceptEventArgs acceptEventArgs = new AcceptEventArgs(tcpClient);
			try
			{
				this.OnClientAccept(acceptEventArgs);
				if (acceptEventArgs.Cancel)
				{
					socket.Close();
				}
				else
				{
					if (acceptEventArgs.JobProcessor == null)
					{
						acceptEventArgs.JobProcessor = this.jobProcessor;
					}
					if (this.DoOnHugeMessage != null)
					{
						acceptEventArgs.PacketAnalyzer.OnHugeMessageReceive += this.DoOnHugeMessage;
					}
					if (this.WriteLogFunc != null)
					{
						acceptEventArgs.PacketAnalyzer.WriteLog += this.WriteLogFunc;
					}
					tcpClient.Activate(socket, acceptEventArgs.JobProcessor, acceptEventArgs.PacketAnalyzer);
				}
			}
			catch (Exception value)
			{
				socket.Close();
				this.OnExceptionOccur(new EventArgs<Exception>(value));
			}
		}

		private static bool IsPrivateIP(IPAddress ip)
		{
			if (ip.AddressFamily != AddressFamily.InterNetwork)
			{
				throw new ArgumentException("Only IPv4 supported for private ip addresses.", "ip");
			}
			int num = BitConverter.ToInt32(ip.GetAddressBytes(), 0);
			for (int i = 0; i <= TcpServerBase<TClient>.PrivateIPInformation.GetUpperBound(0); i++)
			{
				int num2 = TcpServerBase<TClient>.PrivateIPInformation[i, 0];
				int num3 = TcpServerBase<TClient>.PrivateIPInformation[i, 1];
				if ((num & num3) == num2)
				{
					return true;
				}
			}
			return false;
		}

        public static IPAddress GetLocalIP(ServerBindType bindType)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    switch (bindType)
                    {
                        case ServerBindType.Any:
                            return address;

                        case ServerBindType.Public:
                        case ServerBindType.PublicPrivate:
                            if (TcpServerBase<TClient>.IsPrivateIP(address))
                            {
                                break;
                            }
                            return address;

                        case ServerBindType.Private:
                        case ServerBindType.PrivatePublic:
                            if (!TcpServerBase<TClient>.IsPrivateIP(address))
                            {
                                break;
                            }
                            return address;
                    }
                }
            }
            foreach (IPAddress address3 in hostEntry.AddressList)
            {
                if (address3.AddressFamily == AddressFamily.InterNetwork)
                {
                    switch (bindType)
                    {
                        case ServerBindType.PublicPrivate:
                            if (!TcpServerBase<TClient>.IsPrivateIP(address3))
                            {
                                break;
                            }
                            return address3;

                        case ServerBindType.PrivatePublic:
                            if (TcpServerBase<TClient>.IsPrivateIP(address3))
                            {
                                break;
                            }
                            return address3;
                    }
                }
            }
            throw new SocketException(10049);
        }

		private static readonly int[,] PrivateIPInformation = new int[,]
		{
			{
				10,
				255
			},
			{
				4268,
				61695
			},
			{
				43200,
				65535
			},
			{
				65193,
				65535
			}
		};

		private bool active;

		private Socket socket;

		private JobProcessor jobProcessor;
	}
}
