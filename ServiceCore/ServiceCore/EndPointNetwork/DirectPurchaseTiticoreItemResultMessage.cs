using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DirectPurchaseTiticoreItemResultMessage : IMessage
	{
		public bool Result { get; set; }

		public DirectPurchaseCashShopItemResultMessage.DirectPurchaseItemFailReason Reason { get; set; }

		public int ProductNo { get; set; }
	}
}
