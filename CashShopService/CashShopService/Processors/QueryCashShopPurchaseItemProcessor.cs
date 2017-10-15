using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class QueryCashShopPurchaseItemProcessor : EntityProcessor<QueryCashShopPurchaseItem, CashShopPeer>
	{
		public QueryCashShopPurchaseItemProcessor(CashShopService service, QueryCashShopPurchaseItem op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			CashShopPurchaseRequestArguments arg = new CashShopPurchaseRequestArguments
			{
				ProductNo = base.Operation.ProductNo,
				OrderQuantity = base.Operation.Quantity,
				Attribute0 = base.Operation.Attribute0,
				Attribute1 = base.Operation.Attribute1,
				Attribute2 = base.Operation.Attribute2,
				Attribute3 = base.Operation.Attribute3,
				Attribute4 = base.Operation.Attribute4
			};
			base.Finished = true;
			if (this.service.IsCashShopStopped)
			{
				base.Entity.SendErrorDialog("CashShop_EmergencyStop");
				yield return new FailMessage("[QueryCashShopPurchaseItemProcessor] service.IsCashShopStopped")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (base.Entity.CID == -1L)
			{
				base.Entity.SendErrorDialog("CashShop_NotReady");
				yield return new FailMessage("[QueryCashShopPurchaseItemProcessor] Entity.CID")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (base.Entity.BeginPurchaseItem(base.Operation.IsForCommonInven, arg) != null)
			{
				yield return new OkMessage();
			}
			else
			{
				yield return new FailMessage("[QueryCashShopPurchaseItemProcessor] Entity.BeginPurchaseItem")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			yield break;
		}

		private CashShopService service;
	}
}
