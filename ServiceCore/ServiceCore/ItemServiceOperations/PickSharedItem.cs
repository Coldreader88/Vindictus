using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class PickSharedItem : Operation
	{
		public long TargetItemID
		{
			get
			{
				return this.targetItem;
			}
		}

		public int Amount
		{
			get
			{
				return this.amount;
			}
		}

		public byte TargetCollection
		{
			get
			{
				return this.targetCollection;
			}
		}

		public int Target
		{
			get
			{
				return this.targetSlot;
			}
		}

		public PickSharedItem(long i, int a, byte c, int t)
		{
			this.targetItem = i;
			this.amount = a;
			this.targetCollection = c;
			this.targetSlot = t;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private long targetItem;

		private int amount;

		private byte targetCollection;

		private int targetSlot;
	}
}
