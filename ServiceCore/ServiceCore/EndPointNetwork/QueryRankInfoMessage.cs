using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRankInfoMessage : IMessage
	{
		public string RankID { get; set; }

		public int PeriodType { get; set; }

		public override string ToString()
		{
			return string.Format("QueryRankInfoMessage {0} {1}", this.RankID, this.PeriodType);
		}
	}
}
