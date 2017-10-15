using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DestroySlotItemMessage : IMessage
	{
		public long ItemID
		{
			get
			{
				return this.itemID;
			}
		}

		public DestroySlotItemMessage(long iid)
		{
			this.itemID = iid;
		}

		public override string ToString()
		{
			return string.Format("DestroySlotItemMessage[ slot = {0} ]", this.itemID);
		}

		private long itemID;
	}
}
