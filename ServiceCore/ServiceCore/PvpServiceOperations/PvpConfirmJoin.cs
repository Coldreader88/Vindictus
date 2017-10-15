using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class PvpConfirmJoin : Operation
	{
		public string PvpMode { get; set; }

		public long CID { get; set; }

		public PvpConfirmJoin(string pvpMode, long cid)
		{
			this.PvpMode = pvpMode;
			this.CID = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
