using System;
using System.Collections.Generic;
using ServiceCore.QuestOwnershipServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class BindTarget : Operation
	{
		public QuestDigest Target { get; set; }

		public BindTarget.FailReasonEnum FailReason
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
			return new BindTarget.Request(this);
		}

		[NonSerialized]
		private BindTarget.FailReasonEnum failReason;

		public enum FailReasonEnum
		{
			InvalidState,
			NoSectorInfo,
			Unknown
		}

		private class Request : OperationProcessor<BindTarget>
		{
			public Request(BindTarget op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.FailReason = BindTarget.FailReasonEnum.Unknown;
				if (!(base.Feedback is BindTarget.FailReasonEnum))
				{
					base.Result = (base.Feedback is OkMessage);
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = (BindTarget.FailReasonEnum)base.Feedback;
				}
				yield break;
			}
		}
	}
}
