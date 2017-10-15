using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopRefund : Operation
	{
		public int OrderNo { get; set; }

		public int ProductNo { get; set; }

		public QueryCashShopRefund(int orderno, int productno)
		{
			this.OrderNo = orderno;
			this.ProductNo = productno;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
