using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class RepairItem : Operation
	{
		public ICollection<long> ItemIDs { get; set; }

		public bool AddAllEquippedItems { get; set; }

		public bool AddAllBrokenItems { get; set; }

		public bool AddAllRepairableItems { get; set; }

		public bool ReturnNotEnoughGold { get; set; }

		public RepairItem(ICollection<long> itemIDs, bool HideNotEnoughGold)
		{
			this.ItemIDs = (itemIDs ?? ((ICollection<long>)new long[0]));
			this.ReturnNotEnoughGold = HideNotEnoughGold;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
