using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class OpenGuild : Operation
	{
		public GuildMemberKey GuildMemberKey { get; set; }

		public string GuildName { get; set; }

		public string GuildNameID { get; set; }

		public string GuildIntro { get; set; }

		public OpenGuild(GuildMemberKey guildMemberKey, string guildName, string guildNameID, string guildIntro)
		{
			this.GuildMemberKey = guildMemberKey;
			this.GuildName = guildName;
			this.GuildNameID = guildNameID;
			this.GuildIntro = guildIntro;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OpenGuild.Request(this);
		}

		[NonSerialized]
		public long GuildID;

		[NonSerialized]
		public string ErrorType;

		private class Request : OperationProcessor<OpenGuild>
		{
			public Request(OpenGuild op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Result = true;
					base.Operation.GuildID = (long)base.Feedback;
					base.Operation.ErrorType = null;
				}
				else if (base.Feedback is string)
				{
					base.Result = false;
					base.Operation.GuildID = 0L;
					base.Operation.ErrorType = (string)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.GuildID = 0L;
					base.Operation.ErrorType = null;
				}
				yield break;
			}
		}
	}
}
