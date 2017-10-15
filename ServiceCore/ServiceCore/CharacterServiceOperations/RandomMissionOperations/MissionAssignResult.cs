using System;

namespace ServiceCore.CharacterServiceOperations.RandomMissionOperations
{
	public enum MissionAssignResult
	{
		Success,
		AssignedSameMissionBefore,
		HasSameMission,
		MaxCapability,
		NoPostedMission,
		ExceptionOccured,
		LevelLimit,
		UnExpected
	}
}
