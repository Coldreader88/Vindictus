using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class CloseGuildMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("CloseGuildMessage[ ]", new object[0]);
		}
	}
}
