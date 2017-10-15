using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class UnregisterPvpPlayer : Operation
	{
		public long CID { get; set; }

		public UnregisterPvpPlayer(long CID)
		{
			this.CID = CID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
