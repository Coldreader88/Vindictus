using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseExtraStorageMessage : IMessage
	{
		public long ItemID
		{
			get
			{
				return this.itemID;
			}
		}

		public byte StorageID
		{
			get
			{
				return this.storageID;
			}
		}

		public UseExtraStorageMessage(long itemID, byte storageID)
		{
			this.itemID = itemID;
			this.storageID = storageID;
		}

		private long itemID;

		private byte storageID;
	}
}
