using System;

namespace ServiceCore.EndPointNetwork.UserDS
{
	[Serializable]
	public sealed class UserDSHostConnectionQuery : IMessage
	{
		public override string ToString()
		{
			return string.Format("UserDSHostConnectionQuery", new object[0]);
		}
	}
}
