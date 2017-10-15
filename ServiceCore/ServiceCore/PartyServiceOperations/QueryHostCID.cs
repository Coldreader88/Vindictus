using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class QueryHostCID : Operation
	{
		public List<long> AvailableHosts { get; set; }

		public QueryHostCID(List<long> availableHosts)
		{
			this.AvailableHosts = availableHosts;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryHostCID.Request(this);
		}

		[NonSerialized]
		public long HostCID;

		private class Request : OperationProcessor<QueryHostCID>
		{
			public Request(QueryHostCID op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Operation.HostCID = (long)base.Feedback;
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
