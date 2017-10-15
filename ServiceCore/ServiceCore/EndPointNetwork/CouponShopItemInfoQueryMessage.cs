using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CouponShopItemInfoQueryMessage : IMessage
	{
		public short ShopVersion { get; set; }

		public override string ToString()
		{
			return string.Format("CouponShopItemInfoQueryMessage[ {0} ]", this.ShopVersion);
		}
	}
}
