using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestTargetResultMessage : IMessage
	{
		public string CharacterName { get; set; }

		public int GoalID { get; set; }

		public bool IsGoalSuccess { get; set; }

		public bool IsQuestSuccess { get; set; }

		public int Exp { get; set; }

		public int Gold { get; set; }

		public int Ap { get; set; }

		public QuestTargetResultMessage(string characterName, int goalID, bool isGoalSuccess, bool isQuestSuccess, int exp, int gold, int ap)
		{
			this.CharacterName = characterName;
			this.GoalID = goalID;
			this.IsGoalSuccess = isGoalSuccess;
			this.IsQuestSuccess = this.IsQuestSuccess;
			this.Exp = exp;
			this.Gold = gold;
			this.Ap = ap;
		}

		public override string ToString()
		{
			return string.Format("TargetResultMessage[ CID = {0} goalID = {1} positive = {2} ]", this.CharacterName, this.GoalID, this.IsGoalSuccess);
		}
	}
}
