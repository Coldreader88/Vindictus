using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class QueryGuildListMessage : IMessage
	{
		public int QueryType { get; set; }

		public string SearchKey { get; set; }

		public int Page { get; set; }

		public byte PageSize { get; set; }

		public override string ToString()
		{
			return string.Format("QueryGuildListMessage[ SearchKey = {0} ]", this.SearchKey);
		}
	}
}
