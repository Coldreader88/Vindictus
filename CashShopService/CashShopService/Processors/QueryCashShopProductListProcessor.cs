using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class QueryCashShopProductListProcessor : EntityProcessor<QueryCashShopProductList, CashShopPeer>
	{
		public QueryCashShopProductListProcessor(CashShopService service, QueryCashShopProductList op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (base.Entity.QueryProductList())
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("[QueryCashShopProductListProcessor] Entity.QueryProductList")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			yield break;
		}
	}
}
