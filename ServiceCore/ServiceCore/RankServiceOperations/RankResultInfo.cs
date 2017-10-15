using System;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class RankResultInfo
	{
		public int EventID { get; set; }

		public int Rank { get; set; }

		public int RankPrev { get; set; }

		public long Score { get; set; }

		public string CharacterName { get; set; }

		public RankResultInfo(int eventID, int rank, int rankPrev, long score)
		{
			this.EventID = eventID;
			this.Rank = rank;
			this.RankPrev = rankPrev;
			this.Score = score;
			this.CharacterName = "";
		}

		public RankResultInfo(int eventID, int rank, int rankPrev, long score, string characterName)
		{
			this.EventID = eventID;
			this.Rank = rank;
			this.RankPrev = rankPrev;
			this.Score = score;
			this.CharacterName = characterName;
		}

		public override string ToString()
		{
			return string.Format("RankResult({0} {1} {2} {3} {4})", new object[]
			{
				this.EventID,
				this.Rank,
				this.RankPrev,
				this.Score,
				this.CharacterName
			});
		}
	}
}
