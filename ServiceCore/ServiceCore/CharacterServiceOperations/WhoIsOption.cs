using System;

namespace ServiceCore.CharacterServiceOperations
{
	[Flags]
	public enum WhoIsOption
	{
		None = 0,
		Stat = 1,
		Costume = 2,
		Skill = 4,
		Title = 8,
		Equipment = 16,
		Mission = 32,
		StatusEffect = 64,
		GuildLevelUpBonus = 128,
		UpdateGuild = 256,
		ManufactureExp = 512,
		FriendRecommendedInfo = 1024,
		ApplyBadStatus = 2048,
		SkillEnhance = 4096
	}
}
