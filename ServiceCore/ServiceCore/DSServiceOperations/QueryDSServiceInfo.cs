using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class QueryDSServiceInfo : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new QueryDSServiceInfo.Request(this);
		}

		[NonSerialized]
		public string message;

		private class Request : OperationProcessor<QueryDSServiceInfo>
		{
			public Request(QueryDSServiceInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is string)
				{
					base.Operation.message = (string)base.Feedback;
					base.Result = true;
				}
				else
				{
					base.Operation.message = "no Data";
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
