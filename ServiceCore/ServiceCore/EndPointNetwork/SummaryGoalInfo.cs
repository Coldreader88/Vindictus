using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SummaryGoalInfo
	{
		public int GoalID { get; set; }

		public int Exp { get; set; }

		public int Gold { get; set; }

		public int Ap { get; set; }

		public SummaryGoalInfo(int goalID, int exp, int gold, int ap)
		{
			this.GoalID = goalID;
			this.Exp = exp;
			this.Gold = gold;
			this.Ap = ap;
		}
	}
}
