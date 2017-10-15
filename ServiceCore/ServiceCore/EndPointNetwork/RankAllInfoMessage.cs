using System;
using System.Collections.Generic;
using ServiceCore.RankServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RankAllInfoMessage : IMessage
	{
		public IList<RankResultInfo> RankResult { get; set; }

		public int PeriodType { get; set; }

		public RankAllInfoMessage(IList<RankResultInfo> RankResult, int PeriodType)
		{
			this.RankResult = RankResult;
			this.PeriodType = PeriodType;
		}

		public override string ToString()
		{
			return string.Format("RankAllInfoMessage[{0}], {1}", this.RankResult.Count, this.PeriodType);
		}
	}
}
