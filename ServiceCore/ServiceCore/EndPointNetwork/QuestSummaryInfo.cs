using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestSummaryInfo
	{
		public string QuestID { get; set; }

		public int SwearID { get; set; }

		public bool IsTodayQuest { get; set; }

		public MicroPlayResult Result { get; set; }

		public int LastProgress { get; set; }

		public int CurrentProgress { get; set; }

		public Dictionary<int, SummaryGoalInfo> ClearedGoals { get; set; }

		public List<SummaryRewardInfo> BattleExp { get; set; }

		public List<SummaryRewardInfo> EtcExp { get; set; }

		public List<SummaryRewardInfo> MainGold { get; set; }

		public List<SummaryRewardInfo> EtcGold { get; set; }

		public List<SummaryRewardInfo> QuestAp { get; set; }

		public List<SummaryRewardInfo> EtcAp { get; set; }

		public int Exp { get; set; }

		public int Gold { get; set; }

		public int Ap { get; set; }

		public Dictionary<string, int> RewardItem { get; set; }

		public QuestSummaryInfo(string questID, int swearID, MicroPlayResult result)
		{
			this.QuestID = questID;
			this.SwearID = swearID;
			this.Result = result;
			this.ClearedGoals = new Dictionary<int, SummaryGoalInfo>();
			this.BattleExp = new List<SummaryRewardInfo>();
			this.EtcExp = new List<SummaryRewardInfo>();
			this.MainGold = new List<SummaryRewardInfo>();
			this.EtcGold = new List<SummaryRewardInfo>();
			this.QuestAp = new List<SummaryRewardInfo>();
			this.EtcAp = new List<SummaryRewardInfo>();
			this.RewardItem = new Dictionary<string, int>();
		}

		public override string ToString()
		{
			return string.Format("Summary(XP{0} GOLD{1} AP{2})", this.Exp, this.Gold, this.Ap);
		}
	}
}
