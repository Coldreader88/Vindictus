using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public class UserCareStateUpdateMessage : IMessage
	{
		public int UserCareType { get; set; }

		public int UserCareNextState { get; set; }

		public UserCareStateUpdateMessage(int type, int nextState)
		{
			this.UserCareType = type;
			this.UserCareNextState = nextState;
		}
	}
}
