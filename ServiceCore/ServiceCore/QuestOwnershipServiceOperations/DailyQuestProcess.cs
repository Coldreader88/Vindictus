using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class DailyQuestProcess : Operation
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
			return new DailyQuestProcess.Request(this);
		}

		[NonSerialized]
		private DateTime nextOp;

		private class Request : OperationProcessor<DailyQuestProcess>
		{
			public Request(DailyQuestProcess op) : base(op)
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
