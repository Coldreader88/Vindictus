using System;

namespace ServiceCore.EndPointNetwork
{
	public enum RandomMissionCommand
	{
		AssignMission = 1,
		GetAllPostedMission,
		GetMissionStatus,
		SetProgress,
		GiveUpMission,
		CompleteMission,
		RequestClearingMissionCompletionCount,
		ForceToSchedule,
		ReloadData,
		SetMissionCompletionCount,
		UnKnown
	}
}
