using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildMemberListMessage : IMessage
	{
		public bool IsFullUpdate { get; set; }

		public List<GuildMemberInfo> Members { get; set; }

		public GuildMemberListMessage(bool isFullUpdate, List<GuildMemberInfo> members)
		{
			this.IsFullUpdate = isFullUpdate;
			this.Members = members;
		}

		public override string ToString()
		{
			return string.Format("GuildMemberListMessage[ Members = {0} ]", this.Members.Count);
		}
	}
}
