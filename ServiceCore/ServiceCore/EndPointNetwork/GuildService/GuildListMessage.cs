using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildListMessage : IMessage
	{
		public List<InGameGuildInfo> GuildList { get; set; }

		public int Page { get; set; }

		public int TotalPage { get; set; }

		public GuildListMessage(List<InGameGuildInfo> guildList, int page, int totalPage)
		{
			this.GuildList = guildList;
			this.Page = page;
			this.TotalPage = totalPage;
		}

		public override string ToString()
		{
			return string.Format("GuildListMessage[ {0} ]", this.GuildList.Count);
		}
	}
}
