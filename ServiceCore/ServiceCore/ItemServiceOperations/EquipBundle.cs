using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class EquipBundle : Operation
	{
		public string ItemClass { get; set; }

		public int QuickSlotID { get; set; }

		public EquipBundle(string itemClass, int quickSlotID)
		{
			this.ItemClass = itemClass;
			this.QuickSlotID = quickSlotID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
