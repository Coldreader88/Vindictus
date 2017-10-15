using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QuerySharedInventory : Operation
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

		public override OperationProcessor RequestProcessor()
		{
			return new QuerySharedInventory.Request(this);
		}

		[NonSerialized]
		private ICollection<StorageInfo> storageInfo;

		[NonSerialized]
		private ICollection<SlotInfo> inventoryInfo;

		private class Request : OperationProcessor<QuerySharedInventory>
		{
			public Request(QuerySharedInventory op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is SharedInventoryInfo)
				{
					SharedInventoryInfo sharedInventoryInfo = base.Feedback as SharedInventoryInfo;
					base.Operation.storageInfo = sharedInventoryInfo.StorageInfo;
					base.Operation.inventoryInfo = sharedInventoryInfo.InventorySlotInfo;
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
