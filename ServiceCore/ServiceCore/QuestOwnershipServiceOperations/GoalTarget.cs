using System;
using ServiceCore.HeroesContents;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class GoalTarget
	{
		public int GoalID { get; set; }

		public int Weight { get; set; }

		public string Regex { get; set; }

		public int Count { get; set; }

		public bool Positive { get; set; }

		public int Exp { get; set; }

		public int BaseExp { get; set; }

		public int Gold { get; set; }

		public string ItemReward { get; set; }

		public int ItemNum { get; set; }

		public static GoalTarget From(QuestGoalInfo info)
		{
			return new GoalTarget
			{
				GoalID = info.ID,
				Weight = info.Weight,
				Regex = ((info.Target == null || info.GameSetting != null) ? "" : info.Target),
				Count = info.TargetCount.Value,
				Positive = (info.IsPositive != 0),
				Exp = info.XP * FeatureMatrix.GetInteger("ExpRate_Combat") / 100,
				Gold = info.Gold * FeatureMatrix.GetInteger("GoldRate_Combat") / 100,
				BaseExp = info.XP,
				ItemReward = ((info.Item == null) ? "" : info.Item),
				ItemNum = info.ItemNum
			};
		}

		public override string ToString()
		{
			return string.Format("GoalTarget({0} x {1}{2} Exp {3} Gold{4} Item{5}x{6}", new object[]
			{
				this.Regex,
				this.Count,
				this.Positive ? "+" : "-",
				this.Exp,
				this.Gold,
				this.ItemReward,
				this.ItemNum
			});
		}
	}
}
