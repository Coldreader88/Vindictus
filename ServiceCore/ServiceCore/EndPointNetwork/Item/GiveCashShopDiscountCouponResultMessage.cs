using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class GiveCashShopDiscountCouponResultMessage : IMessage
	{
		public GiveCashShopDiscountCouponResultMessage(bool isSuccess)
		{
			this.IsSuccess = isSuccess;
		}

		public override string ToString()
		{
			return string.Format("GiveCashShopDiscountCouponResultMessage", new object[0]);
		}

		private bool IsSuccess;
	}
}
