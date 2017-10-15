using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class NotifyStoryEvent : Operation
	{
		public StoryEventType type { get; set; }

		public string Object { get; set; }

		public bool IsSuccess { get; set; }

		public int StorySectorID
		{
			get
			{
				return this.storySectorID;
			}
			set
			{
				this.storySectorID = value;
			}
		}

		public NotifyStoryEvent(StoryEventType type, string Object, bool IsSuccess)
		{
			this.type = type;
			this.Object = Object;
			this.IsSuccess = IsSuccess;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NotifyStoryEvent.Request(this);
		}

		[NonSerialized]
		private int storySectorID;

		private class Request : OperationProcessor<NotifyStoryEvent>
		{
			public Request(NotifyStoryEvent op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Operation.StorySectorID = (int)base.Feedback;
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
