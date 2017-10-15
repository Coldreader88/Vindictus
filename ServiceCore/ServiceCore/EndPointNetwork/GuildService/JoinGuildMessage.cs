using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class JoinGuildMessage : IMessage
	{
		public int GuildSN { get; set; }

		public override string ToString()
		{
			return string.Format("JoinGuildMessage[ {0} ]", this.GuildSN);
		}
	}
}
