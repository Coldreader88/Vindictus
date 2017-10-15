using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DeleteFriendshipInfoMessage : IMessage
	{
		public int FriendID { get; set; }

		public DeleteFriendshipInfoMessage(int friendID)
		{
			this.FriendID = friendID;
		}
	}
}
