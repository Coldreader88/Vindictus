using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class QueryIsOTPEnabled : Operation
	{
		public bool IsOTP
		{
			get
			{
				return this.secured;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryIsOTPEnabled.Request(this);
		}

		[NonSerialized]
		private bool secured;

		private class Request : OperationProcessor<QueryIsOTPEnabled>
		{
			public Request(QueryIsOTPEnabled op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.secured = (base.Feedback is bool && (bool)base.Feedback);
				yield break;
			}
		}
	}
}
