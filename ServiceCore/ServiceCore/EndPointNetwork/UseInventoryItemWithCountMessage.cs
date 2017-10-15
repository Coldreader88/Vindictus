using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseInventoryItemWithCountMessage : IMessage
	{
		public long ItemID { get; set; }

		public long TargetItemID { get; set; }

		public int TargetItemCount { get; set; }

		public UseInventoryItemWithCountMessage(long itemID, long targetItemID, int targetItemCount)
		{
			this.ItemID = itemID;
			this.TargetItemID = targetItemID;
			this.TargetItemCount = targetItemCount;
		}

		public override string ToString()
		{
			return string.Format("UseInventoryItemWithCountMessage[ itemID = {0}, targetItemID = {1}, targetItemCount = {2} ]", this.ItemID, this.TargetItemID, this.TargetItemCount);
		}
	}
}
