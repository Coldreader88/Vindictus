using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRankByCharacterName : Operation
	{
		public string CharacterName { get; private set; }

		public int PeriodType { get; private set; }

		public string GuildName { get; private set; }

		public IList<RankResultInfo> RankResults
		{
			get
			{
				return this.rankResults;
			}
		}

		public QueryRankByCharacterName(string CharacterName, int PeriodType, string GuildName)
		{
			this.CharacterName = CharacterName;
			this.PeriodType = PeriodType;
			this.GuildName = GuildName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRankByCharacterName.Request(this);
		}

		[NonSerialized]
		private IList<RankResultInfo> rankResults;

		private class Request : OperationProcessor<QueryRankByCharacterName>
		{
			public Request(QueryRankByCharacterName op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IList<RankResultInfo>)
				{
					base.Operation.rankResults = (base.Feedback as IList<RankResultInfo>);
					base.Result = true;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
