using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildNoticeEditMessage : IMessage
	{
		public string Text { get; set; }

		public override string ToString()
		{
			return string.Format("GuildNoticeEditMessage[ {0} ]", this.Text);
		}
	}
}
