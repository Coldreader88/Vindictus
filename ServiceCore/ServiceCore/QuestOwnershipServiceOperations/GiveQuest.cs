using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class GiveQuest : Operation
	{
		public string QuestID { get; set; }

		public bool Reveal { get; set; }

		public GiveQuest.FailReasonEnum FailReason
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

		public GiveQuest(string questID)
		{
			this.QuestID = questID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GiveQuest.Request(this);
		}

		[NonSerialized]
		private GiveQuest.FailReasonEnum failReason;

		public enum FailReasonEnum : byte
		{
			Success = 255,
			Unknown = 0,
			NoCharacter,
			AlreadyOwned,
			NoSuchQuest,
			NotOwned,
			SubmitError
		}

		private class Request : OperationProcessor<GiveQuest>
		{
			public Request(GiveQuest op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
					base.Operation.FailReason = GiveQuest.FailReasonEnum.Success;
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = (GiveQuest.FailReasonEnum)base.Feedback;
				}
				yield break;
			}
		}
	}
}
