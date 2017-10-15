using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LeaveChannelMessage : IMessage
	{
		public long ChannelID { get; set; }

		public override string ToString()
		{
			return string.Format("LeaveChannelMessage() {{ ChannelID = {0} }}", this.ChannelID);
		}
	}
}
