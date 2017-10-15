using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateSharedStorageInfoMessage : IMessage
	{
		public ICollection<StorageInfo> StorageInfos { get; private set; }

		public UpdateSharedStorageInfoMessage(ICollection<StorageInfo> info)
		{
			this.StorageInfos = info;
		}
	}
}
