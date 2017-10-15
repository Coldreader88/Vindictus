using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DyeItemCash : Operation
	{
		public long ItemID { get; set; }

		public long AmpleID { get; set; }

		public int Part { get; set; }

		public DyeItemCash(long itemID, long ampleID, int part)
		{
			this.ItemID = itemID;
			this.AmpleID = ampleID;
			this.Part = part;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
