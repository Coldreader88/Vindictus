using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RequestBraceletCombination : Operation
	{
		public long BraceletItemID { get; set; }

		public long GemstoneItemID { get; set; }

		public int GemstoneIndex { get; set; }

		public bool IsChanging { get; set; }

		public RequestBraceletCombination(long breaceItemID, long gemstoneItemID, int gemstoneIndex, bool isChanging)
		{
			this.BraceletItemID = breaceItemID;
			this.GemstoneItemID = gemstoneItemID;
			this.GemstoneIndex = gemstoneIndex;
			this.IsChanging = isChanging;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
