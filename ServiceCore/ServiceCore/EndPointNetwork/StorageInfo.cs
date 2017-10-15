using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StorageInfo
	{
		public byte StorageID
		{
			get
			{
				return this.storageID;
			}
		}

		public byte IsAvailable
		{
			get
			{
				return this.isAvailable;
			}
		}

		public int LastSecsToExpire
		{
			get
			{
				return this.lastSecsToExpire;
			}
		}

		public string StorageName
		{
			get
			{
				return this.storageName;
			}
		}

		public int StorageTag
		{
			get
			{
				return this.storageTag;
			}
		}

		public StorageInfo(byte storageID, bool isAvailable, int lastSecs, string name, int tag)
		{
			this.storageID = storageID;
            this.isAvailable = (isAvailable ? (byte)1 : (byte)0);
            this.lastSecsToExpire = lastSecs;
			this.storageName = name;
			this.storageTag = tag;
		}

		private byte storageID;

		private byte isAvailable;

		private int lastSecsToExpire;

		private string storageName;

		private int storageTag;
	}
}
