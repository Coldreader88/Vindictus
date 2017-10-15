using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class QueryRankHeavyFavorites : Operation
	{
		public IList<RankResultInfo> RankResults
		{
			get
			{
				return this.rankResults;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryRankHeavyFavorites.Request(this);
		}

		[NonSerialized]
		private IList<RankResultInfo> rankResults;

		private class Request : OperationProcessor<QueryRankHeavyFavorites>
		{
			public Request(QueryRankHeavyFavorites op) : base(op)
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
