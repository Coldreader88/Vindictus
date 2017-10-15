using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class SlotInfo
	{
		public long ItemID { get; set; }

		public string ItemClass { get; set; }

		public int Num { get; set; }

		public int Tab { get; set; }

		public int Slot { get; set; }

		public bool IsExpireable { get; set; }

		public long ExpireDateTimeDiff { get; set; }

		public long ExpireDateTimeTick { get; set; }

		public int[] Colors { get; set; }

		public int Durability { get; set; }

		public int MaxDurability { get; set; }

		public int MaxDurabilityBonus { get; set; }

		public bool Tradable { get; set; }

		public string ExpanderName { get; set; }

		public ICollection<ItemAttributeElement> Attributes { get; set; }

		public IDictionary<int, bool> PrefixEnchantStatus { get; set; }

		public IDictionary<int, bool> SuffixEnchantStatus { get; set; }

		public int MinPrice { get; set; }

		public int MaxPrice { get; set; }

		public int AvgPrice { get; set; }

		public int Color1
		{
			get
			{
				return this.Colors[0];
			}
		}

		public int Color2
		{
			get
			{
				return this.Colors[1];
			}
		}

		public int Color3
		{
			get
			{
				return this.Colors[2];
			}
		}

		public long MinUpTimeDateTimeDiff { get; private set; }

		public SlotInfo(long itemID, string iclass, int n, int tab, int slot, DateTime? expireDateTime, DateTime? minUpDateTime, bool tradable, string expanderName, IDictionary<string, ItemAttributeElement> attributes, int minPrice, int maxPrice, int avgPrice)
		{
			this.ItemID = itemID;
			this.ItemClass = iclass;
			this.Num = n;
			this.Tab = tab;
			this.Slot = slot;
			if (expireDateTime != null)
			{
				this.IsExpireable = true;
				this.ExpireDateTimeDiff = expireDateTime.Value.Ticks - DateTime.UtcNow.Ticks;
				this.ExpireDateTimeTick = expireDateTime.Value.Ticks;
			}
			else
			{
				this.IsExpireable = false;
				this.ExpireDateTimeDiff = -1L;
				this.ExpireDateTimeTick = -1L;
			}
			if (minUpDateTime != null)
			{
				this.MinUpTimeDateTimeDiff = minUpDateTime.Value.Ticks - DateTime.UtcNow.Ticks;
			}
			else
			{
				this.MinUpTimeDateTimeDiff = 0L;
			}
			this.Colors = new int[]
			{
				-1,
				-1,
				-1
			};
			this.Durability = 0;
			this.MaxDurability = 0;
			this.MaxDurabilityBonus = 0;
			this.Tradable = tradable;
			this.Attributes = attributes.Values.ToList<ItemAttributeElement>();
			this.PrefixEnchantStatus = new Dictionary<int, bool>();
			this.SuffixEnchantStatus = new Dictionary<int, bool>();
			this.ExpanderName = expanderName;
			this.MinPrice = minPrice;
			this.MaxPrice = maxPrice;
			this.AvgPrice = avgPrice;
		}

		public SlotInfo(long itemID, string iclass, int n, int tab, int slot, DateTime? expireDateTime, DateTime? minUpDateTime, int[] colors, int durability, int maxDurability, int maxDurabilityBonus, bool tradable, string expanderName, IDictionary<string, ItemAttributeElement> attributes, IDictionary<int, bool> prefixEnchantStatus, IDictionary<int, bool> suffixEnchantStatus, int minPrice, int maxPrice, int avgPrice)
		{
			this.ItemID = itemID;
			this.ItemClass = iclass;
			this.Num = n;
			this.Tab = tab;
			this.Slot = slot;
			if (expireDateTime != null)
			{
				this.IsExpireable = true;
				this.ExpireDateTimeDiff = expireDateTime.Value.Ticks - DateTime.UtcNow.Ticks;
				this.ExpireDateTimeTick = expireDateTime.Value.Ticks;
			}
			else
			{
				this.IsExpireable = false;
				this.ExpireDateTimeDiff = -1L;
				this.ExpireDateTimeTick = -1L;
			}
			if (minUpDateTime != null)
			{
				this.MinUpTimeDateTimeDiff = minUpDateTime.Value.Ticks - DateTime.UtcNow.Ticks;
			}
			else
			{
				this.MinUpTimeDateTimeDiff = -1L;
			}
			this.Colors = colors;
			this.Durability = durability;
			this.MaxDurability = maxDurability;
			this.MaxDurabilityBonus = maxDurabilityBonus;
			this.Tradable = tradable;
			this.Attributes = attributes.Values.ToList<ItemAttributeElement>();
			this.PrefixEnchantStatus = prefixEnchantStatus;
			this.SuffixEnchantStatus = suffixEnchantStatus;
			this.ExpanderName = expanderName;
			this.MinPrice = minPrice;
			this.MaxPrice = maxPrice;
			this.AvgPrice = avgPrice;
		}

		public override string ToString()
		{
			return string.Format("{0} : {1} x {2} ({3}, {4})", new object[]
			{
				this.ItemID,
				this.ItemClass,
				this.Num,
				this.Tab,
				this.Slot
			});
		}

		public static readonly int ColorOverrideNum = 3;
	}
}
