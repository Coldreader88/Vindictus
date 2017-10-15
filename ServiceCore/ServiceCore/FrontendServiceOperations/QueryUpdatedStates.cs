using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class QueryUpdatedStates : Operation
	{
		public ICollection<UpdatedStateElement> States
		{
			get
			{
				return this.states;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryUpdatedStates.Request(this);
		}

		[NonSerialized]
		private ICollection<UpdatedStateElement> states;

		private class Request : OperationProcessor<QueryUpdatedStates>
		{
			public Request(QueryUpdatedStates op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.states = (base.Feedback as List<UpdatedStateElement>);
				yield break;
			}
		}
	}
}
