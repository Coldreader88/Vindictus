using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class QueryCashShopProductInfoByCashShopKey : Operation
	{
		public List<CashShopItem> QueryList { get; set; }

		public List<int> ProductNoList
		{
			get
			{
				return this.productNos;
			}
		}

		public int TotalPrice
		{
			get
			{
				return this.totalPrice;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCashShopProductInfoByCashShopKey.Request(this);
		}

		[NonSerialized]
		private List<int> productNos;

		[NonSerialized]
		private int totalPrice;

		private class Request : OperationProcessor<QueryCashShopProductInfoByCashShopKey>
		{
			public Request(QueryCashShopProductInfoByCashShopKey op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is List<int>)
				{
					base.Operation.productNos = (base.Feedback as List<int>);
					yield return null;
					base.Operation.totalPrice = (int)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
