using System;

namespace ServiceCore.EndPointNetwork.CharacterList
{
	public enum DeleteCharacterCancelResult
	{
		Unknown,
		Success,
		Fail_NoSuchCharacter,
		Fail_InvalidStatus,
		Fail_Exception,
		Fail_Unknown
	}
}
