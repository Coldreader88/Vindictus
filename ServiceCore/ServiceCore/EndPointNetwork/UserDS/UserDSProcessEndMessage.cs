using System;

namespace ServiceCore.EndPointNetwork.UserDS
{
	[Serializable]
	public sealed class UserDSProcessEndMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("UserDSProcessEndMessage", new object[0]);
		}
	}
}
