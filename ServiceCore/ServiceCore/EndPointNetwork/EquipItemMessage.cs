using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EquipItemMessage : IMessage
	{
		public long ItemID
		{
			get
			{
				return this.itemID;
			}
		}

		public int PartID
		{
			get
			{
				return this.partID;
			}
		}

		public EquipItemMessage(long itemID, int partID)
		{
			this.itemID = itemID;
			this.partID = partID;
		}

		public override string ToString()
		{
			return string.Format("EquipItemMessage[ itemID = {0} partID = {1} ]", this.itemID, this.partID);
		}

		private long itemID;

		private int partID;
	}
}
