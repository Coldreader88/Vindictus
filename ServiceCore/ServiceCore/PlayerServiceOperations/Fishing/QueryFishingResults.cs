using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PlayerServiceOperations.Fishing
{
	[Serializable]
	public sealed class QueryFishingResults : Operation
	{
		public IList<FishingResultInfo> FishResult
		{
			get
			{
				return this.fishResult;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryFishingResults.Request(this);
		}

		[NonSerialized]
		private IList<FishingResultInfo> fishResult;

		private class Request : OperationProcessor<QueryFishingResults>
		{
			public Request(QueryFishingResults op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IList<FishingResultInfo>)
				{
					base.Result = true;
					base.Operation.fishResult = (base.Feedback as IList<FishingResultInfo>);
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
