using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class GiveCashShopDiscountCouponMessage : IMessage
	{
		public string CouponCode { get; set; }

		public override string ToString()
		{
			return string.Format("GiveCashShopDiscountCouponMessage[ CouponCode = {0} ]", this.CouponCode);
		}
	}
}
