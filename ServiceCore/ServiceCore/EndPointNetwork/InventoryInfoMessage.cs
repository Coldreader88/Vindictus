using System;
using System.Collections.Generic;
using ServiceCore.ItemServiceOperations;
using Utility;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class InventoryInfoMessage : IMessage
	{
		public ICollection<StorageInfo> StorageInfos { get; private set; }

		public ICollection<SlotInfo> SlotInfos { get; private set; }

		public IDictionary<int, long> EquipmentInfo { get; private set; }

		public QuickSlotInfo QuickSlotInfo { get; private set; }

		public ICollection<int> UnequippableParts { get; private set; }

		public InventoryInfoMessage(ICollection<StorageInfo> storages, ICollection<SlotInfo> infos, IDictionary<int, long> equips, QuickSlotInfo qinfo, ICollection<int> unequippableParts)
		{
			Log<InventoryInfoMessage>.Logger.DebugFormat("inventory size : [{0}, {1}, {2}, {3}]", new object[]
			{
				storages.Count,
				infos.Count,
				equips.Count,
				qinfo.ToString()
			});
			this.StorageInfos = storages;
			this.SlotInfos = infos;
			this.EquipmentInfo = equips;
			this.QuickSlotInfo = qinfo;
			this.UnequippableParts = unequippableParts;
		}

		public override string ToString()
		{
			return string.Format("InventoryInfoMessage [ slotInfo x {0} equipmentInfo x {1}]", this.SlotInfos.Count, this.EquipmentInfo.Count);
		}
	}
}
