using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCashShopPurchaseGiftMessage : IMessage
	{
		public int ProductNo;

		public string TargetID;

		public string Message;

		public short Quantity;

		public string Attribute0;

		public string Attribute1;

		public string Attribute2;

		public string Attribute3;

		public string Attribute4;
	}
}
