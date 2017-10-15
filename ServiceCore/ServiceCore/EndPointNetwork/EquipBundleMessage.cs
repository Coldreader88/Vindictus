using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EquipBundleMessage : IMessage
	{
		public string ItemClass { get; set; }

		public int QuickSlotID { get; set; }

		public EquipBundleMessage(string itemClass, int quickSlotID)
		{
			this.ItemClass = itemClass;
			this.QuickSlotID = quickSlotID;
		}

		public override string ToString()
		{
			return string.Format("EquipBundleMessage[ itemClass = {0} quickSlot = {1} ]", this.ItemClass, this.QuickSlotID);
		}
	}
}
