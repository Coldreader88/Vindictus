using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class AddHousingPlayer : Operation
	{
		public long CID { get; set; }

		public int NexonSN { get; set; }

		public int SlotNumber { get; set; }

		public long FrontendID { get; set; }

		public AddHousingPlayer.FailReasonEnum FailReason
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
			return new AddHousingPlayer.Request(this);
		}

		[NonSerialized]
		private AddHousingPlayer.FailReasonEnum failReason;

		public enum FailReasonEnum
		{
			Unknown,
			NoSuchHousingplay,
			SlotNotEmpty,
			SlotNotEmpty2,
			P2PJoinFail,
			PlayerInfoFail,
			FullHousingPlay
		}

		private class Request : OperationProcessor<AddHousingPlayer>
		{
			public Request(AddHousingPlayer op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
				}
				else
				{
					base.Result = false;
					if (base.Feedback is AddHousingPlayer.FailReasonEnum)
					{
						base.Operation.FailReason = (AddHousingPlayer.FailReasonEnum)base.Feedback;
					}
					else
					{
						base.Operation.FailReason = AddHousingPlayer.FailReasonEnum.Unknown;
					}
				}
				yield break;
			}
		}
	}
}
