using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RequestPartChanging : Operation
	{
		public long CombinedEquipItemID { get; set; }

		public int TargetIndex { get; set; }

		public long PartItemID { get; set; }

		public RequestPartChanging(long combinedEquipItemID, int targetIndex, long partItemID)
		{
			this.CombinedEquipItemID = combinedEquipItemID;
			this.TargetIndex = targetIndex;
			this.PartItemID = partItemID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
