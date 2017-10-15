using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildInfoMessage : IMessage
	{
		public InGameGuildInfo GuildInfo { get; set; }

		public GuildInfoMessage(InGameGuildInfo info)
		{
			this.GuildInfo = info;
		}

		public override string ToString()
		{
			return string.Format("GuildInfoMessage[ {0} ]", this.GuildInfo);
		}
	}
}
