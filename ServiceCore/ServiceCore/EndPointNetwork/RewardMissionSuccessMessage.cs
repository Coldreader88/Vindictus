using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations.RandomMissionOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RewardMissionSuccessMessage : IMessage
	{
		public long ID { get; set; }

		public int RewardAP { get; set; }

		public int RewardEXP { get; set; }

		public int RewardGold { get; set; }

		public List<string> RewardItemIDs { get; set; }

		public List<int> RewardItemNums { get; set; }

		public RewardMissionSuccessMessage(RewardItemInfo itemInfo)
		{
			this.RewardItemIDs = new List<string>();
			this.RewardItemNums = new List<int>();
			this.RewardItemIDs.AddRange(itemInfo.RewardItemIDs);
			this.RewardItemNums.AddRange(itemInfo.RewardItemNums);
		}
	}
}
