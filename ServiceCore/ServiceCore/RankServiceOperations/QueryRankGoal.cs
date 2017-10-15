using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRankGoal : Operation
	{
		public long CID { get; private set; }

		public int EventID { get; private set; }

		public int PeriodType { get; private set; }

		public string RivalName
		{
			get
			{
				return this.rivalName;
			}
		}

		public long RivalScore
		{
			get
			{
				return this.rivalScore;
			}
		}

		public int RivalRank
		{
			get
			{
				return this.rivalRank;
			}
		}

		public int RivalRankPrev
		{
			get
			{
				return this.rivalRankPrev;
			}
		}

		public QueryRankGoal(long CID, int EventID, int PeriodType)
		{
			this.CID = CID;
			this.EventID = EventID;
			this.PeriodType = PeriodType;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRankGoal.Request(this);
		}

		[NonSerialized]
		private string rivalName;

		private long rivalScore;

		private int rivalRank;

		private int rivalRankPrev;

		private class Request : OperationProcessor<QueryRankGoal>
		{
			public Request(QueryRankGoal op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is string)
				{
					base.Operation.rivalName = (base.Feedback as string);
					yield return null;
					base.Operation.rivalScore = (long)base.Feedback;
					yield return null;
					base.Operation.rivalRank = (int)base.Feedback;
					yield return null;
					base.Operation.rivalRankPrev = (int)base.Feedback;
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
