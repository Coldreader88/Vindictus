using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CashShopService.Processors
{
	public class DirectPickUpByProductNoProcessor : EntityProcessor<DirectPickUpByProductNo, CashShopPeer>
	{
		public DirectPickUpByProductNoProcessor(CashShopService service, DirectPickUpByProductNo op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.IsCashShopStopped)
			{
				base.Entity.SendErrorDialog("CashShop_EmergencyStop");
				base.Finished = true;
				yield return new FailMessage("[DirectPickUpByProductNoProcessor] service.IsCashShopStopped")
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
					yield return new FailMessage("[DirectPickUpByProductNoProcessor] Entity.IsConsecutivePurchase")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
				else
				{
					base.Entity.UpdatePurchaseTime(currentTime);
					List<CashShopPurchaseRequestArguments> args = new List<CashShopPurchaseRequestArguments>();
					Dictionary<string, int> resultDic = new Dictionary<string, int>();
					int price = 0;
					foreach (int productNo in base.Operation.ProductNoList)
					{
						CashShopProductListElement request;
						if (!this.service.ProductByProductID.TryGetValue(productNo, out request))
						{
							base.Finished = true;
							Log<DirectPickUpByProductNoProcessor>.Logger.ErrorFormat("No Cash Item : [{0}]", productNo);
							yield return new FailMessage("[DirectPickUpByProductNoProcessor] productNo")
							{
								Reason = FailMessage.ReasonCode.LogicalFail
							};
							yield break;
						}
						CashShopPurchaseRequestArguments arg = new CashShopPurchaseRequestArguments
						{
							ProductNo = request.ProductNo,
							OrderQuantity = 1,
							Attribute0 = "",
							Attribute1 = "",
							Attribute2 = "",
							Attribute3 = "",
							Attribute4 = ""
						};
						if (resultDic.ContainsKey(request.ProductID))
						{
							Dictionary<string, int> dictionary;
							string productID;
							(dictionary = resultDic)[productID = request.ProductID] = dictionary[productID] + 1;
						}
						else
						{
							resultDic.Add(request.ProductID, 1);
						}
						args.Add(arg);
						price += request.SalePrice;
					}
					AsyncResultSync sync = new AsyncResultSync(this.service.Thread);
					IAsyncResult ar = base.Entity.BeginDirectPurchaseItem(args, price, base.Operation.IsCredit, new AsyncCallback(sync.AsyncCallback), "DirectPurchase");
					if (ar != null)
					{
						yield return sync;
					}
					if (ar == null && !sync.Result)
					{
						base.Finished = true;
						string failReason = "BeginDirectPurchaseItem failed";
						yield return failReason;
						yield return new FailMessage("[DirectPickUpByProductNoProcessor] sync.Result")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
					}
					else
					{
						IList<string> value = base.Entity.EndDirectPurchaseItem(ar);
						base.Finished = true;
						if (value == null || value.Count != base.Operation.ProductNoList.Count)
						{
							string failReason = "EndDirectPurchaseItem failed.";
							yield return failReason;
							yield return new FailMessage("[DirectPickUpByProductNoProcessor] value.Count")
							{
								Reason = FailMessage.ReasonCode.LogicalFail
							};
						}
						else
						{
							yield return base.Entity.GetDirectPurchaseOrderNo(ar);
							yield return resultDic;
						}
					}
				}
			}
			yield break;
		}

		private CashShopService service;
	}
}
