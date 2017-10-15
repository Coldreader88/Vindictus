using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Threading;
using Nexon.Nisms.Packets;

namespace Nexon.Nisms
{
	public class Connection : IDisposable
	{
		public bool IsConnected
		{
			get
			{
				return this.connection.Connected;
			}
		}

		public event EventHandler<EventArgs<Exception>> ConnectionFail;

		public event EventHandler<EventArgs> Disconnected;

		public event EventHandler<EventArgs> ConnectionSucceed;

		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		public void Dispose()
		{
			if (this.connection != null)
			{
				this.connection.ConnectionFail -= this.OnConnectionFail;
				this.connection.Disconnected -= this.OnDisconnected;
				this.connection.ExceptionOccur -= this.OnExceptionOccur;
				this.connection.PacketReceive -= this.connection_PacketReceive;
				if (this.connection.Connected)
				{
					this.connection.Disconnect();
				}
				this.connection = null;
			}
		}

		public ICollection<Category> CategoryArray { get; private set; }

		public ICollection<Product> ProductArray { get; private set; }

		public event EventHandler<EventArgs> CategoryArrayChanged;

		public event EventHandler<EventArgs> ProductArrayChanged;

		public Connection(string packetEncodingType)
		{
			this.RegistEncodingType(packetEncodingType);
			this.CategoryArray = new Category[0];
			this.ProductArray = new Product[0];
			this.handlers[1] = ((Packet packet) => new InitializeResponse(ref packet));
			this.handlers[2] = ((Packet packet) => new HeartBeatResponse(ref packet));
			this.handlers[97] = delegate(Packet packet)
			{
				CategoryInquiryResponse categoryInquiryResponse = new CategoryInquiryResponse(ref packet);
				if (categoryInquiryResponse.Result == Result.Successful)
				{
					this.CategoryArray = categoryInquiryResponse.CategoryArray;
					if (this.CategoryArrayChanged != null)
					{
						this.CategoryArrayChanged(this, EventArgs.Empty);
					}
				}
				return categoryInquiryResponse;
			};
			this.handlers[85] = delegate(Packet packet)
			{
				ProductInquiryResponse productInquiryResponse = new ProductInquiryResponse(ref packet);
				this.releaseTicks = productInquiryResponse.ReleaseTicks;
				if (productInquiryResponse.Result == Result.Successful)
				{
					this.ProductArray = productInquiryResponse.ProductArray;
					if (this.ProductArrayChanged != null)
					{
						this.ProductArrayChanged(this, EventArgs.Empty);
					}
				}
				return productInquiryResponse;
			};
			this.handlers[17] = ((Packet packet) => new CheckBalanceResponse(ref packet));
			this.handlers[18] = ((Packet packet) => new CheckBalanceEXResponse(ref packet));
			this.handlers[36] = ((Packet packet) => new PurchaseItemAttributeResponse(ref packet));
			this.handlers[35] = ((Packet packet) => new PurchaseItemEXResponse(ref packet));
			this.handlers[37] = ((Packet packet) => new PurchaseGiftAttributeResponse(ref packet));
			this.handlers[65] = ((Packet packet) => new InventoryInquiryResponse(ref packet));
			this.handlers[73] = ((Packet packet) => new InventoryInquiryReadResponse(ref packet));
			this.handlers[74] = ((Packet packet) => new InventoryPickupResponse(ref packet));
			this.handlers[76] = ((Packet packet) => new InventoryPickupOnceResponse(ref packet));
			this.handlers[75] = ((Packet packet) => new InventoryPickupRollbackResponse(ref packet));
			this.handlers[72] = ((Packet packet) => new InventoryClearResponse(ref packet));
			this.handlers[128] = ((Packet packet) => new PurchaseItemRefundResponse(ref packet));
			this.connection.ConnectionSucceed += this.OnConnectionSucceed;
			this.connection.ConnectionFail += this.OnConnectionFail;
			this.connection.ConnectionSucceed += this.connection_ConnectionSucceed;
			this.connection.Disconnected += this.OnDisconnected;
			this.connection.ExceptionOccur += this.OnExceptionOccur;
			this.connection.PacketReceive += this.connection_PacketReceive;
		}

		public void Connect(JobProcessor thread, IPEndPoint endPoint, string serviceCode, byte serverNo)
		{
			this.thread = thread;
			this.serviceCode = serviceCode;
			this.serverNo = serverNo;
			this.connection.Connect(this.thread, endPoint, new PacketAnalyzer());
		}

		private void OnConnectionSucceed(object sender, EventArgs e)
		{
			if (this.ConnectionSucceed != null)
			{
				this.ConnectionSucceed(sender, e);
			}
		}

		private void OnConnectionFail(object sender, EventArgs<Exception> e)
		{
			if (this.ConnectionFail != null)
			{
				this.ConnectionFail(sender, e);
			}
		}

		private void connection_ConnectionSucceed(object sender, EventArgs e)
		{
			this.BeginInitialize(this.serviceCode, this.serverNo, delegate(IAsyncResult ar)
			{
				this.CategoryInquery(null);
			}, null);
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			if (this.Disconnected != null)
			{
				this.Disconnected(sender, e);
			}
		}

		private void OnExceptionOccur(object sender, EventArgs<Exception> e)
		{
			if (this.ExceptionOccur != null)
			{
				this.ExceptionOccur(sender, e);
			}
		}

		private void connection_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet arg = new Packet(e.Value);
			Func<Packet, object> func = this.handlers[(int)arg.PacketType];
			if (func != null)
			{
				object arg2 = func(arg);
				this.thread.Enqueue(Job.Create<int, object>(new Action<int, object>(this.Process), arg.PacketNo, arg2));
			}
		}

		private AsyncResult Transmit(Packet packet, AsyncCallback callback, object state)
		{
			AsyncResult asyncResult = new AsyncResult(callback, state);
			if (this.queries.ContainsKey(packet.PacketNo))
			{
				asyncResult.Complete(true);
				return asyncResult;
			}
			this.queries.Add(packet.PacketNo, asyncResult);
			this.connection.Transmit<Packet>(packet);
			return asyncResult;
		}

		private void Process(int packetNo, object response)
		{
			AsyncResult asyncResult;
			if (this.queries.TryGetValue(packetNo, out asyncResult))
			{
				this.queries.Remove(packetNo);
				asyncResult.Response = response;
				asyncResult.Complete();
			}
		}

		public static Encoding EncodeType
		{
			get
			{
				if (Connection.encodeType == null)
				{
					return Encoding.Default;
				}
				return Connection.encodeType;
			}
			private set
			{
				if (value != null)
				{
					Connection.encodeType = value;
				}
			}
		}

		private void RegistEncodingType(string customEncoding)
		{
			switch (customEncoding)
			{
			case "Unicode":
			case "UTF16":
			case "UTF-16":
				Connection.EncodeType = Encoding.Unicode;
				return;
			}
			Connection.EncodeType = Encoding.Default;
		}

		private IAsyncResult BeginInitialize(string serviceCode, byte serverNo, AsyncCallback callback, object state)
		{
			InitializeRequest initializeRequest = new InitializeRequest(serviceCode, serverNo);
			return this.Transmit(initializeRequest.Serialize(), callback, state);
		}

		private InitializeResponse EndInitialize(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as InitializeResponse;
		}

		private IAsyncResult BeginHeartBeat(DateTime releaseTicks, AsyncCallback callback, object state)
		{
			HeartBeatRequest heartBeatRequest = new HeartBeatRequest(releaseTicks);
			return this.Transmit(heartBeatRequest.Serialize(), callback, state);
		}

		private HeartBeatResponse EndHeartBeat(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as HeartBeatResponse;
		}

		private IAsyncResult BeginCategoryInquiry(AsyncCallback callback, object state)
		{
			CategoryInquiryRequest categoryInquiryRequest = new CategoryInquiryRequest();
			return this.Transmit(categoryInquiryRequest.Serialize(), callback, state);
		}

		private CategoryInquiryResponse EndCategoryInquiry(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as CategoryInquiryResponse;
		}

		private IAsyncResult BeginProductInquiry(int pageIndex, int rowPerPage, AsyncCallback callback, object state)
		{
			ProductInquiryRequest productInquiryRequest = new ProductInquiryRequest(pageIndex, rowPerPage);
			return this.Transmit(productInquiryRequest.Serialize(), callback, state);
		}

		private ProductInquiryResponse EndProductInquiry(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as ProductInquiryResponse;
		}

		public IAsyncResult BeginCheckBalance(string userID, AsyncCallback callback, object state)
		{
			CheckBalanceRequest checkBalanceRequest = new CheckBalanceRequest(userID);
			return this.Transmit(checkBalanceRequest.Serialize(), callback, state);
		}

		public CheckBalanceResponse EndCheckBalance(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as CheckBalanceResponse;
		}

		public IAsyncResult BeginCheckBalanceEX(string userID, AsyncCallback callback, object state)
		{
			CheckBalanceEXRequest checkBalanceEXRequest = new CheckBalanceEXRequest(userID);
			return this.Transmit(checkBalanceEXRequest.Serialize(), callback, state);
		}

		public CheckBalanceEXResponse EndCheckBalanceEX(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as CheckBalanceEXResponse;
		}

		public IAsyncResult BeginPurchaseItem(PurchaseItemAttributeRequest request, AsyncCallback callback, object state)
		{
			return this.Transmit(request.Serialize(), callback, state);
		}

		public PurchaseItemAttributeResponse EndPurchaseItem(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as PurchaseItemAttributeResponse;
		}

		public IAsyncResult BeginPurchaseItemEX(PurchaseItemEXRequest request, AsyncCallback callback, object state)
		{
			return this.Transmit(request.Serialize(), callback, state);
		}

		public PurchaseItemEXResponse EndPurchaseItemEX(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as PurchaseItemEXResponse;
		}

		public IAsyncResult BeginPurchaseGift(PurchaseGiftAttributeRequest request, AsyncCallback callback, object state)
		{
			return this.Transmit(request.Serialize(), callback, state);
		}

		public PurchaseGiftAttributeResponse EndPurchaseGift(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as PurchaseGiftAttributeResponse;
		}

		public IAsyncResult BeginInventoryInquiry(string gameID, ShowInventory showInventory, int pageIndex, int rowPerPage, AsyncCallback callback, object state)
		{
			InventoryInquiryRequest inventoryInquiryRequest = new InventoryInquiryRequest(gameID, showInventory, pageIndex, rowPerPage);
			return this.Transmit(inventoryInquiryRequest.Serialize(), callback, state);
		}

		public InventoryInquiryResponse EndInventoryInquiry(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as InventoryInquiryResponse;
		}

		public IAsyncResult BeginInventoryInquiryRead(int orderNo, int productNo, AsyncCallback callback, object state)
		{
			InventoryInquiryReadRequest inventoryInquiryReadRequest = new InventoryInquiryReadRequest(orderNo, productNo);
			return this.Transmit(inventoryInquiryReadRequest.Serialize(), callback, state);
		}

		public InventoryInquiryReadResponse EndInventoryInquiryRead(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as InventoryInquiryReadResponse;
		}

		public IAsyncResult BeginInventoryPickup(int orderNo, int productNo, short orderQuantity, string extendValue, AsyncCallback callback, object state)
		{
			InventoryPickupRequest inventoryPickupRequest = new InventoryPickupRequest(orderNo, productNo, orderQuantity, extendValue);
			return this.Transmit(inventoryPickupRequest.Serialize(), callback, state);
		}

		public InventoryPickupResponse EndInventoryPickup(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as InventoryPickupResponse;
		}

		public IAsyncResult BeginInventoryPickupOnce(int orderNo, int productNo, short orderQuantity, string extendValue, AsyncCallback callback, object state)
		{
			InventoryPickupOnceRequest inventoryPickupOnceRequest = new InventoryPickupOnceRequest(orderNo, productNo, orderQuantity, extendValue);
			return this.Transmit(inventoryPickupOnceRequest.Serialize(), callback, state);
		}

		public InventoryPickupOnceResponse EndInventoryPickupOnce(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as InventoryPickupOnceResponse;
		}

		public IAsyncResult BeginInventoryPickupRollback(int orderNo, int productNo, string extendValue, AsyncCallback callback, object state)
		{
			InventoryPickupRollbackRequest inventoryPickupRollbackRequest = new InventoryPickupRollbackRequest(orderNo, productNo, extendValue);
			return this.Transmit(inventoryPickupRollbackRequest.Serialize(), callback, state);
		}

		public InventoryPickupRollbackResponse EndInventoryPickupRollback(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as InventoryPickupRollbackResponse;
		}

		public IAsyncResult BeginInventoryClear(string gameID, string processMessage, short orderQuantity, string extendValue, AsyncCallback callback, object state)
		{
			InventoryClearRequest inventoryClearRequest = new InventoryClearRequest(gameID, processMessage);
			return this.Transmit(inventoryClearRequest.Serialize(), callback, state);
		}

		public InventoryClearResponse EndInventoryClear(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as InventoryClearResponse;
		}

		public IAsyncResult BeginPurchaseItemRefund(IPAddress remoteIP, string requestID, string gameID, int usageNo, int productNo, short quantity, AsyncCallback callback, object state)
		{
			PurchaseItemRefundRequest purchaseItemRefundRequest = new PurchaseItemRefundRequest
			{
				GameID = gameID,
				RemoteIP = remoteIP,
				UsageNo = usageNo,
				RequestID = requestID,
				ProductNo = productNo,
				Quantity = quantity
			};
			return this.Transmit(purchaseItemRefundRequest.Serialize(), callback, state);
		}

		public PurchaseItemRefundResponse EndPurchaseItemRefund(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as PurchaseItemRefundResponse;
		}

		private void HeartBeat(DateTime ticks)
		{
			this.BeginHeartBeat(this.releaseTicks, new AsyncCallback(this.CategoryInquery), null);
		}

		private void CategoryInquery(IAsyncResult ar)
		{
			HeartBeatResponse heartBeatResponse = this.EndHeartBeat(ar);
			if (heartBeatResponse == null || heartBeatResponse.Result == Result.ProductListUpdated)
			{
				this.BeginCategoryInquiry(new AsyncCallback(this.ProductInquery), null);
			}
			Scheduler.Schedule(this.thread, Job.Create<DateTime>(new Action<DateTime>(this.HeartBeat), this.releaseTicks), new TimeSpan(0, 2, 0));
		}

		private void ProductInquery(IAsyncResult ar)
		{
			this.BeginProductInquiry(1, int.MaxValue, null, null);
		}

		private TcpClient connection = new TcpClient();

		private JobProcessor thread;

		private string serviceCode;

		private byte serverNo;

		private DateTime releaseTicks;

		private Func<Packet, object>[] handlers = new Func<Packet, object>[256];

		private IDictionary<int, AsyncResult> queries = new Dictionary<int, AsyncResult>();

		private static Encoding encodeType;
	}
}
