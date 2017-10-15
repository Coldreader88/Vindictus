using System;

namespace WcfChatRelay.Server.GuildChat
{
	public class ChatOpEventArg : EventArgs
	{
		public long GuildKey { get; set; }

		public long CID { get; set; }
	}
}
