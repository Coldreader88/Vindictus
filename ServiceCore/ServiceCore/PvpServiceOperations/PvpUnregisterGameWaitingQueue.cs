using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public class PvpUnregisterGameWaitingQueue : Operation
	{
		public long CID { get; set; }

		public PvpUnregisterGameWaitingQueue(long cid)
		{
			this.CID = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
