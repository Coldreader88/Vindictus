using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRankByCID : Operation
	{
		public long CID { get; private set; }

		public int PeriodType { get; private set; }

		public long GID { get; private set; }

		public int RankLimit { get; private set; }

		public IList<RankResultInfo> RankResults
		{
			get
			{
				return this.rankResults;
			}
		}

		public IList<int> RankTitleList
		{
			get
			{
				return this.rankTitleList;
			}
		}

		public QueryRankByCID(long CID, int PeriodType, long GID, int RankLimit)
		{
			this.CID = CID;
			this.PeriodType = PeriodType;
			this.GID = GID;
			this.RankLimit = RankLimit;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRankByCID.Request(this);
		}

		[NonSerialized]
		private IList<RankResultInfo> rankResults;

		[NonSerialized]
		private IList<int> rankTitleList;

		private class Request : OperationProcessor<QueryRankByCID>
		{
			public Request(QueryRankByCID op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IList<RankResultInfo>)
				{
					base.Operation.rankResults = (base.Feedback as IList<RankResultInfo>);
					yield return null;
					base.Operation.rankTitleList = (base.Feedback as IList<int>);
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
