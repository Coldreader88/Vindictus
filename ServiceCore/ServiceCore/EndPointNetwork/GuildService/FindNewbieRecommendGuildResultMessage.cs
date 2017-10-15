using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class FindNewbieRecommendGuildResultMessage : IMessage
	{
		public InGameGuildInfo info { get; set; }

		public FindNewbieRecommendGuildResultMessage(InGameGuildInfo guild)
		{
			this.info = guild;
		}

		public override string ToString()
		{
			return string.Format("{0}", this.info.ToString());
		}
	}
}
