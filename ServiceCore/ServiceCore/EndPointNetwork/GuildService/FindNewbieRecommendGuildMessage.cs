using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public class FindNewbieRecommendGuildMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("FindNewbieRecommendGuildMessage", new object[0]);
		}
	}
}
