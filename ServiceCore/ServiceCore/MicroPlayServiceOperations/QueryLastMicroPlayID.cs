using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class QueryLastMicroPlayID : Operation
	{
		public long CID { get; set; }

		public long MicroPlayID
		{
			get
			{
				return this.microPlayID;
			}
		}

		public long PartyID
		{
			get
			{
				return this.partyID;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryLastMicroPlayID.Request(this);
		}

		[NonSerialized]
		private long microPlayID;

		[NonSerialized]
		private long partyID;

		private class Request : OperationProcessor<QueryLastMicroPlayID>
		{
			public Request(QueryLastMicroPlayID op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Operation.microPlayID = (long)base.Feedback;
					yield return null;
					base.Operation.partyID = (long)base.Feedback;
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
