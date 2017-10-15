using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildStoryRequestMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("GuildStoryRequestMessage[ ]", new object[0]);
		}
	}
}
