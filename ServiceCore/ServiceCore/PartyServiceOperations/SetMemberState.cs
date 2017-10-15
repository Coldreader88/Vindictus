using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class SetMemberState : Operation
	{
		public int SlotNum { get; set; }

		public ReadyState State { get; set; }

		public int ResultSlot
		{
			get
			{
				return this.resultSlot;
			}
		}

		public SetMemberState.FailReasonEnum FailReason
		{
			get
			{
				return this.failReason;
			}
			set
			{
				this.failReason = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new SetMemberState.Request(this);
		}

		[NonSerialized]
		private int resultSlot;

		[NonSerialized]
		private SetMemberState.FailReasonEnum failReason;

		public enum FailReasonEnum : byte
		{
			NullEntity,
			InvalidSlot,
			HostCannotChangeState,
			CannotReadyInTown,
			PartyFull,
			Unknown
		}

		private class Request : OperationProcessor<SetMemberState>
		{
			public Request(SetMemberState op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.resultSlot = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
					if (base.Feedback is SetMemberState.FailReasonEnum)
					{
						base.Operation.FailReason = (SetMemberState.FailReasonEnum)base.Feedback;
					}
					else
					{
						base.Operation.FailReason = SetMemberState.FailReasonEnum.Unknown;
					}
				}
				yield break;
			}
		}
	}
}
