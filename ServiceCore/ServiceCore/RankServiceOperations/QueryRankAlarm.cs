using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRankAlarm : Operation
	{
		public long CID { get; private set; }

		public long GID { get; private set; }

		public IList<RankAlarmInfo> RankAlarms
		{
			get
			{
				return this.rankAlarms;
			}
		}

		public QueryRankAlarm(long CID, long GID)
		{
			this.CID = CID;
			this.GID = GID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRankAlarm.Request(this);
		}

		[NonSerialized]
		private IList<RankAlarmInfo> rankAlarms;

		private class Request : OperationProcessor<QueryRankAlarm>
		{
			public Request(QueryRankAlarm op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IList<RankAlarmInfo>)
				{
					base.Operation.rankAlarms = (base.Feedback as IList<RankAlarmInfo>);
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
