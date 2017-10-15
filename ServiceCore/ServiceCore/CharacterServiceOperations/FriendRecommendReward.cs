using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class FriendRecommendReward : Operation
	{
		public string friendRewardItemClass { get; set; }

		public int friendItemCount { get; set; }

		public string myRewardItemClass { get; set; }

		public int myItemCount { get; set; }

		public string friendmailTitle { get; set; }

		public string friendmailText { get; set; }

		public string mymailTitle { get; set; }

		public string mymailText { get; set; }

		public bool usenotifySystem { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
