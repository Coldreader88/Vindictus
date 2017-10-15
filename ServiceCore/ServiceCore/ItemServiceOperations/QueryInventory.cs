using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryInventory : Operation
	{
		public ICollection<StorageInfo> StorageInfo
		{
			get
			{
				return this.storageInfo;
			}
		}

		public ICollection<SlotInfo> InventoryInfo
		{
			get
			{
				return this.inventoryInfo;
			}
		}

		public IDictionary<int, long> EquipmentInfo
		{
			get
			{
				return this.equipmentInfo;
			}
		}

		public QuickSlotInfo QuickSlotInfo
		{
			get
			{
				return this.quickSlotInfo;
			}
		}

		public ICollection<int> UnequippableParts
		{
			get
			{
				return this.unequippableParts;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryInventory.Request(this);
		}

		[NonSerialized]
		private ICollection<StorageInfo> storageInfo;

		[NonSerialized]
		private ICollection<SlotInfo> inventoryInfo;

		[NonSerialized]
		private IDictionary<int, long> equipmentInfo;

		[NonSerialized]
		private QuickSlotInfo quickSlotInfo;

		[NonSerialized]
		private ICollection<int> unequippableParts;

		private class Request : OperationProcessor<QueryInventory>
		{
			public Request(QueryInventory op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is InventoryInfo)
				{
					InventoryInfo inventoryInfo = base.Feedback as InventoryInfo;
					base.Operation.storageInfo = inventoryInfo.StorageInfo;
					base.Operation.inventoryInfo = inventoryInfo.InventorySlotInfo;
					base.Operation.equipmentInfo = inventoryInfo.EquipmentInfo;
					base.Operation.quickSlotInfo = inventoryInfo.QuickSlotInfo;
					base.Operation.unequippableParts = inventoryInfo.UnequippableParts;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
