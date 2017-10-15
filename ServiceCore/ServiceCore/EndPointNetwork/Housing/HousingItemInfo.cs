using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingItemInfo
	{
		public long HousingItemID { get; set; }

		public int PropClass { get; set; }

		public int Slot { get; set; }

		public int Count { get; set; }

		public HousingItemInfo(long itemID, int propClass, int count, int slot)
		{
			this.HousingItemID = itemID;
			this.PropClass = propClass;
			this.Slot = slot;
			this.Count = count;
		}

		public override string ToString()
		{
			return string.Format("HousingItemInfo [{0} / {1} x {2} / {3}]", new object[]
			{
				this.HousingItemID,
				this.PropClass,
				this.Count,
				this.Slot
			});
		}
	}
}
