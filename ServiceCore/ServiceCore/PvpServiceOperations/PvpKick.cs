using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class PvpKick : Operation
	{
		public long HostCID { get; set; }

		public int SlotNum { get; set; }

		public PvpKick(long hostCID, int slotNum)
		{
			this.HostCID = hostCID;
			this.SlotNum = slotNum;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
