using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopGiftSender : Operation
	{
		public int OrderNo { get; set; }

		public QueryCashShopGiftSender(int orederNo)
		{
			this.OrderNo = orederNo;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
