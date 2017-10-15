using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DirectPurchaseCashShopItemMessage : IMessage
	{
		public int ProductNo { get; set; }

		public int cashType { get; set; }
	}
}
