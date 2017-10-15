using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MMOChannelServiceOperations
{
	[Serializable]
	public class SyncLoad : Operation
	{
		public int ServiceID { get; set; }

		public long Load { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
