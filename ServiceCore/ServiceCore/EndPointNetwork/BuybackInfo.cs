using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BuybackInfo
	{
		public BuybackInfo(int Order, string ItemClass, int Count, int ItemColor1, int ItemColor2, int ItemColor3, int PriceCount)
		{
			this.ShopID = "Buyback";
			this.Order = Order;
			this.ItemClass = ItemClass;
			this.ItemCount = Count;
			this.ItemColor1 = ItemColor1;
			this.ItemColor2 = ItemColor2;
			this.ItemColor3 = ItemColor3;
			this.PriceItemClass = "gold";
			this.PriceCount = PriceCount;
		}

		public override string ToString()
		{
			return string.Format("BuybackInfo(ShopID = Buyback ItemClass = {0} Count = {1} PriceCount = {2})", this.ItemClass, this.ItemCount, this.PriceCount);
		}

		public string ShopID;

		public int Order;

		public string ItemClass;

		public int ItemCount;

		public int ItemColor1;

		public int ItemColor2;

		public int ItemColor3;

		public string PriceItemClass;

		public int PriceCount;
	}
}
