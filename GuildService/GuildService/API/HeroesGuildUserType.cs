using System;

namespace GuildService.API
{
	public enum HeroesGuildUserType
	{
		master = 5,
		guildLeader = 13,
		sysop = 15,
		guildMember = 75,
		associate_CSO = 80,
		member_lv5 = 85,
		member_lv4 = 95,
		member_lv3 = 105,
		member_lv2 = 115,
		member_lv1 = 125,
		webmember = 135,
		alliedMember = 145,
		memberWaiting = 155,
		webmemberWaiting = 158,
		bookmarkedUser = 165,
		memberSeceded = 175,
		guest = 225,
		unknown = 235,
		deniedUser = 245,
		rejectedUser = 255
	}
}
