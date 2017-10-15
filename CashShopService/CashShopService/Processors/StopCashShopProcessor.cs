using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using UnifiedNetwork.Cooperation;

namespace CashShopService.Processors
{
	internal class StopCashShopProcessor : OperationProcessor
	{
		public StopCashShopProcessor(CashShopService service, StopCashShop op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			this.service.IsCashShopStopped = (base.Operation as StopCashShop).TargetState;
			yield return new OkMessage();
			yield break;
		}

		private CashShopService service;
	}
}
