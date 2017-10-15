using System;
using System.Collections.Generic;
using System.ServiceModel;
using WcfChatRelay.GuildChat;

namespace WcfChatRelay.Server.GuildChat
{
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, UseSynchronizationContext = false)]
	internal class ServiceCallback : IServiceCallback
	{
		public event EventHandler<ChatMemberSyncEventArg> MembersSync;

		public event EventHandler<ChatJoinEventArg> JoinRequested;

		public event EventHandler<ChatOpEventArg> LeaveRequested;

		public event EventHandler<ChatEventArg> ChatReceived;

		public void RefreshWebMember(long guildKey, IDictionary<long, string> members)
		{
			if (this.MembersSync != null)
			{
				ChatMemberSyncEventArg e = new ChatMemberSyncEventArg
				{
					GuildKey = guildKey,
					Members = members
				};
				this.MembersSync(this, e);
			}
		}

		private void JoinCallback(string result, IAsyncResult ar)
		{
			JoinAsyncResult joinAsyncResult = ar as JoinAsyncResult;
			if (joinAsyncResult != null)
			{
				joinAsyncResult.Result = result;
				joinAsyncResult.Complete();
			}
		}

		public IAsyncResult BeginJoinChatRoom(long guildKey, long cid, string sender, AsyncCallback callback, object asyncState)
		{
			JoinAsyncResult joinAsyncResult = new JoinAsyncResult(callback, asyncState);
			if (this.JoinRequested != null)
			{
				ChatJoinEventArg e = new ChatJoinEventArg
				{
					GuildKey = guildKey,
					CID = cid,
					Sender = sender,
					AsyncResult = joinAsyncResult,
					Callback = new JoinCompleted(this.JoinCallback)
				};
				this.JoinRequested(this, e);
				return joinAsyncResult;
			}
			IAsyncResult result;
			try
			{
				result = joinAsyncResult;
			}
			finally
			{
				joinAsyncResult.Complete();
			}
			return result;
		}

		public string EndJoinChatRoom(IAsyncResult result)
		{
			JoinAsyncResult joinAsyncResult = result as JoinAsyncResult;
			if (joinAsyncResult != null)
			{
				return joinAsyncResult.Result;
			}
			return null;
		}

		public void SendChat(long guildKey, long cid, string sender, string message)
		{
			if (this.ChatReceived != null)
			{
				ChatEventArg e = new ChatEventArg
				{
					GuildKey = guildKey,
					CID = cid,
					Sender = sender,
					Message = message
				};
				this.ChatReceived(this, e);
			}
		}

		public void LeaveChatRoom(long guildKey, long cid)
		{
			if (this.LeaveRequested != null)
			{
				ChatOpEventArg e = new ChatOpEventArg
				{
					CID = cid,
					GuildKey = guildKey
				};
				this.LeaveRequested(this, e);
			}
		}

		public void WebClose()
		{
			if (this.WebClosed != null)
			{
				this.WebClosed(this, EventArgs.Empty);
			}
		}

		public EventHandler WebClosed;
	}
}
