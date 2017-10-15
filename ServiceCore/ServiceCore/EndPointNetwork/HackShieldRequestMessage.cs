using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HackShieldRequestMessage : IMessage
	{
		public ArraySegment<byte> Request { get; private set; }

		public HackShieldRequestMessage(ArraySegment<byte> request)
		{
			this.Request = request;
		}
	}
}
