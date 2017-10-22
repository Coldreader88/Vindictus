using System;
using System.Collections.Generic;
using ServiceCore.TradeServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;

namespace TradeService
{
	public class ItemPriceInfoProcessor : OperationProcessor<ItemPriceInfo>
	{
		public ItemPriceInfoProcessor(Service service, ItemPriceInfo op) : base(op)
		{
			this.service = (service as TradeService);
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			yield return this.service.TradeItemAvgPrice.GetPricesPartially(base.Operation.PageNum);
			yield return !this.service.TradeItemAvgPrice.IsLastPage(base.Operation.PageNum);
			yield break;
		}

		private TradeService service;
	}
}
