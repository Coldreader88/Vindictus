using System;

namespace GuildService.API
{
	public enum GroupIDCheckResult : byte
	{
		Succeed,
		IDNotSupplied,
		DuplicatedID,
		InvalidCharacter
	}
}
