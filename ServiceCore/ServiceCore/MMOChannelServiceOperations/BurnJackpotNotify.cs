using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MMOChannelServiceOperations
{
	[Serializable]
	public sealed class BurnJackpotNotify : Operation
	{
		public long CID { get; set; }

		public BurnJackpotNotify(long cid)
		{
			this.CID = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
