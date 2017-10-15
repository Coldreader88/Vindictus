using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CashShopService.Processors
{
	public class DirectPickUpProcessor : EntityProcessor<DirectPickUp, CashShopPeer>
	{
		public DirectPickUpProcessor(CashShopService service, DirectPickUp op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.IsCashShopStopped)
			{
				base.Entity.SendErrorDialog("CashShop_EmergencyStop");
				base.Finished = true;
				yield return new FailMessage("[DirectPickUpProcessor] service.IsCashShopStopped")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				DateTime currentTime = DateTime.UtcNow;
				if (base.Entity.IsConsecutivePurchase(currentTime))
				{
					base.Entity.SendErrorDialog("CashShop_ConsecutivePurchase");
					base.Finished = true;
					yield return new FailMessage("[DirectPickUpProcessor] Entity.IsConsecutivePurchase")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
				else
				{
					base.Entity.UpdatePurchaseTime(currentTime);
					List<CashShopPurchaseRequestArguments> args = new List<CashShopPurchaseRequestArguments>();
					List<int> productnos = new List<int>();
					foreach (CashShopItem product in base.Operation.QueryList)
					{
						CashShopProductListElement request;
						if (!this.service.ProductByCashShopItemKey.TryGetValue(product.Key, out request))
						{
							base.Finished = true;
							Log<DirectPickUpProcessor>.Logger.ErrorFormat("No Cash Item : [{0}/{1}/{2}]", product.ItemClass, product.Price, product.Expire);
							yield return new FailMessage("[DirectPickUpProcessor] product.Key")
							{
								Reason = FailMessage.ReasonCode.LogicalFail
							};
							yield break;
						}
						CashShopPurchaseRequestArguments arg = new CashShopPurchaseRequestArguments
						{
							ProductNo = request.ProductNo,
							OrderQuantity = 1,
							Attribute0 = product.Attribute0,
							Attribute1 = product.Attribute1,
							Attribute2 = product.Attribute2,
							Attribute3 = product.Attribute3,
							Attribute4 = product.Attribute4
						};
						args.Add(arg);
						productnos.Add(request.ProductNo);
					}
					AsyncResultSync sync = new AsyncResultSync(this.service.Thread);
					IAsyncResult ar = base.Entity.BeginDirectPurchaseItem(args, base.Operation.TotalPrice, base.Operation.IsCredit, new AsyncCallback(sync.AsyncCallback), "DirectPurchase");
					if (ar != null)
					{
						yield return sync;
					}
					if (ar == null && !sync.Result)
					{
						base.Finished = true;
						foreach (CashShopPurchaseRequestArguments cashShopPurchaseRequestArguments in args)
						{
							Log<DirectPickUpProcessor>.Logger.ErrorFormat("[directPickupProcessor Fail - sync.Result : [ UTC : {0}] : {1}", DateTime.UtcNow, cashShopPurchaseRequestArguments.ProductNo);
						}
						yield return new FailMessage("[DirectPickUpProcessor] sync.Result")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
					}
					else
					{
						IList<string> value = base.Entity.EndDirectPurchaseItem(ar);
						base.Finished = true;
						if (value.Count != base.Operation.QueryList.Count)
						{
							foreach (CashShopPurchaseRequestArguments cashShopPurchaseRequestArguments2 in args)
							{
								Log<DirectPickUpProcessor>.Logger.ErrorFormat("[directPickupProcessor Fail - value.Count : [ UTC : {0}] : {1}", DateTime.UtcNow, cashShopPurchaseRequestArguments2.ProductNo);
							}
							yield return new FailMessage("[DirectPickUpProcessor] value.Count")
							{
								Reason = FailMessage.ReasonCode.LogicalFail
							};
						}
						else
						{
							yield return base.Entity.GetDirectPurchaseOrderNo(ar);
							yield return productnos;
						}
					}
				}
			}
			yield break;
		}

		private CashShopService service;
	}
}
