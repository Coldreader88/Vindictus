using System;
using System.Collections.Generic;

namespace WcfChatRelay.Server.GuildChat
{
	public class ChatMemberSyncEventArg : EventArgs
	{
		public long GuildKey { get; set; }

		public IDictionary<long, string> Members { get; set; }
	}
}
