using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class FishingResultMessage : IMessage
	{
		public IList<FishingResultInfo> FishingResult { get; set; }

		public FishingResultMessage(IList<FishingResultInfo> FishingResult)
		{
			this.FishingResult = FishingResult;
		}

		public override string ToString()
		{
			return string.Format("FishingResultMessage[ BriefFishInfo x {0} ]", this.FishingResult.Count);
		}
	}
}
