using System;

namespace ServiceCore.EndPointNetwork.UserDS
{
	[Serializable]
	public sealed class UserDSHostTransferStartMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("UserDSHostTransferStartMessage", new object[0]);
		}
	}
}
