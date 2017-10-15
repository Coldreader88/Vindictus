using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class UseInventoryItemInQuest : Operation
	{
		public long CID { get; set; }

		public long ItemID { get; set; }

		public string ItemClass { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
