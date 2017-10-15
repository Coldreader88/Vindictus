using System;
using System.Collections.Generic;
using ServiceCore.RankServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RandomRankInfoMessage : IMessage
	{
		public IList<RandomRankResultInfo> RandomRankResult { get; set; }

		public RandomRankInfoMessage(IList<RandomRankResultInfo> RandomRankResult)
		{
			this.RandomRankResult = RandomRankResult;
		}

		public override string ToString()
		{
			return string.Format("RandomRankInfoMessage[{0}]", this.RandomRankResult.Count);
		}
	}
}
