using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CashShopService.Processors
{
	public class QueryCashShopProductInfoByCashShopKeyProcessor : EntityProcessor<QueryCashShopProductInfoByCashShopKey, CashShopPeer>
	{
		public QueryCashShopProductInfoByCashShopKeyProcessor(CashShopService service, QueryCashShopProductInfoByCashShopKey op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.IsCashShopStopped)
			{
				base.Entity.SendErrorDialog("CashShop_EmergencyStop");
				base.Finished = true;
				yield return new FailMessage("[QueryCashShopProductInfoByCashShopKeyProcessor] service.IsCashShopStopped")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				List<int> productnos = new List<int>();
				int price = 0;
				foreach (CashShopItem product in base.Operation.QueryList)
				{
					CashShopProductListElement request;
					if (!this.service.ProductByCashShopItemKey.TryGetValue(product.Key, out request))
					{
						base.Finished = true;
						Log<QueryCashShopProductInfoByCashShopKeyProcessor>.Logger.ErrorFormat("No Cash Item : [{0}/{1}/{2}]", product.ItemClass, product.Price, product.Expire);
						yield return new FailMessage("[QueryCashShopProductInfoByCashShopKeyProcessor] product.Key")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
						yield break;
					}
					price += product.Price;
					productnos.Add(request.ProductNo);
				}
				base.Finished = true;
				yield return productnos;
				yield return price;
			}
			yield break;
		}

		private CashShopService service;
	}
}
