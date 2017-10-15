using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestSecuredOperationMessage : IMessage
	{
		public SecuredOperationType Operation { get; set; }
	}
}
