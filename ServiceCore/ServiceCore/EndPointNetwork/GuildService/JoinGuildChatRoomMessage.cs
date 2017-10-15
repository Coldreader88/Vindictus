using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class JoinGuildChatRoomMessage : IMessage
	{
		public long GuildKey { get; set; }

		public override string ToString()
		{
			return string.Format("JoinGuildChatRoomMessage[ {0} ]", this.GuildKey);
		}
	}
}
