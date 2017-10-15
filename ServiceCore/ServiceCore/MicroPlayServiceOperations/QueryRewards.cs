using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class QueryRewards : Operation
	{
		public IDictionary<long, int> RewardExps
		{
			get
			{
				return this.dicRewardExp;
			}
		}

		public IDictionary<long, IList<byte>> ClearedSectors
		{
			get
			{
				return this.dicCleardSectors;
			}
		}

		public long HostCID
		{
			get
			{
				return this.hostcid;
			}
		}

		public QueryRewards(long cid)
		{
			this.hostcid = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRewards.Request(this);
		}

		private long hostcid;

		[NonSerialized]
		private IDictionary<long, int> dicRewardExp;

		[NonSerialized]
		private IDictionary<long, IList<byte>> dicCleardSectors;

		private class Request : OperationProcessor<QueryRewards>
		{
			public Request(QueryRewards op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.dicRewardExp = (base.Feedback as IDictionary<long, int>);
				if (base.Operation.dicRewardExp == null)
				{
					base.Result = false;
				}
				else
				{
					yield return null;
					base.Operation.dicCleardSectors = (base.Feedback as IDictionary<long, IList<byte>>);
				}
				yield break;
			}
		}
	}
}
