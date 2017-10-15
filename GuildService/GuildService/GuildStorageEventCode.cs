using System;
using ServiceCore;

namespace GuildService
{
	[LogKey("GuildStorageLedger", "Event")]
	public enum GuildStorageEventCode
	{
		Unknown,
		GoldCount_Login,
		ItemCount_Login,
		GoldCount_Logout,
		ItemCount_Logout,
		TransferFrom = 64,
		TransferTo,
		Process,
		Done,
		ChangeSetting,
		PurchaseSlotByGold,
		PurchaseSlotByCash,
		Error_Processing = 128,
		Error_Invalid,
		Error_Limited,
		Error_TransferFail,
		Error_TransferFail_Recovered,
		Error_TransferFail_RecoverFail,
		Error_PurchaseSlotByGold,
		Error_PurchaseSlotByCash,
		Error_ProcessFail
	}
}
