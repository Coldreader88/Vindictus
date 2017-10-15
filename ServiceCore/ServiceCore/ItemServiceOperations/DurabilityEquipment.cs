using System;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DurabilityEquipment
	{
		public DurabilityEquipment(int maxduabilitybonus, int diffdurability)
		{
			this.MaxDurabilityBonus = maxduabilitybonus;
			this.DiffDurability = diffdurability;
		}

		public int MaxDurabilityBonus { get; set; }

		public int DiffDurability { get; set; }
	}
}
