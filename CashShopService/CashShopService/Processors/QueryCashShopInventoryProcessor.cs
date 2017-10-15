using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class QueryCashShopInventoryProcessor : EntityProcessor<QueryCashShopInventory, CashShopPeer>
	{
		public QueryCashShopInventoryProcessor(CashShopService service, QueryCashShopInventory op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			AsyncResultSync sync = new AsyncResultSync(this.service.Thread);
			IAsyncResult ar = base.Entity.BeginInventoryInquiry(new AsyncCallback(sync.AsyncCallback), "DirectPurchase");
			if (ar != null)
			{
				yield return sync;
			}
			if (ar == null && !sync.Result)
			{
				base.Finished = true;
				yield return new FailMessage("[QueryCashShopInventoryProcessor] sync.Result")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				IList<CashShopInventoryElement> value = base.Entity.EndInventoryInquiry(ar);
				base.Finished = true;
				if (value == null)
				{
					yield return new FailMessage("[QueryCashShopInventoryProcessor] value")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
				else
				{
					yield return value;
				}
				base.Entity.FinishInventoryInquiry(ar);
			}
			yield break;
		}

		private CashShopService service;
	}
}
