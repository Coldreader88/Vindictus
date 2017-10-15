using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateStorageStatusMessage : IMessage
	{
		public int StorageNo { get; set; }

		public string StorageName { get; set; }

		public int StorageTag { get; set; }
	}
}
