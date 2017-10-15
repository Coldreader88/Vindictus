using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class SharedInventoryInfo
	{
		public ICollection<StorageInfo> StorageInfo { get; private set; }

		public ICollection<SlotInfo> InventorySlotInfo { get; private set; }

		public SharedInventoryInfo(ICollection<StorageInfo> storageInfo, ICollection<SlotInfo> inventoryInfo)
		{
			this.StorageInfo = storageInfo;
			this.InventorySlotInfo = inventoryInfo;
		}
	}
}
