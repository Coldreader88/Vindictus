using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradePurchaseItemResultMessage : IMessage
	{
		public int UniqueNumber { get; set; }

		public int Result { get; set; }

		public int LeftNumber { get; set; }
	}
}
