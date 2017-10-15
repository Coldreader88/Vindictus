using System;

namespace ServiceCore.RankServiceOperations
{
	[Serializable]
	public sealed class RankAlarmInfo
	{
		public int PeriodType { get; set; }

		public int EventID { get; set; }

		public int Rank { get; set; }

		public RankAlarmInfo(int periodType, int eventID, int rank)
		{
			this.PeriodType = periodType;
			this.EventID = eventID;
			this.Rank = rank;
		}

		public override string ToString()
		{
			return string.Format("RankResult({0} {1} {2})", this.PeriodType, this.EventID, this.Rank);
		}
	}
}
