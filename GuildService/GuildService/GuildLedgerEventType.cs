using System;
using ServiceCore;

namespace GuildService
{
	[LogKey("GuildLedger", "Event")]
	public enum GuildLedgerEventType
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
		UpdateGuildPoint,
		UpdateGuildUserPoint,
		LevelUp,
		SendRewardItemMail,
		SendRewardItemMailFail,
		SendRewardItemMailComplete,
		SendRewardItemToQueue,
		SendRewardItemToQueueFail,
		SendRewardItemToQueueComplete,
		ChangeMaster_Success,
		ChangeMaster_NotMatchOldMaster,
		ChangeMaster_NotFoundNewMaster,
		ChangeMaster_EqualNewAndOldID,
		ChangeMaster_LowRankOldMaster,
		ChangeMaster_SystemError,
		ChangeMaster_SyncFailed,
		ChangeIDAndName_Success,
		ChangeIDAndName_UpdateFailed,
		ChangeIDAndName_InvalidID,
		ChangeIDAndName_InvalidName,
		RESULTCODE_END
	}
}
