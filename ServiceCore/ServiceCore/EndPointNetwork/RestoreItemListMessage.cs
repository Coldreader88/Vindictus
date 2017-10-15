using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RestoreItemListMessage : IMessage
	{
		public ICollection<RestoreItemInfo> RestoreItemList { get; private set; }

		public RestoreItemListMessage(ICollection<RestoreItemInfo> list)
		{
			this.RestoreItemList = list;
		}

		public override string ToString()
		{
			return string.Format("RestoreItemListMessage : Count={0}", this.RestoreItemList.Count);
		}
	}
}
