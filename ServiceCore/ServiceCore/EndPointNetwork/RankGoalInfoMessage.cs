using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RankGoalInfoMessage : IMessage
	{
		public int EventID { get; set; }

		public string RivalName { get; set; }

		public long RivalScore { get; set; }

		public int Rank { get; set; }

		public int RankPrev { get; set; }

		public RankGoalInfoMessage(int EventID, string RivalName, long RivalScore, int Rank, int RankPrev)
		{
			this.EventID = EventID;
			this.RivalName = RivalName;
			this.RivalScore = RivalScore;
			this.Rank = Rank;
			this.RankPrev = RankPrev;
		}

		public override string ToString()
		{
			return string.Format("RankGoalInfoMessage {0}, {1}, {2}, {3}, {4}", new object[]
			{
				this.EventID,
				this.RivalName,
				this.RivalScore,
				this.Rank,
				this.RankPrev
			});
		}
	}
}
