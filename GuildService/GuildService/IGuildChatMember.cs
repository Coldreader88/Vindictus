using System;

namespace GuildService
{
	public interface IGuildChatMember
	{
		long CID { get; }

		long GuildID { get; }

		string Sender { get; }
	}
}
