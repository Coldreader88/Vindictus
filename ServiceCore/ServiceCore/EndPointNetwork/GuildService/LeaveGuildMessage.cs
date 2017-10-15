using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class LeaveGuildMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("LeaveGuildMessage[ ]", new object[0]);
		}
	}
}
