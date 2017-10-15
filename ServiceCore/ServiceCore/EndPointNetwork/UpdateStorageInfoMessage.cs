using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateStorageInfoMessage : IMessage
	{
		public ICollection<StorageInfo> StorageInfos { get; private set; }

		public UpdateStorageInfoMessage(ICollection<StorageInfo> info)
		{
			this.StorageInfos = info;
		}
	}
}
