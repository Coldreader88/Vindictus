using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradeMyItemInfos : IMessage
	{
		public ICollection<TradeItemInfo> TradeItemList { get; set; }

		public int result { get; set; }
	}
}
