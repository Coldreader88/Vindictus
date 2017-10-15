using System;

namespace ServiceCore.EndPointNetwork.UserDS
{
	[Serializable]
	public sealed class UserDSLaunchMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("UserDSLaunchMessage", new object[0]);
		}
	}
}
