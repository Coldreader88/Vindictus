using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public class ItemPriceInfoMessage : IMessage
	{
		public Dictionary<string, PriceRange> Prices { get; set; }
	}
}
