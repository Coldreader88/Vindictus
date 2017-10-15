using System;
using System.Collections.Generic;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class TransferredItemInfo
	{
		public long ItemID { get; set; }

		public string ItemClassEx { get; set; }

		public int ItemCount { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }

		public int MaxDurabilityBonus { get; set; }

		public int ReducedDurability { get; set; }

		public bool IsCharacterBinded { get; set; }

		public DateTime? ExpireDateTime { get; set; }

		public TransferredItemInfo()
		{
		}

		public TransferredItemInfo(long ItemID, string ItemClassEx, int ItemCount, int Color1, int Color2, int Color3, int ReducedDurability, int MaxDurabilityBonus, bool IsCharacterBinded, DateTime? expireDateTime)
		{
			this.ItemID = ItemID;
			this.ItemClassEx = ItemClassEx;
			this.ItemCount = ItemCount;
			this.Color1 = Color1;
			this.Color2 = Color2;
			this.Color3 = Color3;
			this.ReducedDurability = ReducedDurability;
			this.MaxDurabilityBonus = MaxDurabilityBonus;
			this.IsCharacterBinded = IsCharacterBinded;
			this.ExpireDateTime = expireDateTime;
		}

		public TransferredItemInfo(long ItemID, string ItemClass, IDictionary<string, ItemAttributeElement> attributes, int ItemCount, int Color1, int Color2, int Color3, int ReducedDurability, int MaxDurabilityBonus, bool IsCharacterBinded, DateTime? expireDateTime)
		{
			this.ItemID = ItemID;
			this.ItemClassEx = ItemClassExBuilder.Build(ItemClass, attributes);
			this.ItemCount = ItemCount;
			this.Color1 = Color1;
			this.Color2 = Color2;
			this.Color3 = Color3;
			this.ReducedDurability = ReducedDurability;
			this.MaxDurabilityBonus = MaxDurabilityBonus;
			this.IsCharacterBinded = IsCharacterBinded;
			this.ExpireDateTime = expireDateTime;
		}
	}
}
