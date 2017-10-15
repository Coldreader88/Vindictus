using System;
using System.ServiceModel;
using log4net;
using WcfChatRelay.GuildChat;

namespace WcfChatRelay.Server.GuildChat
{
	public class RelayClient
	{
		public string URI { get; private set; }

		public string Name { get; private set; }

		public ILog Logger { get; set; }

		public bool GracefullyClosed { get; set; }

		public event EventHandler Disconnected;

		public event EventHandler<ChatMemberSyncEventArg> MembersSync;

		public event EventHandler<ChatJoinEventArg> JoinRequested;

		public event EventHandler<ChatOpEventArg> LeaveRequested;

		public event EventHandler<ChatEventArg> ChatReceived;

		public event EventHandler WebClosed;

		public RelayClient(string uri, string name)
		{
			this.URI = uri;
			this.Name = name;
		}

		public bool ConnectToService()
		{
			ServiceCallback serviceCallback = new ServiceCallback();
			serviceCallback.MembersSync += delegate(object s, ChatMemberSyncEventArg e)
			{
				if (this.MembersSync != null)
				{
					this.MembersSync(this, e);
				}
			};
			serviceCallback.JoinRequested += delegate(object s, ChatJoinEventArg e)
			{
				if (this.JoinRequested != null)
				{
					this.JoinRequested(this, e);
				}
			};
			serviceCallback.LeaveRequested += delegate(object s, ChatOpEventArg e)
			{
				if (this.LeaveRequested != null)
				{
					this.LeaveRequested(this, e);
				}
			};
			serviceCallback.ChatReceived += delegate(object s, ChatEventArg e)
			{
				if (this.ChatReceived != null)
				{
					this.ChatReceived(this, e);
				}
			};
			ServiceCallback serviceCallback2 = serviceCallback;
			serviceCallback2.WebClosed = (EventHandler)Delegate.Combine(serviceCallback2.WebClosed, new EventHandler(delegate(object s, EventArgs e)
			{
				if (this.WebClosed != null)
				{
					this.WebClosed(this, e);
				}
			}));
			DuplexChannelFactory<IGuildChatService> duplexChannelFactory = new DuplexChannelFactory<IGuildChatService>(serviceCallback, new NetTcpBinding(SecurityMode.None, false), new EndpointAddress(this.URI));
			this.proxy = duplexChannelFactory.CreateChannel();
			IClientChannel clientChannel = this.proxy as IClientChannel;
			clientChannel.Closed += delegate(object s, EventArgs e)
			{
				if (this.Disconnected != null)
				{
					this.Disconnected(this, EventArgs.Empty);
				}
				this.proxy = null;
			};
			try
			{
				this.proxy.SubscribeService(this.Name);
			}
			catch (Exception ex)
			{
				if (this.Logger != null)
				{
					this.Logger.Error("Fail to connect chat relay server", ex);
				}
				return false;
			}
			return true;
		}

		public void SendChat(long guildKey, string sender, string message)
		{
			if (this.proxy != null)
			{
				try
				{
					this.proxy.ChatMessage(guildKey, sender, message);
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Error("Fail to close connection", ex);
					}
					IClientChannel clientChannel = this.proxy as IClientChannel;
					if (clientChannel.State == CommunicationState.Faulted)
					{
						clientChannel.Abort();
					}
				}
			}
		}

		public void RequestMemberInfos(long guildKey)
		{
			if (this.proxy != null)
			{
				try
				{
					this.proxy.RequestMemberInfos(guildKey);
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Error("Fail to close connection", ex);
					}
					IClientChannel clientChannel = this.proxy as IClientChannel;
					if (clientChannel.State == CommunicationState.Faulted)
					{
						clientChannel.Abort();
					}
				}
			}
		}

		public void SendMemberInfo(long guildKey, string sender, bool isOnline)
		{
			if (this.proxy != null)
			{
				try
				{
					this.proxy.MemberInfo(guildKey, sender, isOnline);
					return;
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Error("Fail to close connection", ex);
					}
					IClientChannel clientChannel = this.proxy as IClientChannel;
					if (clientChannel.State == CommunicationState.Faulted)
					{
						clientChannel.Abort();
					}
					return;
				}
			}
			if (this.Logger != null)
			{
				this.Logger.ErrorFormat("Fail to sendMemberInfo {0} {1} {2}", guildKey, sender, isOnline);
			}
		}

		public void KickMember(long guildKey, long cid)
		{
			if (this.proxy != null)
			{
				try
				{
					this.proxy.KickMember(guildKey, cid);
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Error("Fail to close connection", ex);
					}
					IClientChannel clientChannel = this.proxy as IClientChannel;
					if (clientChannel.State == CommunicationState.Faulted)
					{
						clientChannel.Abort();
					}
				}
			}
		}

		public void Ping()
		{
			if (this.proxy != null)
			{
				try
				{
					this.proxy.Ping();
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Error("Fail to Ping", ex);
					}
					IClientChannel clientChannel = this.proxy as IClientChannel;
					if (clientChannel.State == CommunicationState.Faulted)
					{
						clientChannel.Abort();
					}
				}
			}
		}

		public void Close()
		{
			if (this.proxy != null)
			{
				this.GracefullyClosed = true;
				try
				{
					IClientChannel clientChannel = this.proxy as IClientChannel;
					clientChannel.Close();
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Error("Fail to close connection", ex);
					}
				}
				this.proxy = null;
			}
		}

		private IGuildChatService proxy;
	}
}
