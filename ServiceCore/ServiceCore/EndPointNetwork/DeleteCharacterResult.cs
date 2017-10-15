using System;

namespace ServiceCore.EndPointNetwork
{
	public enum DeleteCharacterResult
	{
		Unknown,
		Success_Deleted,
		Success_Deleting,
		Fail_NoSuchCharacter,
		Fail_NoCashShopService,
		Fail_RemainCashItems,
		Fail_NoGuildService,
		Fail_InGuild,
		Fail_NeedMoreWaiting,
		Fail_InvalidStatus,
		Fail_Exception,
		Fail_Unknown,
		Fail_ExcuteOp
	}
}
