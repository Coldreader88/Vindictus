using System;
using System.Collections.Generic;
using ServiceCore.RankServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RankInfoMessage : IMessage
	{
		public IList<RankResultInfo> RankResult { get; set; }

		public int PeriodType { get; set; }

		public RankInfoMessage(IList<RankResultInfo> RankResult, int PeriodType)
		{
			this.RankResult = RankResult;
			this.PeriodType = PeriodType;
		}

		public override string ToString()
		{
			return string.Format("RankInfoMessage[{0}] {1}", this.RankResult.Count, this.PeriodType);
		}
	}
}
