using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PlayerServiceOperations
{
	[Serializable]
	public class TodayMissionComplete : Operation
	{
		public int MissionID { get; set; }

		public TodayMissionComplete(int missionID)
		{
			this.MissionID = missionID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
