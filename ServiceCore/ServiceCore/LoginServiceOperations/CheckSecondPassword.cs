using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.LoginServiceOperations
{
	[Serializable]
	public sealed class CheckSecondPassword : Operation
	{
		public string Password { get; set; }

		public SecondPasswordResultMessage ResultMessage
		{
			get
			{
				return new SecondPasswordResultMessage(this.OperationType, this.Passed, this.FailCount, this.RetryLockedSec);
			}
		}

		public CheckSecondPassword(string password)
		{
			this.Password = password;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CheckSecondPassword.Request(this);
		}

		[NonSerialized]
		public SecondPasswordResultMessage.ProcessType OperationType;

		[NonSerialized]
		public bool Passed;

		[NonSerialized]
		public int FailCount;

		[NonSerialized]
		public int RetryLockedSec;

		private class Request : OperationProcessor<CheckSecondPassword>
		{
			public Request(CheckSecondPassword op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.OperationType = (SecondPasswordResultMessage.ProcessType)base.Feedback;
					yield return null;
					base.Operation.Passed = (bool)base.Feedback;
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
