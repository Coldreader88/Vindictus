using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using UnifiedNetwork.OperationService;
using Utility;

namespace CashShopService.Processors
{
	public class QueryCashShopPurchaseGiftProcessor : EntityProcessor<QueryCashShopPurchaseGift, CashShopPeer>
	{
		public QueryCashShopPurchaseGiftProcessor(CashShopService service, QueryCashShopPurchaseGift op) : base(op)
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
			if (this.service.IsCashShopStopped)
			{
				base.Finished = true;
				base.Entity.SendErrorDialog("CashShop_EmergencyStop");
				yield return new FailMessage("[QueryCashShopPurchaseGiftProcessor] service.IsCashShopStopped")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (base.Entity.CID == -1L)
			{
				base.Finished = true;
				base.Entity.SendErrorDialog("CashShop_NotReady");
				yield return new FailMessage("[QueryCashShopPurchaseGiftProcessor] Entity.CID")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				QueryCIDByName queryCID = new QueryCIDByName(base.Operation.TargetName);
				UnifiedNetwork.OperationService.OperationSync queryCIDSync = new UnifiedNetwork.OperationService.OperationSync
				{
					Service = this.service,
					ServiceCategory = "PlayerService.PlayerService",
					Operation = queryCID
				};
				yield return queryCIDSync;
				if (!queryCIDSync.Result)
				{
					base.Finished = true;
					Log<QueryCashShopPurchaseGiftProcessor>.Logger.ErrorFormat("No cid for name.", base.Operation.TargetName);
					yield return new FailMessage("[QueryCashShopPurchaseGiftProcessor] queryCIDSync.Result")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
				else
				{
					base.Finished = true;
					if (base.Entity.BeginPurchaseGift(arg, string.Format("Char{0}", queryCID.CID), base.Operation.Message) != null)
					{
						yield return new OkMessage();
					}
					else
					{
						yield return new FailMessage("[QueryCashShopPurchaseGiftProcessor] Entity.BeginPurchaseGift")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
					}
				}
			}
			yield break;
		}

		private CashShopService service;
	}
}
