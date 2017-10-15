using System;

namespace ServiceCore.EndPointNetwork
{
	public enum WishListResult
	{
		SUCCESS = 1,
		NOITEM,
		FAILDB = -1,
		FAILDBNOTRESPONSE = -2,
		FAILLOGIC = -3,
		FAILUNKNOWN = -9,
		OVERCOUNT = -10
	}
}
