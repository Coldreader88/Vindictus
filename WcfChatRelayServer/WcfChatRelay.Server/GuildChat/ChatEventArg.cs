using System;

namespace WcfChatRelay.Server.GuildChat
{
	public class ChatEventArg : ChatOpEventArg
	{
		public string Sender { get; set; }

		public string Message { get; set; }
	}
}
