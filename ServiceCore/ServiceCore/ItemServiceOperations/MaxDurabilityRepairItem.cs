using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class MaxDurabilityRepairItem : Operation
	{
		public long TargetItemID { get; set; }

		public long SourceItemID { get; set; }

		public MaxDurabilityRepairItem(long targetItemID, long sourceItemID)
		{
			this.TargetItemID = targetItemID;
			this.SourceItemID = sourceItemID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
