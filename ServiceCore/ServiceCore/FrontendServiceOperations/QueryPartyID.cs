using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class QueryPartyID : Operation
	{
		public long CID { get; set; }

		public QueryPartyID(long cid)
		{
			this.CID = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryPartyID.Request(this);
		}

		[NonSerialized]
		public long PartyID;

		[NonSerialized]
		public long MicroPlayID;

		private class Request : OperationProcessor<QueryPartyID>
		{
			public Request(QueryPartyID op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Operation.PartyID = (long)base.Feedback;
					yield return null;
					base.Operation.MicroPlayID = (long)base.Feedback;
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
