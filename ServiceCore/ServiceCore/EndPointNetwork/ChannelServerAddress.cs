using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ChannelServerAddress : IMessage
	{
		public long ChannelID { get; set; }

		public string Address { get; set; }

		public int Port { get; set; }

		public int Key { get; set; }
	}
}
