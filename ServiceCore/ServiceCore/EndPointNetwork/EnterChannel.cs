using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EnterChannel : IMessage
	{
		public long ChannelID { get; set; }

		public long PartitionID { get; set; }

		public ActionSync Action { get; set; }

		public override string ToString()
		{
			return string.Format("EnterChannel[Channel {0} PartitionID {1}]", this.ChannelID, this.PartitionID);
		}
	}
}
