using System;

namespace ServiceCore.EndPointNetwork
{
	public enum CharacterNameChangeResult
	{
		Unknown,
		Success,
		InvalidName,
		DuplicatedName,
		ReservedName,
		ForbiddenName,
		ChangedCharacterName,
		ItemDestroyFail,
		NameChangeFail
	}
}
