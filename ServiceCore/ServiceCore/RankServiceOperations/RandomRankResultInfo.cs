using System;
using System.Collections.Generic;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class RandomRankResultInfo
	{
		public List<RankResultInfo> RandomRankResult { get; set; }

		public int EventID { get; set; }

		public int PeriodType { get; set; }

		public RandomRankResultInfo(List<RankResultInfo> randomRankResult, int eventID, int periodType)
		{
			this.RandomRankResult = randomRankResult;
			this.EventID = eventID;
			this.PeriodType = periodType;
		}
	}
}
