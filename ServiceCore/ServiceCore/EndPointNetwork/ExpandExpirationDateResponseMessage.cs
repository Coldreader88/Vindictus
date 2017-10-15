using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ExpandExpirationDateResponseMessage : IMessage
	{
		public ExpandExpirationDateResponseMessage(ExpandExpirationDateResponseType type, long itemID)
		{
			this.Type = (int)type;
			this.ItemID = itemID;
		}

		public override string ToString()
		{
			return string.Format("ExpandExpirationDateResponseMessage[ type = {0} , itemID = {1}]", this.Type, this.ItemID);
		}

		private int Type;

		private long ItemID;
	}
}
