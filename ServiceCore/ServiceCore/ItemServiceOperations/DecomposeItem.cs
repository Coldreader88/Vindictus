using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DecomposeItem : Operation
	{
		public long ItemID { get; set; }

		public DecomposeItem(long itemID)
		{
			this.ItemID = itemID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
