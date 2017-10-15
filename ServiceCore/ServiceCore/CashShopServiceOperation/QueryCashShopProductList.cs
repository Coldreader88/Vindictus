using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopProductList : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
