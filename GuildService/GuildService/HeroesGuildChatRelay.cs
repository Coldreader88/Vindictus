using System;
using System.Collections.Generic;
using WcfChatRelay.Server.GuildChat;

namespace GuildService
{
	public class HeroesGuildChatRelay : RelayClient
	{
		public IEnumerable<GuildChatWebMember> WebMembers
		{
			get
			{
				return this.webMembers.Values;
			}
		}

		public GuildChatWebMember this[long index]
		{
			get
			{
				if (this.webMembers.ContainsKey(index))
				{
					return this.webMembers[index];
				}
				return null;
			}
		}

		public HeroesGuildChatRelay(string uri, string name) : base(uri, name)
		{
			this.webMembers = new Dictionary<long, GuildChatWebMember>();
		}

		public void JoinWebMember(GuildChatWebMember member)
		{
			if (this.webMembers.ContainsKey(member.CID))
			{
				this.webMembers.Remove(member.CID);
			}
			this.webMembers.Add(member.CID, member);
		}

		public void LeaveWebMember(long CID)
		{
			this.webMembers.Remove(CID);
		}

		public void Clear()
		{
			this.webMembers.Clear();
		}

		private Dictionary<long, GuildChatWebMember> webMembers;
	}
}
