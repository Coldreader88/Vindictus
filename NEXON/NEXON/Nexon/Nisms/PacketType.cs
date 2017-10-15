using System;

namespace Nexon.Nisms
{
	public enum PacketType : byte
	{
		Initialize = 1,
		HeartBeat,
		CategoryInquiry = 97,
		ProductInquiryVersion2 = 85,
		CheckBalance = 17,
		CheckBalanceEX,
		PurchaseItemAttribute = 36,
		PurchaseItemEX = 35,
		PurchaseGift = 34,
		PurchaseGiftAttribute = 37,
		PurchaseItemRefund = 128,
		InventoryInquiry = 65,
		InventoryInquiryRead = 73,
		InventoryPickup,
		InventoryPickupOnce = 76,
		InventoryPickupRollback = 75,
		InventoryClear = 72
	}
}
