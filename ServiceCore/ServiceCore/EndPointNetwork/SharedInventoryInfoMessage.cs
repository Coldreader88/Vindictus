using System;
using System.Collections.Generic;
using ServiceCore.ItemServiceOperations;
using Utility;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SharedInventoryInfoMessage : IMessage
	{
		public ICollection<StorageInfo> StorageInfos { get; private set; }

		public ICollection<SlotInfo> SlotInfos { get; private set; }

		public SharedInventoryInfoMessage(ICollection<StorageInfo> storages, ICollection<SlotInfo> infos)
		{
			Log<InventoryInfoMessage>.Logger.DebugFormat("Shared inventory size : [{0}, {1}]", storages.Count, infos.Count);
			this.StorageInfos = storages;
			this.SlotInfos = infos;
		}

		public override string ToString()
		{
			return string.Format("SharedInventoryInfoMessage [ slotInfo x {0}]", this.SlotInfos.Count);
		}
	}
}
