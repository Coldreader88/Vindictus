using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class QueryCashShopRefundProcessor : EntityProcessor<QueryCashShopRefund, CashShopPeer>
	{
		public QueryCashShopRefundProcessor(CashShopService service, QueryCashShopRefund op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (base.Entity.BeginRefund(base.Operation.OrderNo, base.Operation.ProductNo) != null)
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("[QueryCashShopRefundProcessor] Entity.BeginRefund")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			yield break;
		}

		private CashShopService service;
	}
}
