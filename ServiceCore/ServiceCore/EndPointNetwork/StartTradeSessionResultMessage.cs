using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StartTradeSessionResultMessage : IMessage
	{
		public int Result { get; set; }
	}
}
