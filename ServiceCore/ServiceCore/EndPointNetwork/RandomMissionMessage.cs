using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RandomMissionMessage : IMessage
	{
		public int MissionCommand { get; set; }

		public long ID { get; set; }

		public string Args { get; set; }

		public long Args2 { get; set; }

		public string MID { get; set; }

		public override string ToString()
		{
			return string.Format("RandomMissionMessage [ MissionCommand = {0} ID = {1} MID={2} , Args = {3}]", new object[]
			{
				this.GetMissionCommand(),
				this.ID,
				this.MID,
				this.Args
			});
		}

		public RandomMissionCommand GetMissionCommand()
		{
			switch (this.MissionCommand)
			{
			case 1:
				return RandomMissionCommand.AssignMission;
			case 2:
				return RandomMissionCommand.GetAllPostedMission;
			case 3:
				return RandomMissionCommand.GetMissionStatus;
			case 4:
				return RandomMissionCommand.SetProgress;
			case 5:
				return RandomMissionCommand.GiveUpMission;
			case 6:
				return RandomMissionCommand.CompleteMission;
			case 7:
				return RandomMissionCommand.RequestClearingMissionCompletionCount;
			case 8:
				return RandomMissionCommand.ForceToSchedule;
			case 9:
				return RandomMissionCommand.ReloadData;
			case 10:
				return RandomMissionCommand.SetMissionCompletionCount;
			default:
				return RandomMissionCommand.UnKnown;
			}
		}
	}
}
