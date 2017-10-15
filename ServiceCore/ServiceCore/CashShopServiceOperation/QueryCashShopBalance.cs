using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopBalance : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
