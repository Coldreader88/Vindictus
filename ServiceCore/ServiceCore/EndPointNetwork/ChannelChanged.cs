using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ChannelChanged : IMessage
	{
		public long ChannelID { get; set; }
	}
}
