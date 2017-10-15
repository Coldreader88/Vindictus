using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WaitTicketMessage : IMessage
	{
		public int QueueSpeed { get; set; }

		public int Position { get; set; }

		public override string ToString()
		{
			return string.Format("WaitTicketMessgae [{0}/{1}]", this.Position, this.QueueSpeed);
		}
	}
}
