using System;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RestoreItemInfo : IMessage
	{
		public SlotInfo Slot { get; private set; }

		public string PriceType { get; private set; }

		public int PriceValue { get; private set; }

		public RestoreItemInfo(SlotInfo slot, string priceType, int priceValue)
		{
			this.Slot = slot;
			this.PriceType = priceType;
			this.PriceValue = priceValue;
		}

		public override string ToString()
		{
			return string.Format("RestoreItemInfo : item={0}({1}), type={2}, value={3}", new object[]
			{
				this.Slot.ItemClass,
				this.Slot.ItemID,
				this.PriceType,
				this.PriceValue
			});
		}
	}
}
