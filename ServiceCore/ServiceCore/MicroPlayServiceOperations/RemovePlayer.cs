using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class RemovePlayer : Operation
	{
		public int SlotNumber { get; set; }

		public long CID { get; set; }

		public RemovePlayer(int slot, long cid)
		{
			this.SlotNumber = slot;
			this.CID = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
