using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseInventoryItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public long TargetItemID { get; set; }

		public UseInventoryItemMessage(long itemID, long targetItemID)
		{
			this.ItemID = itemID;
			this.TargetItemID = targetItemID;
		}

		public override string ToString()
		{
			return string.Format("UseInventoryItemMessage[ itemID = {0}, targetItemID = {1} ]", this.ItemID, this.TargetItemID);
		}
	}
}
