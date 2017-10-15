using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.LoginServiceOperations
{
	[Serializable]
	public sealed class QueryFriendRecommendFlag : Operation
	{
		public int NexonSN { get; set; }

		public int RecommendFlag { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new QueryFriendRecommendFlag.Request(this);
		}

		private class Request : OperationProcessor<QueryFriendRecommendFlag>
		{
			public Request(QueryFriendRecommendFlag op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.RecommendFlag = (int)base.Feedback;
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
