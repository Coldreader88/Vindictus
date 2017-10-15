using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestProgressMessage : IMessage
	{
		public ICollection<QuestProgressInfo> QuestProgress { get; set; }

		public ICollection<AchieveGoalInfo> AchievedGoals { get; set; }

		public QuestProgressMessage(ICollection<QuestProgressInfo> questProgress, ICollection<AchieveGoalInfo> achievedGoals)
		{
			this.QuestProgress = questProgress;
			this.AchievedGoals = achievedGoals;
		}

		public override string ToString()
		{
			return string.Format("QuestProgressMessage [ questProgress x {0} ]", this.QuestProgress.Count);
		}
	}
}
