using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DirectPurchaseCashShopItemResultMessage : IMessage
	{
		public bool Result { get; set; }

		public DirectPurchaseCashShopItemResultMessage.DirectPurchaseItemFailReason Reason { get; set; }

		public enum DirectPurchaseItemFailReason
		{
			Empty,
			BeginPurchaseItemFail,
			EndPurchaseItemFail,
			GiveItemFail,
			UseInventoryItemInQuestFail,
			Unknown
		}
	}
}
