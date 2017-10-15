using System;

namespace GuildService
{
	public class GuildChatWebMember : IGuildChatMember
	{
		public long CID { get; private set; }

		public long GuildID { get; private set; }

		public string Sender { get; private set; }

		public HeroesGuildChatRelay ChatRelay { get; set; }

		public GuildChatWebMember(long cid, long guildID, string sender, HeroesGuildChatRelay server)
		{
			this.CID = cid;
			this.GuildID = guildID;
			this.Sender = sender;
			this.ChatRelay = server;
		}
	}
}
