using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class InventoryInfo
	{
		public ICollection<StorageInfo> StorageInfo { get; private set; }

		public ICollection<SlotInfo> InventorySlotInfo { get; private set; }

		public IDictionary<int, long> EquipmentInfo { get; private set; }

		public QuickSlotInfo QuickSlotInfo { get; private set; }

		public ICollection<int> UnequippableParts { get; private set; }

		public InventoryInfo(ICollection<StorageInfo> storageInfo, ICollection<SlotInfo> inventoryInfo, IDictionary<int, long> equipmentInfo, QuickSlotInfo quickSlotInfo, ICollection<int> unequippableParts)
		{
			this.StorageInfo = storageInfo;
			this.InventorySlotInfo = inventoryInfo;
			this.EquipmentInfo = equipmentInfo;
			this.QuickSlotInfo = quickSlotInfo;
			this.UnequippableParts = unequippableParts;
		}
	}
}
