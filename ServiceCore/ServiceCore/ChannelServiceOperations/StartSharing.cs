using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ChannelServiceOperations
{
	[Serializable]
	public sealed class StartSharing : Operation
	{
		public SharingInfo SharingInfo { get; set; }

		public long CID { get; set; }

		public List<long> TargetsCID { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
