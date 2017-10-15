using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class RequestGoddessProtectionProcessor : EntityProcessor<RequestGoddessProtection, CashShopPeer>
	{
		public RequestGoddessProtectionProcessor(CashShopService service, RequestGoddessProtection op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.IsCashShopStopped)
			{
				base.Entity.SendErrorDialog("CashShop_EmergencyStop");
				base.Finished = true;
				yield return new FailMessage("[RequestGoddessProtectionProcessor] service.IsCashShopStopped")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (base.Entity.CID == -1L)
			{
				base.Finished = true;
				base.Entity.SendErrorDialog("CashShop_NotReady");
				yield return new FailMessage("[RequestGoddessProtectionProcessor] Entity.CID")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				List<CashShopPurchaseRequestArguments> args = new List<CashShopPurchaseRequestArguments>();
				string targetItem;
				if (base.Operation.RequestType == 0)
				{
					targetItem = "goddess_protection";
				}
				else
				{
					targetItem = "goddess_protection_personal";
				}
				CashShopProductListElement product;
				if (!this.service.ProductByItemClass.TryGetValue(targetItem, out product))
				{
					base.Finished = true;
					yield return new FailMessage("[RequestGoddessProtectionProcessor] targetItem")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
				else
				{
					CashShopPurchaseRequestArguments arg = new CashShopPurchaseRequestArguments
					{
						ProductNo = product.ProductNo,
						OrderQuantity = 1,
						Attribute0 = "",
						Attribute1 = "",
						Attribute2 = "",
						Attribute3 = "",
						Attribute4 = ""
					};
					args.Add(arg);
					AsyncResultSync sync = new AsyncResultSync(this.service.Thread);
					IAsyncResult ar = base.Entity.BeginDirectPurchaseItem(args, product.SalePrice, base.Operation.IsCredit, new AsyncCallback(sync.AsyncCallback), "GoddessProtection");
					if (ar != null)
					{
						yield return sync;
					}
					if (ar == null && !sync.Result)
					{
						base.Finished = true;
						yield return new FailMessage("[RequestGoddessProtectionProcessor] !sync.Result")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
					}
					else
					{
						IList<string> value = base.Entity.EndDirectPurchaseItem(ar);
						base.Finished = true;
						if (value.Count == 0)
						{
							yield return new FailMessage("[RequestGoddessProtectionProcessor] value.Count")
							{
								Reason = FailMessage.ReasonCode.LogicalFail
							};
						}
						else
						{
							yield return new OkMessage();
						}
					}
				}
			}
			yield break;
		}

		private CashShopService service;
	}
}
