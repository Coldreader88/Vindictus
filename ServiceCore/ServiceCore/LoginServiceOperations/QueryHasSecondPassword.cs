using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.LoginServiceOperations
{
	[Serializable]
	public sealed class QueryHasSecondPassword : Operation
	{
		public HasSecondPasswordMessage ResultMessage(bool isPassed)
		{
			return new HasSecondPasswordMessage(this.IsFirstQuery, this.HasSecondPassword, isPassed, this.FailCount, this.RetryLockedSec);
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryHasSecondPassword.Request(this);
		}

		[NonSerialized]
		public bool IsFirstQuery;

		[NonSerialized]
		public bool HasSecondPassword;

		[NonSerialized]
		public int FailCount;

		[NonSerialized]
		public int RetryLockedSec;

		private class Request : OperationProcessor<QueryHasSecondPassword>
		{
			public Request(QueryHasSecondPassword op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is bool)
				{
					base.Result = true;
					base.Operation.IsFirstQuery = (bool)base.Feedback;
					yield return null;
					base.Operation.HasSecondPassword = (bool)base.Feedback;
					yield return null;
					base.Operation.FailCount = (int)base.Feedback;
					yield return null;
					base.Operation.RetryLockedSec = (int)base.Feedback;
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
