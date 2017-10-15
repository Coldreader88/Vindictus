using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class EquipBattleEquipments : Operation
	{
		public long OwnerCID { get; set; }

		public string ItemClass { get; set; }

		public int QuickSlotID { get; set; }

		public EquipBattleEquipments(long ownerCid, string itemClass)
		{
			this.OwnerCID = ownerCid;
			this.ItemClass = itemClass;
			this.QuickSlotID = 3;
		}

		public EquipBattleEquipments(long ownerCid, string itemClass, int quickSlotID)
		{
			this.OwnerCID = ownerCid;
			this.ItemClass = itemClass;
			this.QuickSlotID = quickSlotID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
