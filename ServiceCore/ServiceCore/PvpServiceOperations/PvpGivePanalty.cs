using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class PvpGivePanalty : Operation
	{
		public int PlayerIndex { get; set; }

		public long CID { get; set; }

		public PvpGivePanalty(int playerIndex, long cid)
		{
			this.PlayerIndex = playerIndex;
			this.CID = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
