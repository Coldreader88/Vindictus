using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class OpenGuildMessage : IMessage
	{
		public string GuildName { get; set; }

		public string GuildNameID { get; set; }

		public string GuildIntro { get; set; }

		public override string ToString()
		{
			return string.Format("OpenGuildMessage[ GuildName=/{0}/ GuildNameID=/{1}/ ]", this.GuildName, this.GuildNameID);
		}
	}
}
