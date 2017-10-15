using System;
using System.Collections.Generic;
using ServiceCore.RankServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RankHeavyFavoritesInfoMessage : IMessage
	{
		public IList<RankResultInfo> RankResult { get; set; }

		public RankHeavyFavoritesInfoMessage(IList<RankResultInfo> RankResult)
		{
			this.RankResult = RankResult;
		}

		public override string ToString()
		{
			return string.Format("RankHeavyFavoritesInfoMessage[{0}]", this.RankResult.Count);
		}
	}
}
