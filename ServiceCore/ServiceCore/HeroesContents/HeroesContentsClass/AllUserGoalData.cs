using System;

namespace ServiceCore.HeroesContents.HeroesContentsClass
{
	public class AllUserGoalData
	{
		public int GoalID { get; set; }

		public int SlotID { get; set; }

		public int GoalType { get; set; }

		public string GoalRegExp { get; set; }

		public string Reward { get; set; }

		public int RewardNum { get; set; }

		public int GoalCount { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public bool IsEndEvent { get; set; }

		public string QuestName { get; set; }

		public AllUserGoalData()
		{
			this.GoalID = 0;
			this.SlotID = 0;
			this.GoalType = 0;
			this.GoalRegExp = null;
			this.GoalCount = 0;
			this.Reward = null;
			this.RewardNum = 0;
			this.StartTime = default(DateTime);
			this.EndTime = default(DateTime);
			this.IsEndEvent = false;
			this.QuestName = null;
		}

		public AllUserGoalData(AllUserGoalInfo info)
		{
			this.GoalID = info.ID;
			this.SlotID = info.SlotID;
			this.GoalType = info.GoalType;
			this.GoalRegExp = info.Goal;
			this.GoalCount = info.GoalCount;
			this.Reward = info.RewardItemClass;
			this.RewardNum = info.RewardNum;
			this.StartTime = info.StartTime;
			this.EndTime = info.EndTime;
			this.IsEndEvent = false;
			this.QuestName = info.GoalName;
		}
	}
}
