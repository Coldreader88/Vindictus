using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.LoginServiceOperations
{
	[Serializable]
	public sealed class QueryNexonSN : Operation
	{
		public string NexonID { get; set; }

		public int NexonSN
		{
			get
			{
				return this.nexonSN;
			}
			private set
			{
				this.nexonSN = value;
			}
		}

		public QueryNexonSN(string nexonID)
		{
			this.NexonID = nexonID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryNexonSN.Request(this);
		}

		[NonSerialized]
		private int nexonSN;

		private class Request : OperationProcessor<QueryNexonSN>
		{
			public Request(QueryNexonSN op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is int)
				{
					base.Result = true;
					base.Operation.NexonSN = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.NexonSN = -1;
				}
				yield break;
			}
		}
	}
}
