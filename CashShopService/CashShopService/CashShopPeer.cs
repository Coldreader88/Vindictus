using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Linq.Expressions;
using Devcat.Core;
using Devcat.Core.Threading;
using Nexon.Nisms;
using Nexon.Nisms.Packets;
using ServiceCore;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.HeroesContents;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;


namespace CashShopService
{
	public class CashShopPeer
	{
		private CashShopService Service { get; set; }

		public IEntity Entity { get; private set; }

		public IEntityProxy ItemConnection
		{
			get
			{
				return this.itemConn;
			}
		}

		public IEntityProxy FrontendConnection
		{
			get
			{
				return this.frontendConn;
			}
		}

		public int NexonSN { get; set; }

		public string CharacterGameID
		{
			get
			{
				return string.Format("Char{0}", this.CID);
			}
		}

		public string NexonID { get; set; }

		public long FID { get; set; }

		public byte UserAge { get; set; }

		public long CID { get; set; }

		public int CharacterSN { get; set; }

		public IPAddress RemoteIPAddress { get; set; }

		public string CommonInvenGameID
		{
			get
			{
				return string.Format("Inven{0}", this.NexonSN);
			}
		}

		public Dictionary<int, long> GiftSenderCIDDict { get; set; }

		public CashShopPeer(CashShopService service, IEntity entity)
		{
			this.Service = service;
			this.Entity = entity;
			this.itemConn = null;
			this.frontendConn = null;
			this.NexonSN = 0;
			this.NexonID = "";
			this.UserAge = 0;
			this.CharacterSN = -1;
			this.CID = -1L;
			this.FID = -1L;
			this.RemoteIPAddress = IPAddress.None;
			this.GiftSenderCIDDict = new Dictionary<int, long>();
			this.Entity.Used += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				IEntityAdapter value = e.Value;
				if (value.RemoteCategory == "FrontendServiceCore.FrontendService" || this.Entity.UseCount == 0)
				{
					if (this.frontendConn != null && !this.frontendConn.IsClosed)
					{
						this.frontendConn.Close();
					}
					if (this.itemConn != null && !this.itemConn.IsClosed)
					{
						this.itemConn.Close();
					}
					this.Entity.Close();
					(this.Entity.Tag as CashShopPeer).CleanUp();
					Scheduler.Schedule(this.Service.Thread, Job.Create(delegate
					{
						if (!this.Entity.IsClosed)
						{
							this.Entity.Close(true);
						}
					}), new TimeSpan(0, 0, 30));
				}
			};
		}

		public void Ready()
		{
			if (this.FID != -1L && (this.frontendConn == null || this.frontendConn.IsClosed))
			{
				this.frontendConn = this.Service.Connect(this.Entity, new Location
				{
					ID = this.FID,
					Category = "FrontendServiceCore.FrontendService"
				});
			}
			if (this.CID != -1L)
			{
				if (this.itemConn == null || this.itemConn.IsClosed)
				{
					this.itemConn = this.Service.Connect(this.Entity, new Location
					{
						ID = this.CID,
						Category = "PlayerService.PlayerService"
					});
				}
				this.FlushUnsendedItem();
				return;
			}
			if (this.itemConn != null)
			{
				this.itemConn.Close();
				this.itemConn = null;
			}
		}

		public void CleanUp()
		{
			this.GiftSenderCIDDict.Clear();
			this.recoveryQueue.Clear();
		}

		internal void SendErrorDialog(string message)
		{
			SystemMessage serializeObject = new SystemMessage(SystemMessageCategory.Dialog, new HeroesString(string.Format("GameUI_Heroes_{0}", message)));
			SendPacket op = SendPacket.Create<SystemMessage>(serializeObject);
			this.FrontendConnection.RequestOperation(op);
		}

		public bool QueryProductList()
		{
			if (!this.Service.Connection.IsConnected)
			{
				return false;
			}
			CashShopCategoryListMessage serializeObject = new CashShopCategoryListMessage(this.Service.Categories);
			this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopCategoryListMessage>(serializeObject));
			CashShopProductListMessage serializeObject2 = new CashShopProductListMessage(this.Service.Products);
			this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopProductListMessage>(serializeObject2));
			return true;
		}

		public bool QueryBeautyShopInfo(BaseCharacter characterType)
		{
			if (this.Service.Connection.IsConnected)
			{
				List<CashShopProductListElement> list = new List<CashShopProductListElement>();
				List<BeautyShopCouponListElement> list2 = new List<BeautyShopCouponListElement>();
				foreach (CashShopProductListElement cashShopProductListElement in this.Service.BeautyShopProducts)
				{
					if (this.IsAvailableFor(cashShopProductListElement.ProductID, characterType))
					{
						list.Add(cashShopProductListElement);
					}
				}
				foreach (BeautyShopCouponListElement beautyShopCouponListElement in this.Service.BeautyShopCouponList)
				{
					if (this.IsAvailableFor(beautyShopCouponListElement.ItemClass, characterType))
					{
						list2.Add(beautyShopCouponListElement);
					}
				}
				BeautyShopInfoMessage serializeObject = new BeautyShopInfoMessage(this.Service.BeautyShopCategories.Values, list, list2);
				this.FrontendConnection.RequestOperation(SendPacket.Create<BeautyShopInfoMessage>(serializeObject));
				return true;
			}
			return false;
		}

		private bool IsAvailableFor(string itemClass, BaseCharacter character)
		{
			if (itemClass == null)
			{
				return false;
			}
			ItemClassInfo itemClassInfo;
			this.Service.ItemClassInfoDic.TryGetValue(itemClass, out itemClassInfo);
			if (itemClassInfo != null)
			{
				int num = 1 << (int)character;
				return Convert.ToBoolean(num & itemClassInfo.ClassRestriction);
			}
			return false;
		}

		public IAsyncResult BeginCheckBalance()
		{
			if (!this.Service.Connection.IsConnected)
			{
				return null;
			}
			if (this.Service.RefundlessBalanceEnabled)
			{
				return this.Service.Connection.BeginCheckBalanceEX(this.NexonID, new AsyncCallback(this.EndCheckBalance), null);
			}
			return this.Service.Connection.BeginCheckBalance(this.NexonID, new AsyncCallback(this.EndCheckBalance), null);
		}

		private void EndCheckBalance(IAsyncResult asyncResult)
		{
			if (this.Service.RefundlessBalanceEnabled)
			{
				CheckBalanceEXResponse checkBalanceEXResponse = this.Service.Connection.EndCheckBalanceEX(asyncResult);
				if (checkBalanceEXResponse == null || checkBalanceEXResponse.Result != Result.Successful)
				{
					return;
				}
				CashShopBalanceMessage serializeObject = new CashShopBalanceMessage(checkBalanceEXResponse.Balance, checkBalanceEXResponse.NoRefundBalance);
				this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopBalanceMessage>(serializeObject));
				return;
			}
			else
			{
				CheckBalanceResponse checkBalanceResponse = this.Service.Connection.EndCheckBalance(asyncResult);
				if (checkBalanceResponse == null || checkBalanceResponse.Result != Result.Successful)
				{
					return;
				}
				CashShopBalanceMessage serializeObject2 = new CashShopBalanceMessage(checkBalanceResponse.Balance, 0);
				this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopBalanceMessage>(serializeObject2));
				return;
			}
		}

		public IAsyncResult BeginInventoryInquiry(AsyncCallback callback, object state)
		{
			CashShopPeer.QueryInventoryResult queryInventoryResult = new CashShopPeer.QueryInventoryResult(state, callback);
			if (!this.Service.Connection.IsConnected)
			{
				return null;
			}
			this.GiftSenderCIDDict.Clear();
			if (this.CharacterGameID != "" && this.CID != -1L)
			{
				IAsyncResult asyncResult = this.Service.Connection.BeginInventoryInquiry(this.CharacterGameID, ShowInventory.True, 1, 64, new AsyncCallback(this.connect_EndInventoryInquiry), queryInventoryResult);
				queryInventoryResult.AsyncResult = asyncResult;
				queryInventoryResult.Replies.Add(asyncResult);
				return queryInventoryResult;
			}
			return null;
		}

		private void connect_EndInventoryInquiry(IAsyncResult asyncResult)
		{
			InventoryInquiryResponse response = this.Service.Connection.EndInventoryInquiry(asyncResult);
			CashShopPeer.QueryInventoryResult queryResult = asyncResult.AsyncState as CashShopPeer.QueryInventoryResult;
			if (response == null || response.Result != Result.Successful)
			{
				CashShopFailMessage serializeObject = new CashShopFailMessage((int)((response == null) ? Result.Failed : response.Result));
				this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopFailMessage>(serializeObject));
				queryResult.Complete(false);
				return;
			}
			this.Service.Thread.Enqueue(Job.Create(delegate
			{
				foreach (Order order in response.Order)
				{
					CashShopInventoryElement cashShopInventoryElement = new CashShopInventoryElement(order);
					queryResult.Result.Add(cashShopInventoryElement);
					if (cashShopInventoryElement.IsGift)
					{
						Log<CashShopPeer>.Logger.InfoFormat("Gift : [orderNo = {0}, senderCID = {1}]", cashShopInventoryElement.OrderNo, order.SenderGameID);
						long? cid = order.SenderGameID.GetCID();
						if (cid != null)
						{
							Log<CashShopPeer>.Logger.InfoFormat("Gift =>> [orderNo = {0}, senderCID = {1}]", cashShopInventoryElement.OrderNo, cid.Value);
							this.GiftSenderCIDDict.Add(cashShopInventoryElement.OrderNo, cid.Value);
						}
					}
				}
				if (queryResult.Replies.Contains(asyncResult))
				{
					queryResult.Replies.Remove(asyncResult);
				}
				queryResult.TryComplete();
			}));
		}

		public IList<CashShopInventoryElement> EndInventoryInquiry(IAsyncResult ar)
		{
			if (!(ar is CashShopPeer.QueryInventoryResult))
			{
				return null;
			}
			return (ar as CashShopPeer.QueryInventoryResult).Result;
		}

		public void FinishInventoryInquiry(IAsyncResult ar)
		{
			if (!(ar is CashShopPeer.QueryInventoryResult))
			{
				return;
			}
			(ar as CashShopPeer.QueryInventoryResult).CleanUp();
		}

		private void CashShopProcessLog(CashShopProcess row, string OrderType, string Event)
		{
			this.CashShopProcessLog("", row.OrderNo, row.ProductNo, row.Quantity, row.CID, row.NexonSN, OrderType, Event);
		}

		private void CashShopProcessLog(string orderno, CashShopProcess row, string OrderType, string Event)
		{
			this.CashShopProcessLog(row.OrderID, orderno, row.ProductNo, row.Quantity, row.CID, row.NexonSN, OrderType, Event);
		}

		public void CashShopProcessLog(int orderNo, GiveItem giveItemOp, bool result)
		{
			this.CashShopProcessLog("", orderNo.ToString(), 0, giveItemOp.ItemRequestInfo.Elements.Count, this.CID, this.NexonSN, "GiveItems", "GiveItems");
			foreach (ItemRequestInfo.Element element in giveItemOp.ItemRequestInfo.Elements)
			{
				this.CashShopProcessLog(element.ItemClassEx, orderNo.ToString(), 0, element.Num, this.CID, this.NexonSN, "", result ? "GiveItem" : "GiveItemFail");
			}
		}

		public void CashShopProcessLog(string OrderID, string OrderNo, int ProductNo, int Quantity, long CID, int NexonSN, string OrderType, string Event)
		{
			try
			{
				using (CashShopProcessLogDataContext cashShopProcessLogDataContext = new CashShopProcessLogDataContext())
				{
					cashShopProcessLogDataContext.AddCashShopProcessLog(OrderID, OrderNo, new int?(ProductNo), new int?(Quantity), new long?(CID), new int?(NexonSN), OrderType, Event);
				}
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
		}

		public DateTime? LastPurchaseTime { get; set; }

		public bool IsConsecutivePurchase(DateTime requestTime)
		{
			return this.LastPurchaseTime != null && this.LastPurchaseTime.Value + new TimeSpan(0, 0, 5) > requestTime;
		}

		public void UpdatePurchaseTime(DateTime requestTime)
		{
			this.LastPurchaseTime = new DateTime?(requestTime);
		}

		public IAsyncResult BeginInventoryPickup(int orderno, int productno, short quantity, AsyncCallback callback, object state)
		{
			CashShopPeer.PickUpResult pickUpResult = new CashShopPeer.PickUpResult(state, callback);
			if (!this.Service.Connection.IsConnected)
			{
				return null;
			}
			pickUpResult.OrderNo = orderno;
			pickUpResult.AsyncResult = this.Service.Connection.BeginInventoryPickupOnce(orderno, productno, quantity, "abc", new AsyncCallback(this.connect_EndInventoryPickup), pickUpResult);
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					CashShopProcess entity = new CashShopProcess
					{
						OrderNo = orderno.ToString(),
						ProductNo = productno,
						Quantity = (int)quantity,
						CID = this.CID,
						NexonSN = this.NexonSN,
						OrderType = 1,
						Requested = ((pickUpResult != null) ? 1 : 0)
					};
					cashShopProcessDataContext.CashShopProcess.InsertOnSubmit(entity);
					cashShopProcessDataContext.SubmitChanges();
				}
				this.CashShopProcessLog(orderno.ToString(), "", productno, (int)quantity, this.CID, this.NexonSN, "PickUp", "Ordered");
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
				return null;
			}
			return pickUpResult;
		}

		private void connect_EndInventoryPickup(IAsyncResult asyncResult)
		{
			InventoryPickupOnceResponse response = this.Service.Connection.EndInventoryPickupOnce(asyncResult);
			CashShopPeer.PickUpResult ar = asyncResult.AsyncState as CashShopPeer.PickUpResult;
			if (response == null || response.Result != Result.Successful)
			{
				CashShopFailMessage serializeObject = new CashShopFailMessage((int)((response == null) ? Result.Failed : response.Result));
				this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopFailMessage>(serializeObject));
				ar.Complete();
				return;
			}
			this.Service.Thread.Enqueue(Job.Create(delegate
			{
				ar.Result = response;
				ar.Complete();
			}));
		}

		public InventoryPickupOnceResponse EndInventoryPickUp(IAsyncResult asyncresult)
		{
			if (!(asyncresult is CashShopPeer.PickUpResult))
			{
				return null;
			}
			InventoryPickupOnceResponse ar = (asyncresult as CashShopPeer.PickUpResult).Result;
			if ((asyncresult as CashShopPeer.PickUpResult).Result == null)
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source = from row in cashShopProcessDataContext.CashShopProcess
					where row.OrderNo == (asyncresult as CashShopPeer.PickUpResult).OrderNo.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess in source.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(cashShopProcess, "PickUp", "OrderFailed");
						cashShopProcessDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess);
					}
					cashShopProcessDataContext.SubmitChanges();
				}
				return null;
			}
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext2 = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source2 = from row in cashShopProcessDataContext2.CashShopProcess
					where row.OrderNo == ar.OrderNo.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess2 in source2.AsEnumerable<CashShopProcess>())
					{
						cashShopProcess2.Accepted = 1;
						this.CashShopProcessLog(cashShopProcess2, "PickUp", "Accepted");
					}
					cashShopProcessDataContext2.SubmitChanges();
				}
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
			return ar;
		}

		public void EndInventoryPickUp_OnComplete(InventoryPickupOnceResponse ar)
		{
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source = from row in cashShopProcessDataContext.CashShopProcess
					where row.OrderNo == ar.OrderNo.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess in source.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(cashShopProcess, "PickUp", "Processed");
						cashShopProcessDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess);
					}
					cashShopProcessDataContext.SubmitChanges();
				}
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
			this.FlushUnsendedItem();
		}

		public void EndInventoryPickUp_OnFail(InventoryPickupOnceResponse ar)
		{
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source = from row in cashShopProcessDataContext.CashShopProcess
					where row.OrderNo == ar.OrderNo.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess in source.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(cashShopProcess, "PickUp", "ProcessFailed");
						cashShopProcessDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess);
					}
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					if (ar.SubProduct.Count == 0)
					{
						dictionary[ar.ProductID] = (int)(ar.OrderQuantity * ar.ProductPieces);
					}
					else
					{
						foreach (SubProduct subProduct in ar.SubProduct)
						{
							dictionary[subProduct.ProductID] = (int)subProduct.ProductPieces;
						}
					}
					if (this.recoveryQueue.ContainsKey(ar.OrderNo))
					{
						Log<CashShopPeer>.Logger.WarnFormat("중복된 PickUp Fail입니다 : [CID={0}, OrderNo={1}]", this.CID, ar.OrderNo);
						this.recoveryQueue[ar.OrderNo] = dictionary;
					}
					else
					{
						this.recoveryQueue.Add(ar.OrderNo, dictionary);
					}
					cashShopProcessDataContext.SubmitChanges();
				}
				this.BeginInventoryPickupRollback(ar.OrderNo, ar.ProductNo);
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
		}

		public IAsyncResult BeginInventoryPickupRollback(int orderno, int productno)
		{
			if (!this.Service.Connection.IsConnected)
			{
				return null;
			}
			this.SendErrorDialog("CashShop_PickUpFail");
			IAsyncResult result = this.Service.Connection.BeginInventoryPickupRollback(orderno, productno, "", new AsyncCallback(this.EndInventoryPickupRollback), null);
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					CashShopProcess entity = new CashShopProcess
					{
						OrderNo = orderno.ToString(),
						ProductNo = productno,
						Quantity = 0,
						CID = this.CID,
						NexonSN = this.NexonSN,
						OrderType = 2,
						Requested = 1,
						Accepted = 0
					};
					cashShopProcessDataContext.CashShopProcess.InsertOnSubmit(entity);
					cashShopProcessDataContext.SubmitChanges();
				}
				this.CashShopProcessLog("", orderno.ToString(), productno, 0, this.CID, this.NexonSN, "Rollback", "Ordered");
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
				return null;
			}
			return result;
		}

		private void EndInventoryPickupRollback(IAsyncResult asyncResult)
		{
			InventoryPickupRollbackResponse ar = this.Service.Connection.EndInventoryPickupRollback(asyncResult);
			if (ar == null || ar.Result != Result.Successful)
			{
				CashShopFailMessage serializeObject = new CashShopFailMessage((int)((ar == null) ? Result.Failed : ar.Result));
				this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopFailMessage>(serializeObject));
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source = from row in cashShopProcessDataContext.CashShopProcess
					where row.OrderNo == ar.OrderNo.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess in source.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(ar.OrderNo.ToString(), cashShopProcess, "Rollback", "OrderFailed");
						cashShopProcessDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess);
					}
					cashShopProcessDataContext.SubmitChanges();
				}
				if (ar != null)
				{
					Dictionary<string, int> dictionary;
					if (this.recoveryQueue.TryGetValue(ar.OrderNo, out dictionary))
					{
						using (CashShopProcessDataContext cashShopProcessDataContext2 = new CashShopProcessDataContext())
						{
							foreach (KeyValuePair<string, int> keyValuePair in dictionary)
							{
								CashShopProcess entity = new CashShopProcess
								{
									OrderID = keyValuePair.Key,
									OrderNo = ar.OrderNo.ToString(),
									Quantity = keyValuePair.Value,
									CID = this.CID,
									NexonSN = this.NexonSN,
									OrderType = 128
								};
								cashShopProcessDataContext2.CashShopProcess.InsertOnSubmit(entity);
							}
							cashShopProcessDataContext2.SubmitChanges();
							return;
						}
					}
					Log<CashShopPeer>.Logger.ErrorFormat("PickUp Rollback 기록이 없습니다 : [CID={0}, OrderNo={1}]", this.CID, ar.OrderNo);
				}
				return;
			}
			if (this.recoveryQueue.ContainsKey(ar.OrderNo))
			{
				this.recoveryQueue.Remove(ar.OrderNo);
			}
			else
			{
				Log<CashShopPeer>.Logger.ErrorFormat("PickUp Rollback 기록이 없습니다 : [CID={0}, OrderNo={1}]", this.CID, ar.OrderNo);
			}
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext3 = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source2 = from row in cashShopProcessDataContext3.CashShopProcess
					where row.OrderNo == ar.OrderNo.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess2 in source2.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(ar.OrderNo.ToString(), cashShopProcess2, "Rollback", "Accepted");
						cashShopProcessDataContext3.CashShopProcess.DeleteOnSubmit(cashShopProcess2);
					}
					cashShopProcessDataContext3.SubmitChanges();
				}
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
		}

		public void FlushUnsendedItem()
		{
			try
			{
				ItemRequestInfo itemRequestInfo = new ItemRequestInfo();
				List<string> ordernos = new List<string>();
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> queryable = from row in cashShopProcessDataContext.CashShopProcess
					where row.CID == this.CID && row.OrderType == 128
					select row;
					foreach (CashShopProcess cashShopProcess in queryable)
					{
						if (!ordernos.Contains(cashShopProcess.OrderNo))
						{
							ordernos.Add(cashShopProcess.OrderNo);
						}
						itemRequestInfo.Add(cashShopProcess.OrderID, cashShopProcess.Quantity);
					}
				}
				if (itemRequestInfo.Count != 0)
				{
					GiveItem op = new GiveItem(itemRequestInfo, GiveItem.FailMethodEnum.OperationFail, GiveItem.SourceEnum.CashShop);
					op.OnComplete += delegate(Operation result)
					{
						this.CashShopProcessLog(-1, op, true);
						using (CashShopProcessDataContext cashShopProcessDataContext2 = new CashShopProcessDataContext())
						{
							IQueryable<CashShopProcess> queryable2 = from row in cashShopProcessDataContext2.CashShopProcess
							where row.CID == this.CID && row.OrderType == 128
							select row;
							foreach (CashShopProcess entity in queryable2)
							{
								cashShopProcessDataContext2.CashShopProcess.DeleteOnSubmit(entity);
							}
							foreach (string orderNo in ordernos)
							{
								this.CashShopProcessLog("", orderNo, 0, 0, this.CID, this.NexonSN, "PickUp", "Fixed");
							}
							cashShopProcessDataContext2.SubmitChanges();
						}
					};
					op.OnFail += delegate(Operation _)
					{
						this.CashShopProcessLog(-1, op, false);
					};
					this.ItemConnection.RequestOperation(op);
				}
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while checking logs.", ex);
			}
		}

		public IAsyncResult BeginPurchaseItem(bool IsForCommonInven, CashShopPurchaseRequestArguments arg)
		{
			if (!this.Service.Connection.IsConnected)
			{
				return null;
			}
			string text = this.Service.MakeOrderID();
			IAsyncResult result = null;
			CashShopProductListElement cashShopProductListElement = null;
			if (this.Service.RefundlessBalanceEnabled)
			{
				List<PurchaseItemEXRequest.Product> list = new List<PurchaseItemEXRequest.Product>();
				PurchaseItemEXRequest.Product item = new PurchaseItemEXRequest.Product
				{
					ProductNo = arg.ProductNo,
					OrderQuantity = arg.OrderQuantity
				};
				int num = 0;
				if (this.Service.ProductByProductID.TryGetValue(arg.ProductNo, out cashShopProductListElement))
				{
					num += (int)arg.OrderQuantity * cashShopProductListElement.SalePrice;
					list.Add(item);
				}
				PurchaseItemEXRequest request = new PurchaseItemEXRequest
				{
					RemoteIP = this.RemoteIPAddress,
					Reason = Reason.GameClient,
					GameID = (IsForCommonInven ? this.CommonInvenGameID : this.CharacterGameID),
					UserID = (ServiceCore.FeatureMatrix.IsEnable("zhTW") ? this.NexonSN.ToString() : this.NexonID),
					UserOID = this.NexonSN,
					UserName = "",
					UserAge = this.UserAge,
					OrderID = text + (this.Service.MakeOrderIDWithRemoteIP ? ("/" + this.RemoteIPAddress.ToString()) : ""),
					PaymentType = PaymentType.NexonCash,
					TotalAmount = num,
					ProductArray = list,
					PaymentRule = PaymentRule.Refundless
				};
				result = this.Service.Connection.BeginPurchaseItemEX(request, new AsyncCallback(this.EndPurchaseItem), null);
			}
			else
			{
				List<PurchaseItemAttributeRequest.Product> list2 = new List<PurchaseItemAttributeRequest.Product>();
				PurchaseItemAttributeRequest.Product item2 = new PurchaseItemAttributeRequest.Product
				{
					ProductNo = arg.ProductNo,
					OrderQuantity = arg.OrderQuantity,
					Attribute0 = arg.Attribute0,
					Attribute1 = arg.Attribute1,
					Attribute2 = arg.Attribute2,
					Attribute3 = arg.Attribute3,
					Attribute4 = arg.Attribute4
				};
				int num2 = 0;
				if (this.Service.ProductByProductID.TryGetValue(arg.ProductNo, out cashShopProductListElement))
				{
					num2 += (int)arg.OrderQuantity * cashShopProductListElement.SalePrice;
					list2.Add(item2);
				}
				PurchaseItemAttributeRequest request2 = new PurchaseItemAttributeRequest
				{
					RemoteIP = this.RemoteIPAddress,
					Reason = Reason.GameClient,
					GameID = (IsForCommonInven ? this.CommonInvenGameID : this.CharacterGameID),
					UserID = (ServiceCore.FeatureMatrix.IsEnable("zhTW") ? this.NexonSN.ToString() : this.NexonID),
					UserOID = this.NexonSN,
					UserName = "",
					UserAge = this.UserAge,
					OrderID = text + (this.Service.MakeOrderIDWithRemoteIP ? ("/" + this.RemoteIPAddress.ToString()) : ""),
					PaymentType = PaymentType.NexonCash,
					TotalAmount = num2,
					ProductArray = list2
				};
				result = this.Service.Connection.BeginPurchaseItem(request2, new AsyncCallback(this.EndPurchaseItem), null);
			}
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					CashShopProcess entity = new CashShopProcess
					{
						OrderID = text,
						ProductNo = arg.ProductNo,
						Quantity = (int)arg.OrderQuantity,
						CID = this.CID,
						NexonSN = this.NexonSN,
						OrderType = 0,
						Requested = 1,
						Accepted = 0
					};
					cashShopProcessDataContext.CashShopProcess.InsertOnSubmit(entity);
					cashShopProcessDataContext.SubmitChanges();
				}
				this.CashShopProcessLog(text, "", arg.ProductNo, (int)arg.OrderQuantity, this.CID, this.NexonSN, "Purchase", "Ordered");
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Warn("Error while making logs", ex);
				return null;
			}
			CashShopProductListElement cashShopProductListElement2;
			this.Service.ProductByProductID.TryGetValue(arg.ProductNo, out cashShopProductListElement2);
			return result;
		}

		private void EndPurchaseItem(IAsyncResult asyncResult)
		{
			PurchaseItemResponse ar = null;
			if (this.Service.RefundlessBalanceEnabled)
			{
				ar = this.Service.Connection.EndPurchaseItemEX(asyncResult).ItemResponse;
			}
			else
			{
				ar = this.Service.Connection.EndPurchaseItem(asyncResult).ItemResponse;
			}
			if (ar == null || ar.Result != Result.Successful)
			{
				CashShopFailMessage serializeObject = new CashShopFailMessage((int)((ar == null) ? Result.Failed : ar.Result));
				this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopFailMessage>(serializeObject));
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source = from row in cashShopProcessDataContext.CashShopProcess
					where row.OrderID == ar.OrderID.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess in source.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(ar.OrderNo.ToString(), cashShopProcess, "Purchase", "OrderFailed");
						cashShopProcessDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess);
					}
					cashShopProcessDataContext.SubmitChanges();
				}
				return;
			}
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext2 = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source2 = from row in cashShopProcessDataContext2.CashShopProcess
					where row.OrderID == ar.OrderID.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess2 in source2.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(ar.OrderNo.ToString(), cashShopProcess2, "Purchase", "Accepted");
						cashShopProcessDataContext2.CashShopProcess.DeleteOnSubmit(cashShopProcess2);
						cashShopProcessDataContext2.SubmitChanges();
					}
				}
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
			this.BeginCheckBalance();
		}

		public IAsyncResult BeginDirectPurchaseItem(List<CashShopPurchaseRequestArguments> args, int Price, bool IsCredit, AsyncCallback callback, object state)
		{
			if (!this.Service.Connection.IsConnected)
			{
				return null;
			}
			CashShopPeer.DirectPurchaseItemResult directPurchaseItemResult = new CashShopPeer.DirectPurchaseItemResult(state, callback);
			string text = this.Service.MakeOrderID();
			if (this.Service.RefundlessBalanceEnabled)
			{
				List<PurchaseItemEXRequest.Product> list = new List<PurchaseItemEXRequest.Product>();
				int num = 0;
				foreach (CashShopPurchaseRequestArguments cashShopPurchaseRequestArguments in args)
				{
					PurchaseItemEXRequest.Product item = new PurchaseItemEXRequest.Product
					{
						ProductNo = cashShopPurchaseRequestArguments.ProductNo,
						OrderQuantity = cashShopPurchaseRequestArguments.OrderQuantity
					};
					list.Add(item);
					CashShopProductListElement cashShopProductListElement;
					if (!this.Service.ProductByProductID.TryGetValue(cashShopPurchaseRequestArguments.ProductNo, out cashShopProductListElement))
					{
						return null;
					}
					num += (int)cashShopPurchaseRequestArguments.OrderQuantity * cashShopProductListElement.SalePrice;
				}
				directPurchaseItemResult.PurchasedItemList = list;
				if (args.Count == 0 || num != Price)
				{
					Log<CashShopPeer>.Logger.ErrorFormat("Price mismatch : [{0} != {1}]", num, Price);
					return null;
				}
				PurchaseItemEXRequest request = new PurchaseItemEXRequest
				{
					RemoteIP = this.RemoteIPAddress,
					Reason = Reason.GameClient,
					GameID = this.CharacterGameID,
					UserID = (ServiceCore.FeatureMatrix.IsEnable("zhTW") ? this.NexonSN.ToString() : this.NexonID),
					UserOID = this.NexonSN,
					UserName = "",
					UserAge = this.UserAge,
					OrderID = text + (this.Service.MakeOrderIDWithRemoteIP ? ("/" + this.RemoteIPAddress.ToString()) : ""),
					PaymentType = PaymentType.NexonCash,
					TotalAmount = num,
					ProductArray = list,
					PaymentRule = (IsCredit ? PaymentRule.Refundable : PaymentRule.Refundless)
				};
				directPurchaseItemResult.AsyncResult = this.Service.Connection.BeginPurchaseItemEX(request, new AsyncCallback(this.Connection_EndDirectPurchaseItem), directPurchaseItemResult);
			}
			else
			{
				List<PurchaseItemAttributeRequest.Product> list2 = new List<PurchaseItemAttributeRequest.Product>();
				int num2 = 0;
				foreach (CashShopPurchaseRequestArguments cashShopPurchaseRequestArguments2 in args)
				{
					PurchaseItemAttributeRequest.Product item2 = new PurchaseItemAttributeRequest.Product
					{
						ProductNo = cashShopPurchaseRequestArguments2.ProductNo,
						OrderQuantity = cashShopPurchaseRequestArguments2.OrderQuantity,
						Attribute0 = cashShopPurchaseRequestArguments2.Attribute0,
						Attribute1 = cashShopPurchaseRequestArguments2.Attribute1,
						Attribute2 = cashShopPurchaseRequestArguments2.Attribute2,
						Attribute3 = cashShopPurchaseRequestArguments2.Attribute3,
						Attribute4 = cashShopPurchaseRequestArguments2.Attribute4
					};
					list2.Add(item2);
					CashShopProductListElement cashShopProductListElement2;
					if (!this.Service.ProductByProductID.TryGetValue(cashShopPurchaseRequestArguments2.ProductNo, out cashShopProductListElement2))
					{
						return null;
					}
					num2 += (int)cashShopPurchaseRequestArguments2.OrderQuantity * cashShopProductListElement2.SalePrice;
				}
				directPurchaseItemResult.PurchasedItemList = null;
				if (args.Count == 0 || num2 != Price)
				{
					Log<CashShopPeer>.Logger.ErrorFormat("Price Mismatch : [{0} != {1}]", num2, Price);
					return null;
				}
				PurchaseItemAttributeRequest request2 = new PurchaseItemAttributeRequest
				{
					RemoteIP = this.RemoteIPAddress,
					Reason = Reason.GameClient,
					GameID = this.CharacterGameID,
					UserID = (ServiceCore.FeatureMatrix.IsEnable("zhTW") ? this.NexonSN.ToString() : this.NexonID),
					UserOID = this.NexonSN,
					UserName = "",
					UserAge = this.UserAge,
					OrderID = text + (this.Service.MakeOrderIDWithRemoteIP ? ("/" + this.RemoteIPAddress.ToString()) : ""),
					PaymentType = PaymentType.NexonCash,
					TotalAmount = num2,
					ProductArray = list2
				};
				directPurchaseItemResult.AsyncResult = this.Service.Connection.BeginPurchaseItem(request2, new AsyncCallback(this.Connection_EndDirectPurchaseItem), directPurchaseItemResult);
			}
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					CashShopProcess entity = new CashShopProcess
					{
						OrderID = text,
						ProductNo = -1,
						Quantity = -1,
						CID = this.CID,
						NexonSN = this.NexonSN,
						OrderType = 0,
						Requested = 1,
						Accepted = 0
					};
					cashShopProcessDataContext.CashShopProcess.InsertOnSubmit(entity);
					cashShopProcessDataContext.SubmitChanges();
				}
				this.CashShopProcessLog(text, "", -1, -1, this.CID, this.NexonSN, "D.Purchase", "Ordered");
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
				return null;
			}
			int num3 = 0;
			foreach (CashShopPurchaseRequestArguments cashShopPurchaseRequestArguments3 in args)
			{
				num3++;
				CashShopProductListElement cashShopProductListElement3;
				this.Service.ProductByProductID.TryGetValue(cashShopPurchaseRequestArguments3.ProductNo, out cashShopProductListElement3);
			}
			return directPurchaseItemResult;
		}

        private void Connection_EndDirectPurchaseItem(IAsyncResult asyncResult)
        {
            CashShopPeer.DirectPurchaseItemResult ar = (CashShopPeer.DirectPurchaseItemResult)null;
            PurchaseItemResponse response = (PurchaseItemResponse)null;
            if (this.Service.RefundlessBalanceEnabled)
            {
                Log<CashShopPeer>.Logger.Info((object)"Purchased by EX routine");
                response = this.Service.Connection.EndPurchaseItemEX(asyncResult).ItemResponse;
            }
            else
            {
                Log<CashShopPeer>.Logger.Info((object)"Normal Purchased routine");
                response = this.Service.Connection.EndPurchaseItem(asyncResult).ItemResponse;
            }
            if (response == null || response.Result != Result.Successful || asyncResult == null)
            {
                CashShopFailMessage serializeObject = new CashShopFailMessage(response == null ? 0 : (int)response.Result);
                Log<CashShopPeer>.Logger.ErrorFormat("CashShop Error(Purchase) : [{0}]", (object)(response == null ? -1 : (int)response.Result));
                this.FrontendConnection.RequestOperation((Operation)SendPacket.Create<CashShopFailMessage>(serializeObject));
                if (response != null)
                {
                    using (CashShopProcessDataContext processDataContext = new CashShopProcessDataContext())
                    {
                        System.Data.Linq.Table<CashShopProcess> cashShopProcess1 = processDataContext.CashShopProcess;
                        Expression<Func<CashShopProcess, bool>> predicate = (Expression<Func<CashShopProcess, bool>>)(row => row.OrderID == response.OrderID.ToString());
                        foreach (CashShopProcess cashShopProcess2 in cashShopProcess1.Where<CashShopProcess>(predicate).AsEnumerable<CashShopProcess>())
                        {
                            int productNo = cashShopProcess2.ProductNo;
                            this.CashShopProcessLog(response.OrderNo.ToString(), cashShopProcess2, "D.Purchase", "OrderFailed");
                            processDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess2);
                        }
                        processDataContext.SubmitChanges();
                    }
                }
                if (asyncResult == null || !(asyncResult.AsyncState is CashShopPeer.DirectPurchaseItemResult))
                    return;
                (asyncResult.AsyncState as CashShopPeer.DirectPurchaseItemResult).Complete();
            }
            else
            {
                ar = asyncResult.AsyncState as CashShopPeer.DirectPurchaseItemResult;
                try
                {
                    using (CashShopProcessDataContext processDataContext = new CashShopProcessDataContext())
                    {
                        System.Data.Linq.Table<CashShopProcess> cashShopProcess1 = processDataContext.CashShopProcess;
                        Expression<Func<CashShopProcess, bool>> predicate = (Expression<Func<CashShopProcess, bool>>)(row => row.OrderID == response.OrderID.ToString());
                        foreach (CashShopProcess cashShopProcess2 in cashShopProcess1.Where<CashShopProcess>(predicate).AsEnumerable<CashShopProcess>())
                        {
                            this.CashShopProcessLog(response.OrderNo.ToString(), cashShopProcess2, "D.Purchase", "Accepted");
                            processDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess2);
                        }
                        processDataContext.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    Log<CashShopPeer>.Logger.Error((object)"Error while making logs", ex);
                }
                this.BeginCheckBalance();
                Scheduler.Schedule(this.Service.Thread, Job.Create((Action)(() =>
                {
                    ar.OrderNo = response.OrderNo;
                    Log<CashShopPeer>.Logger.InfoFormat("Product Array Length : {0}", (object)response.ProductArray.Count);
                    if (ar.PurchasedItemList != null)
                    {
                        foreach (PurchaseItemEXRequest.Product purchasedItem in (IEnumerable<PurchaseItemEXRequest.Product>)ar.PurchasedItemList)
                        {
                            try
                            {
                                using (CashShopProcessDataContext processDataContext = new CashShopProcessDataContext())
                                {
                                    CashShopProcess entity = new CashShopProcess()
                                    {
                                        OrderNo = response.OrderNo.ToString(),
                                        ProductNo = purchasedItem.ProductNo,
                                        Quantity = (int)purchasedItem.OrderQuantity,
                                        CID = this.CID,
                                        NexonSN = this.NexonSN,
                                        OrderType = 1,
                                        Requested = response != null ? 1 : 0
                                    };
                                    processDataContext.CashShopProcess.InsertOnSubmit(entity);
                                    processDataContext.SubmitChanges();
                                }
                                this.CashShopProcessLog(response.OrderNo.ToString(), "", purchasedItem.ProductNo, (int)purchasedItem.OrderQuantity, this.CID, this.NexonSN, "D.PickUp", "Ordered");
                            }
                            catch (Exception ex)
                            {
                                Log<CashShopPeer>.Logger.Error((object)"Error while making logs", ex);
                            }
                            ar.Pickups.Add(this.Service.Connection.BeginInventoryPickupOnce(response.OrderNo, purchasedItem.ProductNo, purchasedItem.OrderQuantity, "abc", new AsyncCallback(this.Connection_EndDirectPurchasePickup), asyncResult.AsyncState));
                        }
                    }
                    else
                    {
                        foreach (PurchaseItemResponse.Product product in (IEnumerable<PurchaseItemResponse.Product>)response.ProductArray)
                        {
                            try
                            {
                                using (CashShopProcessDataContext processDataContext = new CashShopProcessDataContext())
                                {
                                    CashShopProcess entity = new CashShopProcess()
                                    {
                                        OrderNo = response.OrderNo.ToString(),
                                        ProductNo = product.ProductNo,
                                        Quantity = (int)product.OrderQuantity,
                                        CID = this.CID,
                                        NexonSN = this.NexonSN,
                                        OrderType = 1,
                                        Requested = response != null ? 1 : 0
                                    };
                                    processDataContext.CashShopProcess.InsertOnSubmit(entity);
                                    processDataContext.SubmitChanges();
                                }
                                this.CashShopProcessLog(response.OrderNo.ToString(), "", product.ProductNo, (int)product.OrderQuantity, this.CID, this.NexonSN, "D.PickUp", "Ordered");
                            }
                            catch (Exception ex)
                            {
                                Log<CashShopPeer>.Logger.Error((object)"Error while making logs", ex);
                            }
                            ar.Pickups.Add(this.Service.Connection.BeginInventoryPickupOnce(response.OrderNo, product.ProductNo, product.OrderQuantity, "bbb", new AsyncCallback(this.Connection_EndDirectPurchasePickup), asyncResult.AsyncState));
                        }
                    }
                    ar.TryComplete();
                })), 1000);
            }
        }

        private void Connection_EndDirectPurchasePickup(IAsyncResult asyncResult)
		{
			InventoryPickupOnceResponse response = this.Service.Connection.EndInventoryPickupOnce(asyncResult);
			CashShopPeer.DirectPurchaseItemResult ar = asyncResult.AsyncState as CashShopPeer.DirectPurchaseItemResult;
			if (response == null || response.Result != Result.Successful)
			{
				CashShopFailMessage serializeObject = new CashShopFailMessage((int)((response == null) ? Result.Failed : response.Result));
				Log<CashShopPeer>.Logger.ErrorFormat("CashShop Error(PickUp) : [{0}]", (int)((response == null) ? Result.Failed : response.Result));
				this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopFailMessage>(serializeObject));
				if (response != null)
				{
					using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
					{
						IQueryable<CashShopProcess> source = from row in cashShopProcessDataContext.CashShopProcess
						where row.OrderNo == response.OrderNo.ToString()
						select row;
						foreach (CashShopProcess cashShopProcess in source.AsEnumerable<CashShopProcess>())
						{
							this.CashShopProcessLog(cashShopProcess, "D.PickUp", "OrderFailed");
							cashShopProcessDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess);
						}
						cashShopProcessDataContext.SubmitChanges();
					}
				}
				ar.Complete();
				return;
			}
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext2 = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source2 = from row in cashShopProcessDataContext2.CashShopProcess
					where row.OrderNo == response.OrderNo.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess2 in source2.AsEnumerable<CashShopProcess>())
					{
						cashShopProcessDataContext2.CashShopProcess.DeleteOnSubmit(cashShopProcess2);
						this.CashShopProcessLog(cashShopProcess2, "D.PickUp", "Accepted");
					}
					cashShopProcessDataContext2.SubmitChanges();
				}
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
			this.Service.Thread.Enqueue(Job.Create(delegate
			{
				ar.Result.Add(response.ProductID);
				Log<CashShopPeer>.Logger.InfoFormat("Picked Up : {0}", response.ProductID);
				if (ar.Pickups.Contains(asyncResult))
				{
					ar.Pickups.Remove(asyncResult);
				}
				ar.TryComplete();
			}));
		}

		public IList<string> EndDirectPurchaseItem(IAsyncResult ar)
		{
			if (ar == null || !(ar is CashShopPeer.DirectPurchaseItemResult))
			{
				return new List<string>();
			}
			return (ar as CashShopPeer.DirectPurchaseItemResult).Result;
		}

		public int GetDirectPurchaseOrderNo(IAsyncResult ar)
		{
			if (ar == null || !(ar is CashShopPeer.DirectPurchaseItemResult))
			{
				return -1;
			}
			return (ar as CashShopPeer.DirectPurchaseItemResult).OrderNo;
		}

		public IAsyncResult BeginPurchaseGift(CashShopPurchaseRequestArguments arg, string targetID, string message)
		{
			if (!this.Service.Connection.IsConnected)
			{
				return null;
			}
			string text = this.Service.MakeOrderID();
			List<PurchaseGiftAttributeRequest.Product> list = new List<PurchaseGiftAttributeRequest.Product>();
			PurchaseGiftAttributeRequest.Product item = new PurchaseGiftAttributeRequest.Product
			{
				ProductNo = arg.ProductNo,
				OrderQuantity = arg.OrderQuantity,
				Attribute0 = arg.Attribute0,
				Attribute1 = arg.Attribute1,
				Attribute2 = arg.Attribute2,
				Attribute3 = arg.Attribute3,
				Attribute4 = arg.Attribute4
			};
			int num = 0;
			CashShopProductListElement cashShopProductListElement;
			if (this.Service.ProductByProductID.TryGetValue(arg.ProductNo, out cashShopProductListElement))
			{
				num += (int)arg.OrderQuantity * cashShopProductListElement.SalePrice;
				list.Add(item);
			}
			PurchaseGiftAttributeRequest request = new PurchaseGiftAttributeRequest
			{
				RemoteIP = this.RemoteIPAddress,
				Reason = Reason.GameClient,
				GameID = this.CharacterGameID,
				UserID = (ServiceCore.FeatureMatrix.IsEnable("zhTW") ? this.NexonSN.ToString() : this.NexonID),
				UserOID = this.NexonSN,
				UserName = "",
				UserAge = this.UserAge,
				OrderID = text + (this.Service.MakeOrderIDWithRemoteIP ? ("/" + this.RemoteIPAddress.ToString()) : ""),
				PaymentType = PaymentType.NexonCash,
				TotalAmount = num,
				ProductArray = list,
				ReceiverServerNo = (byte)this.Service.ServerNumber,
				Message = message,
				ReceiverGameID = targetID
			};
			IAsyncResult result = this.Service.Connection.BeginPurchaseGift(request, new AsyncCallback(this.EndPurchaseGift), null);
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					CashShopProcess entity = new CashShopProcess
					{
						OrderID = text,
						ProductNo = arg.ProductNo,
						Quantity = (int)arg.OrderQuantity,
						CID = this.CID,
						NexonSN = this.NexonSN,
						OrderType = 3,
						Requested = 1,
						Accepted = 0
					};
					cashShopProcessDataContext.CashShopProcess.InsertOnSubmit(entity);
					cashShopProcessDataContext.SubmitChanges();
				}
				this.CashShopProcessLog(text, "", arg.ProductNo, (int)arg.OrderQuantity, this.CID, this.NexonSN, "Gift", "Ordered");
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
				return null;
			}
			return result;
		}

		private void EndPurchaseGift(IAsyncResult asyncResult)
		{
			PurchaseGiftAttributeResponse ar = this.Service.Connection.EndPurchaseGift(asyncResult);
			if (ar == null || ar.Result != Result.Successful)
			{
				CashShopFailMessage serializeObject = new CashShopFailMessage((int)((ar == null) ? Result.Failed : ar.Result));
				this.FrontendConnection.RequestOperation(SendPacket.Create<CashShopFailMessage>(serializeObject));
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source = from row in cashShopProcessDataContext.CashShopProcess
					where row.OrderID == ar.OrderID.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess in source.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(ar.OrderNo.ToString(), cashShopProcess, "Gift", "OrderFailed");
						cashShopProcessDataContext.CashShopProcess.DeleteOnSubmit(cashShopProcess);
					}
					cashShopProcessDataContext.SubmitChanges();
				}
				return;
			}
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext2 = new CashShopProcessDataContext())
				{
					IQueryable<CashShopProcess> source2 = from row in cashShopProcessDataContext2.CashShopProcess
					where row.OrderID == ar.OrderID.ToString()
					select row;
					foreach (CashShopProcess cashShopProcess2 in source2.AsEnumerable<CashShopProcess>())
					{
						this.CashShopProcessLog(ar.OrderNo.ToString(), cashShopProcess2, "Gift", "Accepted");
						cashShopProcessDataContext2.CashShopProcess.DeleteOnSubmit(cashShopProcess2);
					}
					cashShopProcessDataContext2.SubmitChanges();
				}
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
			this.BeginCheckBalance();
		}

		public bool BeginDirectPickUpRollback(int orderno, List<int> productlist)
		{
			if (!this.Service.Connection.IsConnected)
			{
				return false;
			}
			foreach (int productNo in productlist)
			{
				try
				{
					this.CashShopProcessLog("", orderno.ToString(), productNo, 0, this.CID, this.NexonSN, "Rollback", "Ordered");
				}
				catch (Exception ex)
				{
					Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
					return false;
				}
			}
			foreach (int productNo2 in productlist)
			{
				this.Service.Connection.BeginInventoryPickupRollback(orderno, productNo2, "", new AsyncCallback(this.EndDirectPickUpRollback), null);
			}
			return true;
		}

		private void EndDirectPickUpRollback(IAsyncResult result)
		{
			InventoryPickupRollbackResponse inventoryPickupRollbackResponse = this.Service.Connection.EndInventoryPickupRollback(result);
			string @event = "Processed";
			if (inventoryPickupRollbackResponse == null || inventoryPickupRollbackResponse.Result != Result.Successful)
			{
				@event = "ProcessFailed";
			}
			try
			{
				this.CashShopProcessLog("", inventoryPickupRollbackResponse.OrderNo.ToString(), inventoryPickupRollbackResponse.ProductNo, 0, this.CID, this.NexonSN, "Rollback", @event);
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
		}

		public IAsyncResult BeginRefund(int orderno, int productno)
		{
			if (!this.Service.Connection.IsConnected)
			{
				return null;
			}
			string str = this.Service.MakeOrderID();
			IAsyncResult result = this.Service.Connection.BeginPurchaseItemRefund(this.RemoteIPAddress, str + (this.Service.MakeOrderIDWithRemoteIP ? ("/" + this.RemoteIPAddress.ToString()) : ""), this.CharacterGameID, orderno, productno, 1, new AsyncCallback(this.EndRefund), null);
			try
			{
				this.CashShopProcessLog("", orderno.ToString(), 0, 0, this.CID, this.NexonSN, "Refund", "Ordered");
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
			return result;
		}

		private void EndRefund(IAsyncResult result)
		{
			PurchaseItemRefundResponse purchaseItemRefundResponse = this.Service.Connection.EndPurchaseItemRefund(result);
			string @event;
			if (purchaseItemRefundResponse == null || !purchaseItemRefundResponse.Result)
			{
				@event = "Failed";
				string message = "CashShop_RefundError_QueryError";
				if (purchaseItemRefundResponse != null)
				{
					switch (purchaseItemRefundResponse.ResultCode)
					{
					case 2:
					case 5:
					case 6:
						message = "CashShop_RefundError_RefundDisabled";
						break;
					case 4:
						message = "CashShop_RefundError_Expired";
						break;
					}
				}
				this.SendErrorDialog(message);
			}
			else
			{
				@event = "Refunded";
			}
			try
			{
				this.CashShopProcessLog("", purchaseItemRefundResponse.UsageNo.ToString(), 0, 0, this.CID, this.NexonSN, "Refund", @event);
			}
			catch (Exception ex)
			{
				Log<CashShopPeer>.Logger.Error("Error while making logs", ex);
			}
		}

		private const int ItemPerPage = 64;

		private IEntityProxy itemConn;

		private IEntityProxy frontendConn;

		private Dictionary<int, Dictionary<string, int>> recoveryQueue = new Dictionary<int, Dictionary<string, int>>();

		private enum CashShopOrderType
		{
			Purchase,
			PickUp,
			PickUpRollBack,
			Gift,
			Fix = 128
		}

		private class QueryInventoryResult : IAsyncResult
		{
			public object AsyncState { get; private set; }

			public WaitHandle AsyncWaitHandle { get; private set; }

			public bool CompletedSynchronously { get; private set; }

			public bool IsCompleted { get; private set; }

			internal IAsyncResult AsyncResult { get; set; }

			internal List<CashShopInventoryElement> Result { get; private set; }

			internal ICollection<IAsyncResult> Replies { get; set; }

			public QueryInventoryResult(object state, AsyncCallback callback)
			{
				this.AsyncState = state;
				this.AsyncWaitHandle = new ManualResetEvent(false);
				this.callback = callback;
				this.Result = new List<CashShopInventoryElement>();
				this.Replies = new HashSet<IAsyncResult>();
			}

			public void TryComplete()
			{
				if (0 < this.Replies.Count)
				{
					return;
				}
				this.Complete(false);
			}

			public void Complete()
			{
				this.Complete(false);
			}

			public void Complete(bool completedSynchronously)
			{
				this.CompletedSynchronously = completedSynchronously;
				this.IsCompleted = true;
				(this.AsyncWaitHandle as EventWaitHandle).Set();
				this.callback(this.AsyncResult);
			}

			public void CleanUp()
			{
				this.AsyncState = null;
				this.AsyncWaitHandle = null;
				this.AsyncResult = null;
				this.Result.Clear();
				this.Replies.Clear();
			}

			private AsyncCallback callback;
		}

		private class PickUpResult : IAsyncResult
		{
			public object AsyncState { get; private set; }

			public WaitHandle AsyncWaitHandle { get; private set; }

			public bool CompletedSynchronously { get; private set; }

			public bool IsCompleted { get; private set; }

			internal IAsyncResult AsyncResult { get; set; }

			internal InventoryPickupOnceResponse Result { get; set; }

			internal int OrderNo { get; set; }

			public PickUpResult(object state, AsyncCallback callback)
			{
				this.AsyncState = state;
				this.callback = callback;
				this.Result = null;
				this.OrderNo = -1;
			}

			public void Complete()
			{
				this.callback(this.AsyncResult);
			}

			private AsyncCallback callback;
		}

		private class DirectPurchaseItemResult : IAsyncResult
		{
			public object AsyncState { get; private set; }

			public WaitHandle AsyncWaitHandle { get; private set; }

			public bool CompletedSynchronously { get; private set; }

			public bool IsCompleted { get; private set; }

			internal IAsyncResult AsyncResult { get; set; }

			internal IList<string> Result { get; private set; }

			internal IList<PurchaseItemEXRequest.Product> PurchasedItemList { get; set; }

			internal ICollection<IAsyncResult> Pickups { get; set; }

			internal int OrderNo { get; set; }

			public DirectPurchaseItemResult(object state, AsyncCallback callback)
			{
				this.AsyncState = state;
				this.AsyncWaitHandle = new ManualResetEvent(false);
				this.callback = callback;
				this.Result = new List<string>();
				this.Pickups = new HashSet<IAsyncResult>();
			}

			public void TryComplete()
			{
				if (0 < this.Pickups.Count)
				{
					return;
				}
				this.Complete(false);
			}

			public void Complete()
			{
				this.Complete(false);
			}

			public void Complete(bool completedSynchronously)
			{
				this.CompletedSynchronously = completedSynchronously;
				this.IsCompleted = true;
				(this.AsyncWaitHandle as EventWaitHandle).Set();
				this.callback(this.AsyncResult);
			}

			private AsyncCallback callback;
		}
	}
}
