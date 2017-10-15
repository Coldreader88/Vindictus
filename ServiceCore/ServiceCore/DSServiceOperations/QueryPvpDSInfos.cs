using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class QueryPvpDSInfos : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new QueryPvpDSInfos.Request(this);
		}

		[NonSerialized]
		public Dictionary<int, DSInfo> DSInfoDict;

		private class Request : OperationProcessor<QueryPvpDSInfos>
		{
			public Request(QueryPvpDSInfos op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is Dictionary<int, DSInfo>)
				{
					base.Result = true;
					base.Operation.DSInfoDict = (base.Feedback as Dictionary<int, DSInfo>);
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
