using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TooltipItemInfo
	{
		public string ItemClassEx { get; set; }

		public int ItemCount { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }

		public int MaxDurabilityBonus { get; set; }

		public int ReducedDurability { get; set; }

		public bool IsCharacterBinded { get; set; }

		public bool IsExpireable { get; set; }

		public long ExpireDateTimeDiff { get; set; }

		public long ExpireDateTimeTick { get; set; }

		public TooltipItemInfo(string itemClassEx, int itemCount, int color1, int color2, int color3, int maxDurabilityBonus, int reducedDurability, bool isCharacterBinded, DateTime? expireDateTime)
		{
			this.ItemClassEx = itemClassEx;
			this.ItemCount = itemCount;
			this.Color1 = color1;
			this.Color2 = color2;
			this.Color3 = color3;
			this.MaxDurabilityBonus = maxDurabilityBonus;
			this.ReducedDurability = reducedDurability;
			this.IsCharacterBinded = isCharacterBinded;
			if (expireDateTime == null)
			{
				this.IsExpireable = false;
				this.ExpireDateTimeDiff = -1L;
				this.ExpireDateTimeTick = -1L;
				return;
			}
			this.IsExpireable = true;
			this.ExpireDateTimeDiff = expireDateTime.Value.Ticks - DateTime.UtcNow.Ticks;
			this.ExpireDateTimeTick = expireDateTime.Value.Ticks;
		}

		public override string ToString()
		{
			return string.Format("TooltipItemInfo( ItemClass = {0})", this.ItemClassEx);
		}
	}
}
