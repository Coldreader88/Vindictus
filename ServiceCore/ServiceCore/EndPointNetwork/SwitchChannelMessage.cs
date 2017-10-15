using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SwitchChannelMessage : IMessage
	{
		public long OldChannelID { get; set; }

		public long NewChannelID { get; set; }

		public override string ToString()
		{
			return string.Format("SwitchChannelMessage() {{ OldChannelID = {0}, NewChannelID = {1} }}", this.OldChannelID, this.NewChannelID);
		}
	}
}
