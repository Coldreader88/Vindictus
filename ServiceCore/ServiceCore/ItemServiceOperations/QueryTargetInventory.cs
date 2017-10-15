using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryTargetInventory : Operation
	{
		public long CID { get; set; }

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

		public IDictionary<int, ColoredEquipment> EquipmentDictionary
		{
			get
			{
				Dictionary<int, ColoredEquipment> dictionary = new Dictionary<int, ColoredEquipment>();
				foreach (SlotInfo slotInfo in this.inventoryInfo)
				{
					foreach (KeyValuePair<int, long> keyValuePair in this.equipmentInfo)
					{
						if (slotInfo.ItemID == keyValuePair.Value)
						{
							string itemClass = ItemClassExBuilder.Build(slotInfo.ItemClass, slotInfo.Attributes);
							dictionary.Add(keyValuePair.Key, new ColoredEquipment
							{
								ItemClass = itemClass,
								Color1 = slotInfo.Color1,
								Color2 = slotInfo.Color2,
								Color3 = slotInfo.Color3
							});
							break;
						}
					}
				}
				return dictionary;
			}
		}

		public IDictionary<int, DurabilityEquipment> DuriabilityDictionary
		{
			get
			{
				Dictionary<int, DurabilityEquipment> dictionary = new Dictionary<int, DurabilityEquipment>();
				foreach (SlotInfo slotInfo in this.inventoryInfo)
				{
					foreach (KeyValuePair<int, long> keyValuePair in this.equipmentInfo)
					{
						if (slotInfo.ItemID == keyValuePair.Value)
						{
							dictionary.Add(keyValuePair.Key, new DurabilityEquipment(slotInfo.MaxDurabilityBonus, slotInfo.MaxDurability - slotInfo.Durability));
							break;
						}
					}
				}
				return dictionary;
			}
		}

		public int ItemCountFromInventoryInfo(string itemclass)
		{
			int num = 0;
			foreach (SlotInfo slotInfo in this.inventoryInfo)
			{
				if (slotInfo.ItemClass == itemclass)
				{
					num += slotInfo.Num;
				}
			}
			return num;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryTargetInventory.Request(this);
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

		private class Request : OperationProcessor<QueryTargetInventory>
		{
			public Request(QueryTargetInventory op) : base(op)
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
