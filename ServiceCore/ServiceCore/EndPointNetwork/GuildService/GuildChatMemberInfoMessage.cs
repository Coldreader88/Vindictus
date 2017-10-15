using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildChatMemberInfoMessage : IMessage
	{
		public string Sender { get; set; }

		public bool IsOnline { get; set; }

		public override string ToString()
		{
			return string.Format("GuildChatMemberInfoMessage[ {0}:{1} ]", this.Sender, this.IsOnline);
		}
	}
}
