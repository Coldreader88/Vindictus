using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AllUserGoalEventModifyMessage : IMessage
	{
		public int GoalID { get; set; }

		public int Count { get; set; }

		public AllUserGoalEventModifyMessage(int GoalID, int Count)
		{
			this.GoalID = GoalID;
			this.Count = Count;
		}

		public override string ToString()
		{
			return string.Format("AllUserGoalEventModifyMessage [ goalID = {0} count = {1} ]", this.GoalID, this.Count);
		}
	}
}
