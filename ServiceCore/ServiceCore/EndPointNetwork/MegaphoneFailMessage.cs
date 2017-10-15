using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MegaphoneFailMessage : IMessage
	{
		public int ErrorCode { get; set; }
	}
}
