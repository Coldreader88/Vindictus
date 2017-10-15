using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MailItemInfo
	{
		public long MailItemID { get; set; }

		public long ItemID { get; set; }

		public string ItemClassEx { get; set; }

		public int ItemCount { get; set; }

		public bool Received { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }

		public int MaxDurabilityBonus { get; set; }

		public int ReducedDurability { get; set; }

		public bool IsCharacterBinded { get; set; }

		public bool IsExpireable { get; set; }

		public long ExpireDateTimeDiff { get; set; }

		public long ExpireDateTimeTick { get; set; }

		public MailItemInfo(long mailItemID, long itemID, string itemClassEx, int itemCount, bool received, int color1, int color2, int color3, int maxDurabilityBonus, int reducedDurability, bool isCharacterBinded, DateTime? expireDateTime)
		{
			this.MailItemID = mailItemID;
			this.ItemID = itemID;
			this.ItemClassEx = itemClassEx;
			this.ItemCount = itemCount;
			this.Received = received;
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
			return string.Format("MailItemInfo(MailItemID = {0} ItemClass = {1} ItemCount = {2} Received = {3})", new object[]
			{
				this.MailItemID,
				this.ItemClassEx,
				this.ItemCount,
				this.Received
			});
		}
	}
}
