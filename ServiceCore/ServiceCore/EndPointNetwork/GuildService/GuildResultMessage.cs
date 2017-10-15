using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildResultMessage : IMessage
	{
		public int Result { get; set; }

		public string Arg { get; set; }

		public long GuildID { get; set; }

		public GuildResultMessage(GuildResultEnum result, string arg)
		{
			this.Result = (int)result;
			this.Arg = arg;
			this.GuildID = 0L;
		}

		public GuildResultMessage(GuildResultEnum result)
		{
			this.Result = (int)result;
			this.Arg = "";
			this.GuildID = 0L;
		}

		public GuildResultMessage(GuildResultEnum result, long guildID)
		{
			this.Result = (int)result;
			this.Arg = "";
			this.GuildID = guildID;
		}

		public override string ToString()
		{
			return string.Format("GuildResultMessage[ {0} {1} ]", (GuildResultEnum)this.Result, this.Arg);
		}
	}
}
