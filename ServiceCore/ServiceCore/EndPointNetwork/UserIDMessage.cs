using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UserIDMessage : IMessage
	{
		public int UserID { get; set; }

		public UserIDMessage(int userID)
		{
			this.UserID = userID;
		}
	}
}
