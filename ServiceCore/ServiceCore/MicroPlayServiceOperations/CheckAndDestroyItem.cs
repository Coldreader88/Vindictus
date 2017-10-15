using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class CheckAndDestroyItem : Operation
	{
		public long CID { get; set; }

		public long ItemID { get; set; }

		public CheckAndDestroyItem(long CID, long itemID)
		{
			this.CID = CID;
			this.ItemID = itemID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
