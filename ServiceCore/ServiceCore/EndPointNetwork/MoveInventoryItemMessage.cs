using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MoveInventoryItemMessage : IMessage
	{
		public long ItemID
		{
			get
			{
				return this.targetItemID;
			}
			set
			{
				this.targetItemID = value;
			}
		}

		public byte Storage
		{
			get
			{
				return this.targetStorage;
			}
			set
			{
				this.targetStorage = value;
			}
		}

		public int Target
		{
			get
			{
				return this.targetSlot;
			}
			set
			{
				this.targetSlot = value;
			}
		}

		public override string ToString()
		{
			return string.Format("MoveInventoryItemMessage [ targetItem = {0} targetStorage = {1} targetSlot = {2} ]", this.targetItemID, this.targetStorage, this.targetSlot);
		}

		private long targetItemID;

		private byte targetStorage;

		private int targetSlot;
	}
}
