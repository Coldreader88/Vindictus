using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class QueryPartyCount : Operation
	{
		public Dictionary<string, int> PartyCount
		{
			get
			{
				return this.result;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryPartyCount.Request(this);
		}

		[NonSerialized]
		private Dictionary<string, int> result;

		private class Request : OperationProcessor<QueryPartyCount>
		{
			public Request(QueryPartyCount op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is Dictionary<string, int>)
				{
					base.Operation.result = (base.Feedback as Dictionary<string, int>);
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
