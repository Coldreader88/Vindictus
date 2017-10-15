using System;

namespace WcfChatRelay.Server.GuildChat
{
	public class ChatJoinEventArg : ChatOpEventArg
	{
		public string Sender { get; set; }

		public IAsyncResult AsyncResult { get; set; }

		public JoinCompleted Callback { get; set; }
	}
}
