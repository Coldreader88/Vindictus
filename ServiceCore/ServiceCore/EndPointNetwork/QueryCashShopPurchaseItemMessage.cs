using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCashShopPurchaseItemMessage : IMessage
	{
		public int ProductNo;

		public short Quantity;

		public string Attribute0;

		public string Attribute1;

		public string Attribute2;

		public string Attribute3;

		public string Attribute4;

		public bool IsForCommonInven;
	}
}
