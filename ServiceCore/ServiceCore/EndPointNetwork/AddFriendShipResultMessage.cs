using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AddFriendShipResultMessage : IMessage
	{
		public int Result { get; set; }

		public string friendName { get; set; }

		public AddFriendShipResultMessage(int result, string friendname)
		{
			this.Result = result;
			this.friendName = friendname;
		}

		public enum AddFriendShipResult
		{
			Result_Character_NotFounded,
			Result_SameID,
			Result_Already_Added,
			Result_FriendCount_Max,
			Result_Exception,
			Result_Ok
		}
	}
}
