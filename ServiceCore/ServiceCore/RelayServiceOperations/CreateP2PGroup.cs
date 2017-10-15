using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RelayServiceOperations
{
	[Serializable]
	public sealed class CreateP2PGroup : Operation
	{
		public long[] FrontendIDs { get; set; }

		public string Category { get; set; }

		public long EntityID { get; set; }

		public long P2PGroupID
		{
			get
			{
				return this.p2pGroupID;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new CreateP2PGroup.Request(this);
		}

		[NonSerialized]
		private long p2pGroupID;

		private class Request : OperationProcessor<CreateP2PGroup>
		{
			public Request(CreateP2PGroup op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.p2pGroupID = (long)base.Feedback;
				yield break;
			}
		}
	}
}
