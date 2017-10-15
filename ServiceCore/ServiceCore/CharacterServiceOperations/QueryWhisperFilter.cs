using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryWhisperFilter : Operation
	{
		public IDictionary<string, int> WhisperFilter
		{
			get
			{
				return this.filter;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryWhisperFilter.Request(this);
		}

		[NonSerialized]
		private IDictionary<string, int> filter;

		private class Request : OperationProcessor<QueryWhisperFilter>
		{
			public Request(QueryWhisperFilter op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IDictionary<string, int>)
				{
					base.Result = true;
					base.Operation.filter = (base.Feedback as IDictionary<string, int>);
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
