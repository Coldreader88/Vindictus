using System;
using ServiceCore;

namespace GuildService
{
	[LogKey("GuildStorageLedger", "Operation")]
	public enum GuildStorageOperationCode
	{
		Unknown,
		Report,
		AddGuildItem,
		ArrangeGuildItem,
		PickGuildItem,
		PurchaseGuildStorage,
		UpdateSetting
	}
}
