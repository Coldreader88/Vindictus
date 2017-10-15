using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopItemPickUp : Operation
	{
		public int OrderNo { get; set; }

		public int ProductNo { get; set; }

		public short Quantity { get; set; }

		public QueryCashShopItemPickUp()
		{
		}

		public QueryCashShopItemPickUp(int orderNumber, int productNumber, short quantity)
		{
			this.OrderNo = orderNumber;
			this.ProductNo = productNumber;
			this.Quantity = quantity;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
