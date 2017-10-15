using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class MoveSharedItem : Operation
	{
		public long TargetItemID
		{
			get
			{
				return this.targetItem;
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

		public MoveSharedItem(long i, byte c, int t)
		{
			this.targetItem = i;
			this.targetCollection = c;
			this.targetSlot = t;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private long targetItem;

		private byte targetCollection;

		private int targetSlot;
	}
}
