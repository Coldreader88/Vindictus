using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class RollbackDirectPickUpProcessor : EntityProcessor<RollbackDirectPickUp, CashShopPeer>
	{
		public RollbackDirectPickUpProcessor(CashShopService service, RollbackDirectPickUp op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (!base.Entity.BeginDirectPickUpRollback(base.Operation.OrderNo, base.Operation.ProductNoList))
			{
				yield return new FailMessage("[RollbackDirectPickUpProcessor] Entity.BeginDirectPickUpRollback")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				yield return new OkMessage();
			}
			yield break;
		}

		private CashShopService service;
	}
}
