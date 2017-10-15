using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ChannelServiceOperations
{
	[Serializable]
	public class RecommendChannel : Operation
	{
		public long CID { get; set; }

		public long RegionCode { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new RecommendChannel.Request(this);
		}

		public class Request : OperationProcessor<RecommendChannel>
		{
			public Request(RecommendChannel op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.CID = (long)base.Feedback;
				yield break;
			}
		}
	}
}
