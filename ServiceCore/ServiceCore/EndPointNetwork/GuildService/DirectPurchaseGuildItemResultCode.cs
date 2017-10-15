using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	public enum DirectPurchaseGuildItemResultCode
	{
		RESULTCODE_START,
		Success,
		Unknown,
		GuildNotInitialize,
		CannotParseItem,
		OverMaxMemberLimit,
		DatabaseFail,
		DatabaseException,
		BeginDirectPurchaseItem,
		EndDirectPurchaseItem,
		IsNotMaster,
		ReloadFail,
		RESULTCODE_END
	}
}
