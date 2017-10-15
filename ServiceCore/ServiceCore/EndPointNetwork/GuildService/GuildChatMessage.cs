using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildChatMessage : IMessage
	{
		public string Sender { get; set; }

		public string Message { get; set; }

		public override string ToString()
		{
			return string.Format("GuildChatMessage[ {0}:{1} ]", this.Sender, this.Message);
		}
	}
}
