using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AddFriendshipInfoMessage : IMessage
	{
		public int FriendID { get; set; }

		public int FriendLimitCount { get; set; }

		public AddFriendshipInfoMessage(int friendID, int friendLimitCount)
		{
			this.FriendID = friendID;
			this.FriendLimitCount = friendLimitCount;
		}
	}
}
