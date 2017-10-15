using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UserCareItemOpenMessage : IMessage
	{
		public int Index { get; private set; }

		public UserCareItemOpenMessage(int index)
		{
			this.Index = index;
		}
	}
}
