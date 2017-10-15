using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class RequestGemstoneRollbackMessage : IMessage
	{
		public long BraceletItemID { get; set; }

		public RequestGemstoneRollbackMessage(long braceletItemID)
		{
			this.BraceletItemID = braceletItemID;
		}

		public override string ToString()
		{
			return string.Format("RequestGemstoneRollbackMessage {0}", this.BraceletItemID);
		}
	}
}
