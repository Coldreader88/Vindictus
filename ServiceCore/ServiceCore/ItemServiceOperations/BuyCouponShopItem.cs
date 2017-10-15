using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public class BuyCouponShopItem : Operation
	{
		public short ShopVersion { get; set; }

		public short Order { get; set; }

		public int Count { get; set; }

		public BuyCouponShopItem(short shopVersion, short order, int count)
		{
			this.ShopVersion = shopVersion;
			this.Order = order;
			this.Count = count;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
