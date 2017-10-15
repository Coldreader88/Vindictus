using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public class CouponShopItemBuyMessage : IMessage
	{
		public short ShopID { get; set; }

		public short Order { get; set; }

		public int Count { get; set; }

		public override string ToString()
		{
			return string.Format("BuyCouponShopItemMessage [ {0} {1} {2} ]", this.ShopID, this.Order, this.Count);
		}
	}
}
