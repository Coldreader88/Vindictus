using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class InviteGuildMessage : IMessage
	{
		public string Name { get; set; }

		public override string ToString()
		{
			return string.Format("InviteGuildMessage[ {0} ]", this.Name);
		}
	}
}
