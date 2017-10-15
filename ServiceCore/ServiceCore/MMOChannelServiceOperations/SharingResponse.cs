using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MMOChannelServiceOperations
{
	[Serializable]
	public sealed class SharingResponse : Operation
	{
		public long CID { get; set; }

		public bool Accept { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
