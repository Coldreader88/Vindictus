using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class DailyStoryProcess : Operation
	{
		public DateTime NextOpTime
		{
			get
			{
				return this.nextOp;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new DailyStoryProcess.Request(this);
		}

		[NonSerialized]
		private DateTime nextOp;

		private class Request : OperationProcessor<DailyStoryProcess>
		{
			public Request(DailyStoryProcess op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is DateTime)
				{
					base.Result = true;
					base.Operation.nextOp = (DateTime)base.Feedback;
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
