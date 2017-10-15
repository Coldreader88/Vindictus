using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NotifyPlayerReconnectMessage : IMessage
	{
		public long CID { get; set; }

		public string PlayerName { get; set; }
	}
}
