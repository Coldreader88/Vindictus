using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class ReconnectGuildMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ReconnectGuildMessage[ ]", new object[0]);
		}
	}
}
