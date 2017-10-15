using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations.RandomMissionOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GetCharacterMissionStatusMessage : IMessage
	{
		public GetCharacterMissionStatusMessage(IList<MissionInfo> missionList, int completedMissionCount, DateTime completedMissionCountClearTime)
		{
			CompletedMissionInfoMessage completedMissionInfoMessage = new CompletedMissionInfoMessage(completedMissionCount, completedMissionCountClearTime);
			this.MissionCompletionCount = completedMissionInfoMessage.CompletedMissionCount;
			this.RemainTimeToCleanMissionCompletionCount = completedMissionInfoMessage.RemainTimeToCompletedMissionCountClear;
			this.MissionList = new List<MissionMessage>();
			foreach (MissionInfo mission in missionList)
			{
				this.MissionList.Add(new MissionMessage(mission));
			}
		}

		public int MissionCompletionCount { get; set; }

		public int RemainTimeToCleanMissionCompletionCount { get; set; }

		public ICollection<MissionMessage> MissionList { get; set; }
	}
}
