using System;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;

namespace RemoteControlSystem.Server
{
	public class User
	{
		public TcpClient Connection { get; private set; }

		public event EventHandler<EventArgs> Disconnected;

		public event EventHandler<EventArgs<ArraySegment<byte>>> PacketReceived;

		public static User MutexOwner { get; private set; }

		public int ClientId { get; private set; }

		public string AccountId { get; private set; }

		public bool IsValid { get; private set; }

		public Authority Authority
		{
			get
			{
				return Base.Security.GetUserAuthority(this.AccountId);
			}
		}

		public User(int clientId, TcpClient tcpClient)
		{
			this.ClientId = clientId;
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
			this.IsValid = false;
		}

		public void Login(string _account)
		{
			this.AccountId = _account;
			this.IsValid = true;
		}

		public void Send<T>(T message)
		{
			this.Send(SerializeWriter.ToBinary<T>(message));
		}

		public void Send(Packet packet)
		{
			this.Connection.Transmit(packet);
		}

		public static User RequireMutex(User user)
		{
			User mutexOwner;
			lock (typeof(User))
			{
				if (User.MutexOwner == null)
				{
					User.MutexOwner = user;
					User._mutexCounter = 1;
				}
				else if (User.MutexOwner == user)
				{
					User._mutexCounter++;
				}
				mutexOwner = User.MutexOwner;
			}
			return mutexOwner;
		}

		public static bool ReleaseMutex(User user)
		{
			bool result;
			lock (typeof(User))
			{
				if (User.MutexOwner != null && User.MutexOwner.ClientId == user.ClientId)
				{
					if (--User._mutexCounter == 0)
					{
						User.MutexOwner = null;
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public static bool ReleaseMutexAll(User user)
		{
			bool result;
			lock (typeof(User))
			{
				if (User.MutexOwner != null && User.MutexOwner.ClientId == user.ClientId)
				{
					User.MutexOwner = null;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		private static int _mutexCounter;
	}
}
