using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRankByRankID : Operation
	{
		public string RankID { get; private set; }

		public int PeriodType { get; private set; }

		public IList<RankResultInfo> RankResults
		{
			get
			{
				return this.rankResults;
			}
		}

		public QueryRankByRankID(string RankID, int PeriodType)
		{
			this.RankID = RankID;
			this.PeriodType = PeriodType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRankByRankID.Request(this);
		}

		private IList<RankResultInfo> rankResults;

		private class Request : OperationProcessor<QueryRankByRankID>
		{
			public Request(QueryRankByRankID op) : base(op)
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
