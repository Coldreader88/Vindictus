using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class BuyIngameCashshopUseTirMessage : IMessage
	{
		public List<int> Products { get; set; }
	}
}
