using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopProductInfo : Operation
	{
		public int QueryProductID { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCashShopProductInfo.Request(this);
		}

		[NonSerialized]
		public CashShopProductListElement ReturnItem;

		private class Request : OperationProcessor<QueryCashShopProductInfo>
		{
			public Request(QueryCashShopProductInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.ReturnItem = (base.Feedback as CashShopProductListElement);
				if (base.Operation.ReturnItem == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
