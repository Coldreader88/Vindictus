using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations.RandomMissionOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ChangeMissionStatusMessage : IMessage
	{
		public ChangeMissionStatusMessage(IList<MissionInfo> missionList)
		{
			this.MissionList = new List<MissionMessage>();
			foreach (MissionInfo mission in missionList)
			{
				this.AddMission(mission);
			}
		}

		public ChangeMissionStatusMessage(MissionInfo mission)
		{
			this.MissionList = new List<MissionMessage>();
			this.AddMission(mission);
		}

		public void AddMission(MissionInfo mission)
		{
			this.MissionList.Add(new MissionMessage(mission));
		}

		public ICollection<MissionMessage> MissionList { get; set; }
	}
}
