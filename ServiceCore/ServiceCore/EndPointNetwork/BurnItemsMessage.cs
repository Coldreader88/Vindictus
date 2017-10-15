using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BurnItemsMessage : IMessage
	{
		public List<BurnItemInfo> BurnItemList { get; set; }
	}
}
