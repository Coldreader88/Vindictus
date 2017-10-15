using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PetAccessoryElement
	{
		public PetAccessoryElement(string itemClass, int slotOrder, int accessorySize, int remainTime)
		{
			this.ItemClass = itemClass;
			this.SlotOrder = slotOrder;
			this.AccessorySize = accessorySize;
			this.RemainingTime = remainTime;
		}

		public string ItemClass { get; set; }

		public int SlotOrder { get; set; }

		public int AccessorySize { get; set; }

		public int RemainingTime { get; set; }
	}
}
