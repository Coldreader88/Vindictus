using System;
using ServiceCore;

namespace GuildService
{
	[LogKey("GuildLedger", "Operation")]
	public enum OperationType
	{
		UnknownOperation,
		DirectPurchaseGuildItem,
		GainGuildPoint,
		GuildLevelUp,
		ChangeMaster,
		HeroesCore_OpenGuild = 101,
		HeroesCore_CloseGuild,
		HeroesCore_RequestJoin,
		HeroesCore_ResponseJoin,
		HeroesCore_LeaveGuild,
		HeroesCore_ChangeMaster,
		HeroesCore_ChangeRank,
		HeroesCore_UserNameModify,
		HeroesCore_GuildIDAndNameModify
	}
}
