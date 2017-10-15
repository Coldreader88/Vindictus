using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class JoinChannelMessage : IMessage
	{
		public long ChannelID { get; set; }

		public override string ToString()
		{
			return string.Format("JoinChannelMessage() {{ ChannelID = {0} }}", this.ChannelID);
		}
	}
}
