using System;

namespace Nexon.Nisms
{
	public enum Result
	{
		Successful = 1,
		Failed = 0,
		NotFound = 2,
		ProductListUpdated = 17,
		PurchaseLimit = 11001,
		CashLocked,
		BlockedUser = 12001,
		NotEnoughCash = 12040,
		NoUser = 12002,
		DBError = 99,
		MaintenanceFailed = 255
	}
}
