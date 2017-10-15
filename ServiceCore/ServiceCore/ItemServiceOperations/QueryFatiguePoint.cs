using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryFatiguePoint : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new QueryFatiguePoint.Request(this);
		}

		[NonSerialized]
		public int FatiguePoint;

		private class Request : OperationProcessor<QueryFatiguePoint>
		{
			public Request(QueryFatiguePoint op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.FatiguePoint = (int)base.Feedback;
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
