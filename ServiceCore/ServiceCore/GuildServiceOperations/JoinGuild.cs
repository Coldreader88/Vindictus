using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class JoinGuild : Operation
	{
		public JoinGuild(GuildMemberKey key)
		{
			this.Key = key;
		}

		public BriefGuildInfo BriefGuildInfo
		{
			get
			{
				return this.briefGuildInfo;
			}
			set
			{
				this.briefGuildInfo = value;
			}
		}

		public string ErrorType
		{
			get
			{
				return this.errorType;
			}
			set
			{
				this.errorType = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new JoinGuild.Request(this);
		}

		public GuildMemberKey Key;

		[NonSerialized]
		private BriefGuildInfo briefGuildInfo;

		[NonSerialized]
		private string errorType;

		private class Request : OperationProcessor<JoinGuild>
		{
			public Request(JoinGuild op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					long guildID = (long)base.Feedback;
					yield return null;
					string guildName = (string)base.Feedback;
					yield return null;
					int guildLevel = (int)base.Feedback;
					yield return null;
					GuildMemberRank rank = (GuildMemberRank)base.Feedback;
					yield return null;
					int maxMemberCount = (int)base.Feedback;
					base.Operation.BriefGuildInfo = new BriefGuildInfo(guildID, guildName, guildLevel, rank, maxMemberCount);
					base.Operation.ErrorType = "";
				}
				else if (base.Feedback is string)
				{
					base.Result = false;
					base.Operation.BriefGuildInfo = null;
					base.Operation.ErrorType = (string)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.BriefGuildInfo = null;
					base.Operation.ErrorType = "";
				}
				yield break;
			}
		}
	}
}
