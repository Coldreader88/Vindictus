using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCashShopItemPickUpMessage : IMessage
	{
		public QueryCashShopItemPickUpMessage(int orderno, int productno, short quantity)
		{
			this.OrderNo = orderno;
			this.ProductNo = productno;
			this.Quantity = quantity;
		}

		public int OrderNo;

		public int ProductNo;

		public short Quantity;
	}
}
