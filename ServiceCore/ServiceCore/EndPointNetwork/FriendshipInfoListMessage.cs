using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class FriendshipInfoListMessage : IMessage
	{
		public List<int> FriendList { get; set; }

		public FriendshipInfoListMessage(List<int> friendList)
		{
			this.FriendList = friendList;
		}
	}
}
