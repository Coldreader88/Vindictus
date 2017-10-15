using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SecuredOperationMessage : IMessage
	{
		public SecuredOperationType Operation { get; set; }

		public int LockedTimeInSeconds { get; set; }
	}
}
