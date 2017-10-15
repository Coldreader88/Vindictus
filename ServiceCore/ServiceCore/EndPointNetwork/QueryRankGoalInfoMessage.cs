using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRankGoalInfoMessage : IMessage
	{
		public long CID { get; set; }

		public int EventID { get; set; }

		public int PeriodType { get; set; }

		public override string ToString()
		{
			return string.Format("QueryRankGoalInfoMessage {0} {1} {2}", this.CID, this.EventID, this.PeriodType);
		}
	}
}
