using System;

namespace ServiceCore.EndPointNetwork
{
	public enum BurnResultType
	{
		Success,
		Fail_System,
		Fail_ItemTotalCount,
		Fail_ItemExist,
		Fail_ItemHasCount,
		Fail_ResultItem,
		Fail_RandomItemInfo,
		Fail_GiveCount,
		Fail_ItemOpen,
		Fail_JackpotInfo,
		Fail_GiveItem,
		Fail_Save
	}
}
