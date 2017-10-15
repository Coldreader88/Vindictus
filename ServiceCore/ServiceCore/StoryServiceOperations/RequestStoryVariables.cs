using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class RequestStoryVariables : Operation
	{
		public IDictionary<string, int> Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RequestStoryVariables.Request(this);
		}

		[NonSerialized]
		public IDictionary<string, int> status;

		private class Request : OperationProcessor<RequestStoryVariables>
		{
			public Request(RequestStoryVariables op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IDictionary<string, int>)
				{
					base.Operation.Status = (base.Feedback as IDictionary<string, int>);
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
