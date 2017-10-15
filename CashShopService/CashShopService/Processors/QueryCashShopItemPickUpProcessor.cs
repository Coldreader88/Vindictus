using System;
using System.Collections.Generic;
using Nexon.Nisms.Packets;
using ServiceCore;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CashShopService.Processors
{
	public class QueryCashShopItemPickUpProcessor : EntityProcessor<QueryCashShopItemPickUp, CashShopPeer>
	{
		public QueryCashShopItemPickUpProcessor(CashShopService service, QueryCashShopItemPickUp op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.IsCashShopStopped)
			{
				base.Finished = true;
				base.Entity.SendErrorDialog("CashShop_EmergencyStop");
				yield return new FailMessage("[QueryCashShopItemPickUpProcessor] service.IsCashShopStopped")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else if (base.Entity.CID == -1L)
			{
				base.Finished = true;
				base.Entity.SendErrorDialog("CashShop_NotReady");
				yield return new FailMessage("[QueryCashShopItemPickUpProcessor] Entity.CID")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				AsyncResultSync sync = new AsyncResultSync(this.service.Thread);
				IAsyncResult ar = base.Entity.BeginInventoryPickup(base.Operation.OrderNo, base.Operation.ProductNo, base.Operation.Quantity, new AsyncCallback(sync.AsyncCallback), "DirectPurchase");
				if (ar != null)
				{
					yield return sync;
				}
				if (ar == null && !sync.Result)
				{
					base.Finished = true;
					yield return new FailMessage("[QueryCashShopItemPickUpProcessor] !sync.Result")
					{
						Reason = FailMessage.ReasonCode.LogicalFail
					};
				}
				else
				{
					InventoryPickupOnceResponse asyncResult = base.Entity.EndInventoryPickUp(ar);
					if (asyncResult == null)
					{
						base.Finished = true;
						yield return new FailMessage("[QueryCashShopItemPickUpProcessor] asyncResult")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
					}
					else
					{
						bool isGift = base.Entity.GiftSenderCIDDict.ContainsKey(base.Operation.OrderNo);
						GiveItem giveItemOp = asyncResult.ToGiveItem(this.service, isGift);
						if (giveItemOp != null && giveItemOp.ItemRequestInfo.Count > 0)
						{
							foreach (ItemRequestInfo.Element element in giveItemOp.ItemRequestInfo.Elements)
							{
								Log<QueryCashShopItemPickUpProcessor>.Logger.InfoFormat("{0} x {1}", element.ItemClassEx, element.Num);
							}
							OperationSync giveItemSync = new OperationSync
							{
								Connection = base.Entity.ItemConnection,
								Operation = giveItemOp
							};
							yield return giveItemSync;
							base.Entity.CashShopProcessLog(base.Operation.OrderNo, giveItemOp, giveItemSync.Result);
							if (!giveItemSync.Result || giveItemOp.ErrorCode != GiveItem.ResultEnum.Success)
							{
								Log<QueryCashShopItemPickUpProcessor>.Logger.ErrorFormat("GiveItemSync is failed. NexonID: {0}, CID: {1}", base.Entity.NexonID, base.Entity.CID);
								base.Entity.EndInventoryPickUp_OnFail(asyncResult);
								base.Finished = true;
								yield return new FailMessage("[QueryCashShopItemPickUpProcessor] giveItemOp.ErrorCode")
								{
									Reason = FailMessage.ReasonCode.LogicalFail
								};
							}
							OperationSync giveMileageSync = new OperationSync();
							bool useMileageSystem = FeatureMatrix.IsEnable("CashShop_MileageSystem");
							if (useMileageSystem)
							{
								double mileageRatio = (double)((float)FeatureMatrix.GetInteger("CashShop_MileageRatio") / 100f);
								int totalPrice = this.service.ProductByProductID.TryGetValue(base.Operation.ProductNo).SalePrice * (int)asyncResult.ProductPieces;
								int mileagePoint = Convert.ToInt32(Math.Floor((double)totalPrice * mileageRatio));
								mileagePoint = ((mileagePoint == 0) ? 1 : mileagePoint);
								CashshopMileage mileageOp = null;
								if (isGift)
								{
									long cid = base.Entity.GiftSenderCIDDict.TryGetValue(base.Operation.OrderNo);
									mileageOp = new CashshopMileage(cid, mileagePoint, CashshopMileage.ProcessEnum.ADD_GIFT_USER);
								}
								else
								{
									mileageOp = new CashshopMileage(0L, mileagePoint, CashshopMileage.ProcessEnum.ADD);
								}
								giveMileageSync.Connection = base.Entity.ItemConnection;
								giveMileageSync.Operation = mileageOp;
								yield return giveMileageSync;
								if (!giveMileageSync.Result)
								{
									Log<QueryCashShopItemPickUpProcessor>.Logger.ErrorFormat("GiveItemSync is failed. NexonID: {0}, CID: {1}", base.Entity.NexonID, base.Entity.CID);
									base.Entity.CashShopProcessLog("", base.Operation.OrderNo.ToString(), base.Operation.OrderNo, mileagePoint, base.Entity.CID, base.Entity.NexonSN, "Mileage", "GiveFail");
								}
							}
							base.Entity.EndInventoryPickUp_OnComplete(asyncResult);
							int tircoinCount = 0;
							foreach (ItemRequestInfo.Element element2 in giveItemOp.ItemRequestInfo.Elements)
							{
								if (element2.ItemClassEx.StartsWith("tir_coin"))
								{
									tircoinCount += element2.Num;
								}
							}
							if (tircoinCount > 0)
							{
								SendPacket op = SendPacket.Create<OpenCustomDialogUIMessage>(new OpenCustomDialogUIMessage
								{
									DialogType = 3,
									Arg = new List<string>
									{
										tircoinCount.ToString()
									}
								});
								base.Entity.FrontendConnection.RequestOperation(op);
								SystemMessage serializeObject = new SystemMessage(SystemMessageCategory.System, "GameUI_Heroes_SystemMessage_TirCoin_BuyOK", new object[]
								{
									tircoinCount
								});
								SendPacket op2 = SendPacket.Create<SystemMessage>(serializeObject);
								base.Entity.FrontendConnection.RequestOperation(op2);
							}
							base.Finished = true;
							yield return new OkMessage();
						}
						else
						{
							Log<QueryCashShopItemPickUpProcessor>.Logger.ErrorFormat("ToGiveItem is failed or there's no given item. NexonID: {0}, CID: {1}", base.Entity.NexonID, base.Entity.CID);
							base.Entity.EndInventoryPickUp_OnFail(asyncResult);
							base.Finished = true;
							yield return new FailMessage("[QueryCashShopItemPickUpProcessor] giveItemOp.ItemRequestInfo.Count")
							{
								Reason = FailMessage.ReasonCode.LogicalFail
							};
						}
					}
				}
			}
			yield break;
		}

		private CashShopService service;
	}
}
