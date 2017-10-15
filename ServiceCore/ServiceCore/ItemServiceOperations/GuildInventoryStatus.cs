using System;
using System.Collections.Generic;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class GuildInventoryStatus
	{
		public int SlotCount { get; set; }

		public ICollection<SlotInfo> Inventory { get; set; }
	}
}
