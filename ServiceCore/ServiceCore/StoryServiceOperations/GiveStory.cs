using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class GiveStory : Operation
	{
		public string StoryLineID { get; set; }

		public GiveStory.FailReasonEnum FailReason
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

		public GiveStory(string StoryLineID)
		{
			this.StoryLineID = StoryLineID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new GiveStory.Request(this);
		}

		[NonSerialized]
		private GiveStory.FailReasonEnum failReason;

		public enum FailReasonEnum : byte
		{
			Success = 255,
			Unknown = 0,
			NoCharacter,
			AlreadyOwned,
			NoSuchStory,
			SubmitError,
			AlreadyCompleted,
			InvalidCharacter
		}

		private class Request : OperationProcessor<GiveStory>
		{
			public Request(GiveStory op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
					base.Operation.FailReason = GiveStory.FailReasonEnum.Success;
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = (GiveStory.FailReasonEnum)base.Feedback;
				}
				yield break;
			}
		}
	}
}
