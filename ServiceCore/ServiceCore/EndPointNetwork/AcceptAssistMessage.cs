using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AcceptAssistMessage : IMessage
	{
		public int Slot { get; set; }
	}
}
