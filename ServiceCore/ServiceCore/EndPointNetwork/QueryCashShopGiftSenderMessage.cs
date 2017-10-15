using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCashShopGiftSenderMessage : IMessage
	{
		public int OrderNo { get; set; }

		public override string ToString()
		{
			return string.Format("QueryCashShopGiftSenderMessage[]", new object[0]);
		}
	}
}
