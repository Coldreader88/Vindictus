using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class RequestNewbieRecommendGuildMessage : IMessage
	{
		public int GuildSN { get; set; }

		public override string ToString()
		{
			return string.Format("{0}", this.GuildSN.ToString());
		}
	}
}
