using System;

namespace CashShopService.WishListSystem
{
	public enum WishSPListResult
	{
		SUCCESS = 1,
		FAILDB = -1,
		FAILDBNOTRESPONSE = -2,
		FAILLOGIC = -3,
		FAILUNKNOWN = -9
	}
}
