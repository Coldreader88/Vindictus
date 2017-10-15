using System;

namespace ServiceCore.EndPointNetwork
{
	public enum CharacterNameCheckResult
	{
		Unknown,
		Success,
		InvalidName,
		DuplicatedName,
		ReservedName,
		ForbiddenName,
		ChangedCharacterName
	}
}
