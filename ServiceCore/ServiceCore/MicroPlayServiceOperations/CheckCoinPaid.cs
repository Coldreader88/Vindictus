using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class CheckCoinPaid : Operation
	{
		public long HostSlotNum { get; set; }

		public CheckCoinPaid.FailReason Reason { get; set; }

		public string SuspectName { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new CheckCoinPaid.Request(this);
		}

		public enum FailReason
		{
			UNKNOWN,
			NOT_ENOUGH_COIN,
			CANNOT_REMOVE_COIN,
			ITEM_REQUIRED
		}

		private class Request : OperationProcessor<CheckCoinPaid>
		{
			public Request(CheckCoinPaid op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
				}
				else if (base.Feedback is CheckCoinPaid.FailReason)
				{
					base.Result = false;
					base.Operation.Reason = (CheckCoinPaid.FailReason)base.Feedback;
					yield return null;
					base.Operation.SuspectName = (string)base.Feedback;
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
