using System;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TransferItemInfo
	{
		public long ItemID { get; set; }

		public string ItemClass { get; set; }

		public int ItemCount { get; set; }

		public TransferItemInfo(string ItemClass, int ItemCount)
		{
			this.ItemID = -1L;
			this.ItemClass = ItemClass;
			this.ItemCount = ItemCount;
		}

		public TransferItemInfo(long itemID, string itemClass, int itemCount)
		{
			this.ItemID = itemID;
			this.ItemClass = itemClass;
			this.ItemCount = itemCount;
		}
	}
}
