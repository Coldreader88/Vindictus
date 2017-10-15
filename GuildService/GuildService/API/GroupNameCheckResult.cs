using System;

namespace GuildService.API
{
	public enum GroupNameCheckResult : byte
	{
		Succeed,
		NotMatchedNamingRule,
		NotMatchedNamingRuleMaxBytes,
		RepeatedCharacters,
		DuplicatedName
	}
}
