using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class UseInventoryItem : Operation
	{
		public long ItemID
		{
			get
			{
				return this.itemID;
			}
		}

		public List<long> SharingList { get; set; }

		public long TargetItemID { get; set; }

		public string TargetItemClass { get; set; }

		public long SenderCID { get; set; }

		public string SenderName { get; set; }

		public int MessageType { get; set; }

		public long TargetChannel { get; set; }

		public string Message { get; set; }

		public string ResetSkillID { get; set; }

		public int TargetRank { get; set; }

		public string TargetName { get; set; }

		public bool IsFromTiticore { get; set; }

		public int Position { get; set; }

		public int TargetItemCount { get; set; }

		public UseInventoryItem(long itemID, string targetName)
		{
			this.itemID = itemID;
			this.TargetName = targetName;
		}

		public UseInventoryItem(long itemID, string resetSkillID, int targetRank)
		{
			this.itemID = itemID;
			this.ResetSkillID = resetSkillID;
			this.TargetRank = targetRank;
		}

		public UseInventoryItem(long itemID, List<long> sharingList)
		{
			this.itemID = itemID;
			this.SharingList = sharingList;
		}

		public UseInventoryItem(long itemID, long targetItemID)
		{
			this.itemID = itemID;
			this.TargetItemID = targetItemID;
		}

		public UseInventoryItem(long itemID, bool isFromTiticore, string targetItemClass)
		{
			this.itemID = itemID;
			this.IsFromTiticore = isFromTiticore;
			this.TargetItemClass = targetItemClass;
		}

		public UseInventoryItem(long itemID, long senderCID, string senderName, int messageType, long channel, string message)
		{
			this.itemID = itemID;
			this.SenderCID = senderCID;
			this.SenderName = senderName;
			this.MessageType = messageType;
			this.TargetChannel = channel;
			this.Message = message;
		}

		public UseInventoryItem(long itemID, long targetItemID, int targetItemCount)
		{
			this.itemID = itemID;
			this.TargetItemID = targetItemID;
			this.TargetItemCount = targetItemCount;
		}

		public UseInventoryItem(long itemID, long targetItemID, string targetName)
		{
			this.itemID = itemID;
			this.TargetItemID = targetItemID;
			this.TargetName = targetName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private long itemID;
	}
}
