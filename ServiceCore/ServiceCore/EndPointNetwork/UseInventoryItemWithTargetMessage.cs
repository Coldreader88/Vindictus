using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseInventoryItemWithTargetMessage : IMessage
	{
		public long ItemID { get; set; }

		public string TargetName { get; set; }

		public UseInventoryItemWithTargetMessage(long itemID, string targetName)
		{
			this.ItemID = itemID;
			this.TargetName = targetName;
		}

		public override string ToString()
		{
			return string.Format("UseInventoryItemWithTargetMessage[ itemID = {0}, targetItemID = {1} ]", this.ItemID, this.TargetName);
		}
	}
}
