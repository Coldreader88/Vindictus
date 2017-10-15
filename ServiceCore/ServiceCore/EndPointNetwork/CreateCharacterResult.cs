using System;

namespace ServiceCore.EndPointNetwork
{
	public enum CreateCharacterResult
	{
		Unknown,
		Success,
		InvalidName,
		DuplicatedName,
		ReservedName,
		ForbiddenName,
		ChangedCharacterName,
		CharacterLimit,
		WebConnectionError,
		CreateCharacterStopped,
		MakeEntityFailed,
		CustomizeOperationFailed,
		CostumeSaveFailed,
		InitCharacterFailed,
		OverLimitUsingPreset,
		PremiumSlotLimit
	}
}
