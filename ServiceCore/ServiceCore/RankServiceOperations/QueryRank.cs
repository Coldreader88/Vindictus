using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRank : Operation
	{
		public int PeriodType { get; private set; }

		public int RankLimit { get; private set; }

		public string CharacterName
		{
			get
			{
				return this.characterName;
			}
		}

		public IList<RankResultInfo> RankResults
		{
			get
			{
				return this.rankResults;
			}
		}

		public QueryRank(int PeriodType, int RankLimit)
		{
			this.PeriodType = PeriodType;
			this.RankLimit = RankLimit;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRank.Request(this);
		}

		[NonSerialized]
		private string characterName;

		private IList<RankResultInfo> rankResults;

		private class Request : OperationProcessor<QueryRank>
		{
			public Request(QueryRank op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IList<RankResultInfo>)
				{
					base.Operation.rankResults = (base.Feedback as IList<RankResultInfo>);
					yield return null;
					base.Operation.characterName = (base.Feedback as string);
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
