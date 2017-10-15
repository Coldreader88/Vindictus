using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradeSearchResult : IMessage
	{
		public ICollection<TradeItemInfo> TradeItemList { get; set; }

		public int UniqueNumber { get; set; }

		public bool IsMoreResult { get; set; }

		public int result { get; set; }
	}
}
