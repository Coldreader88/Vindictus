using System;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	public enum CompleteMissionResult
	{
		Success,
		UnExpected,
		CompleteLimit,
		UnSatisfyCondition,
		RewardFault,
		Expired,
		NoMission
	}
}
