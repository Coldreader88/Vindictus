using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BurnRewardItemsMessage : IMessage
	{
		public Dictionary<string, int> RewardItems { get; set; }

		public Dictionary<string, int> RewardMailItems { get; set; }

		public BurnRewardItemsMessage(Dictionary<string, int> rewardItems, Dictionary<string, int> rewardMailItems)
		{
			this.RewardItems = rewardItems;
			this.RewardMailItems = rewardMailItems;
		}
	}
}
