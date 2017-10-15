using System;
using System.Collections.Generic;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildInventoryInfoMessage : IMessage
	{
		public bool IsEnabled { get; private set; }

		public int StorageCount { get; private set; }

		public int GoldLimit { get; set; }

		public long AccessLimtiTag { get; set; }

		public ICollection<SlotInfo> SlotInfos { get; private set; }

		public GuildInventoryInfoMessage(bool isEnabled, int storageCount, int goldLimit, long accessLimit, ICollection<SlotInfo> slotInfos)
		{
			this.IsEnabled = isEnabled;
			this.StorageCount = storageCount;
			this.GoldLimit = goldLimit;
			this.AccessLimtiTag = accessLimit;
			this.SlotInfos = slotInfos;
		}

		public override string ToString()
		{
			return string.Format("GuildInventoryInfoMessage [ slotInfo x {0} ]", this.SlotInfos.Count);
		}
	}
}
