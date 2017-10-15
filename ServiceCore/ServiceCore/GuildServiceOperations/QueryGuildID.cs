using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class QueryGuildID : Operation
	{
		public GuildMemberKey GuildMemberKey { get; set; }

		public QueryGuildID(GuildMemberKey guildMemberKey)
		{
			this.GuildMemberKey = guildMemberKey;
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

		public override OperationProcessor RequestProcessor()
		{
			return new QueryGuildID.Request(this);
		}

		[NonSerialized]
		private BriefGuildInfo briefGuildInfo;

		private class Request : OperationProcessor<QueryGuildID>
		{
			public Request(QueryGuildID op) : base(op)
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
					int MaxMemberLimit = (int)base.Feedback;
					base.Operation.BriefGuildInfo = new BriefGuildInfo(guildID, guildName, guildLevel, rank, MaxMemberLimit);
				}
				else
				{
					base.Result = false;
					base.Operation.BriefGuildInfo = null;
				}
				yield break;
			}
		}
	}
}
