using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class QueryCashShopBalanceProcessor : EntityProcessor<QueryCashShopBalance, CashShopPeer>
	{
		public QueryCashShopBalanceProcessor(CashShopService service, QueryCashShopBalance op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (base.Entity.BeginCheckBalance() != null)
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("[QueryCashShopBalanceProcessor] Entity.BeginCheckBalance")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			yield break;
		}
	}
}
