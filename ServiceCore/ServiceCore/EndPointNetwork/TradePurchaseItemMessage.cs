using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradePurchaseItemMessage : IMessage
	{
		public long TID { get; set; }

		public int PurchaseCount { get; set; }

		public int UniqueNumber { get; set; }
	}
}
