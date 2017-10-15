using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCashShopRefundMessage : IMessage
	{
		public int OrderNo { get; set; }

		public int ProductNo { get; set; }
	}
}
