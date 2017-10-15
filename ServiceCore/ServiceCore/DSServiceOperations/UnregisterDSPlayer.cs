using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class UnregisterDSPlayer : Operation
	{
		public long CID { get; set; }

		public bool ByUser { get; set; }

		public UnregisterDSPlayer(long cid, bool byUser)
		{
			this.CID = cid;
			this.ByUser = byUser;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
