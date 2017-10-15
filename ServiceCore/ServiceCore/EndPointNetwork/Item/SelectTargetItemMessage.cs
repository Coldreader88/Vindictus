using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class SelectTargetItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public List<long> TargetItems { get; set; }

		public string LocalTextKey { get; set; }

		public SelectTargetItemMessage(long itemID, List<long> targetItems, string localTextKey)
		{
			this.ItemID = itemID;
			this.TargetItems = targetItems;
			this.LocalTextKey = localTextKey;
		}

		public override string ToString()
		{
			return string.Format("SelectTargetItemMessage[ itemID = {0}, TargetItems x {1} LocalTextKey = {2}]", this.ItemID, this.TargetItems.Count, this.LocalTextKey);
		}
	}
}
