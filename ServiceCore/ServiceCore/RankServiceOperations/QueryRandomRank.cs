using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRandomRank : Operation
	{
		public IList<RandomRankResultInfo> RandomRankResults
		{
			get
			{
				return this.randomRankResults;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRandomRank.Request(this);
		}

		[NonSerialized]
		private IList<RandomRankResultInfo> randomRankResults;

		private class Request : OperationProcessor<QueryRandomRank>
		{
			public Request(QueryRandomRank op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IList<RandomRankResultInfo>)
				{
					base.Operation.randomRankResults = (base.Feedback as IList<RandomRankResultInfo>);
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
