using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AllUserGoalRewardMessage : IMessage
	{
		public int GoalID { get; set; }

		public AllUserGoalRewardMessage(int goalID)
		{
			this.GoalID = goalID;
		}

		public override string ToString()
		{
			return string.Format("AllUserGoalRewardMessage {0}", this.GoalID);
		}
	}
}
