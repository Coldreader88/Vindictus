using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CashShopService.Processors
{
	public class QueryCashShopProductInfoProcessor : EntityProcessor<QueryCashShopProductInfo, CashShopPeer>
	{
		public QueryCashShopProductInfoProcessor(CashShopService service, QueryCashShopProductInfo op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (this.service.ProductByProductID == null)
			{
				Log<QueryCashShopProductInfoProcessor>.Logger.Error(" ProductList is Empty");
				yield return new FailMessage("[QueryCashShopProductInfoProcessor] service.ProductByProductID")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				CashShopProductListElement e = null;
				try
				{
					e = (from x in this.service.ProductByProductID
					where x.Key == base.Operation.QueryProductID
					select x).Single<KeyValuePair<int, CashShopProductListElement>>().Value;
				}
				catch (Exception ex)
				{
					Log<QueryCashShopProductInfoProcessor>.Logger.Fatal(" ProductID Not Exists.", ex);
				}
				if (e == null)
				{
					yield return new FailMessage("[QueryCashShopProductInfoProcessor] e")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
				else
				{
					yield return e;
				}
			}
			yield break;
		}

		private CashShopService service;
	}
}
