using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRankFavorites : Operation
	{
		public long CID { get; private set; }

		public int PeriodType { get; private set; }

		public long GID { get; private set; }

		public IList<RankResultInfo> RankResults
		{
			get
			{
				return this.rankResults;
			}
		}

		public QueryRankFavorites(long CID, int PeriodType, long GID)
		{
			this.CID = CID;
			this.PeriodType = PeriodType;
			this.GID = GID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRankFavorites.Request(this);
		}

		[NonSerialized]
		private IList<RankResultInfo> rankResults;

		private class Request : OperationProcessor<QueryRankFavorites>
		{
			public Request(QueryRankFavorites op) : base(op)
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
