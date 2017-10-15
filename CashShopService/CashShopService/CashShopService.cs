using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CashShopService.Processors;
using CashShopService.Properties;
using CashShopService.WishListSystem;
using Devcat.Core;
using Devcat.Core.Threading;
using Nexon.Nisms;
using ServiceCore;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.Configuration;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.HeroesContents;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CashShopService
{
	public class CashShopService : Service
	{
		public Connection Connection { get; private set; }

		public int ServerNumber
		{
			get
			{
				return (int)ServiceCoreSettings.Default.CashShopServerNumber;
			}
		}

		public int NISMSGameCode
		{
			get
			{
				return ServiceCoreSettings.Default.NISMSGameCode;
			}
		}

		public bool RefundlessBalanceEnabled
		{
			get
			{
				return ServiceCoreSettings.Default.CashShopRefundlessBalance;
			}
		}

		public bool MakeOrderIDWithRemoteIP
		{
			get
			{
				return ServiceCoreSettings.Default.MakeOrderIDWithRemoteIP;
			}
		}

		public int BeautyShopCategoryNo { get; private set; }

		public string BeautyShopCategoryName { get; private set; }

		public int BeautyShopIgnoreCategoryNo { get; private set; }

		public string BeautyShopIgnoreCategoryName { get; private set; }

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			base.Initialize(thread, CashShopServiceOperations.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterProcessor(typeof(NotifyCashShopUserInfo), (Operation x) => new NotifyCashShopUserInfoProcessor(this, x as NotifyCashShopUserInfo));
			base.RegisterProcessor(typeof(NotifyCashShopCID), (Operation x) => new NotifyCashShopCIDProcessor(this, x as NotifyCashShopCID));
			base.RegisterProcessor(typeof(QueryCashShopBalance), (Operation x) => new QueryCashShopBalanceProcessor(this, x as QueryCashShopBalance));
			base.RegisterProcessor(typeof(QueryCashShopInventory), (Operation x) => new QueryCashShopInventoryProcessor(this, x as QueryCashShopInventory));
			base.RegisterProcessor(typeof(QueryCashShopItemPickUp), (Operation x) => new QueryCashShopItemPickUpProcessor(this, x as QueryCashShopItemPickUp));
			base.RegisterProcessor(typeof(QueryCashShopPurchaseItem), (Operation x) => new QueryCashShopPurchaseItemProcessor(this, x as QueryCashShopPurchaseItem));
			base.RegisterProcessor(typeof(QueryCashShopProductList), (Operation x) => new QueryCashShopProductListProcessor(this, x as QueryCashShopProductList));
			base.RegisterProcessor(typeof(StopCashShop), (Operation x) => new StopCashShopProcessor(this, x as StopCashShop));
			base.RegisterProcessor(typeof(QueryCashShopPurchaseGift), (Operation x) => new QueryCashShopPurchaseGiftProcessor(this, x as QueryCashShopPurchaseGift));
			base.RegisterProcessor(typeof(RequestGoddessProtection), (Operation x) => new RequestGoddessProtectionProcessor(this, x as RequestGoddessProtection));
			base.RegisterProcessor(typeof(DirectPickUp), (Operation x) => new DirectPickUpProcessor(this, x as DirectPickUp));
			base.RegisterProcessor(typeof(QueryCashShopGiftSender), (Operation x) => new QueryCashShopGiftSenderProcessor(this, x as QueryCashShopGiftSender));
			base.RegisterProcessor(typeof(RollbackDirectPickUp), (Operation x) => new RollbackDirectPickUpProcessor(this, x as RollbackDirectPickUp));
			base.RegisterProcessor(typeof(DirectPickUpByProductNo), (Operation x) => new DirectPickUpByProductNoProcessor(this, x as DirectPickUpByProductNo));
			base.RegisterProcessor(typeof(QueryCashShopRefund), (Operation x) => new QueryCashShopRefundProcessor(this, x as QueryCashShopRefund));
			base.RegisterProcessor(typeof(QueryBeautyShopInfo), (Operation x) => new QueryBeautyShopInfoProcessor(this, x as QueryBeautyShopInfo));
			base.RegisterProcessor(typeof(QueryCashShopProductInfo), (Operation x) => new QueryCashShopProductInfoProcessor(this, x as QueryCashShopProductInfo));
			base.RegisterProcessor(typeof(QueryCashShopProductInfoByCashShopKey), (Operation x) => new QueryCashShopProductInfoByCashShopKeyProcessor(this, x as QueryCashShopProductInfoByCashShopKey));
			base.RegisterProcessor(typeof(WishListSelect), (Operation x) => new WishListSelectProcessor(this, x as WishListSelect));
			base.RegisterProcessor(typeof(WishListInsert), (Operation x) => new WishListInsertProcessor(this, x as WishListInsert));
			base.RegisterProcessor(typeof(WishListDelete), (Operation x) => new WishListDeleteProcessor(this, x as WishListDelete));
			this.IsCashShopStopped = false;
			ServiceCore.FeatureMatrix.GetString("");
			this.IsCashShopServiceUseable = (ServiceCore.FeatureMatrix.LangTag != "KO-KR-X-DEV");
			string text = ServiceCore.FeatureMatrix.GetString("NISMSEncodingType");
			if (text == "")
			{
				text = "Default";
				Log<CashShopService>.Logger.WarnFormat("can't find NISMSEncodingType feature. EncodingType will set {0} type.", text);
			}
			this.Connection = new Connection(text);
			this.Connection.CategoryArrayChanged += this.Connection_CategoryArrayChanged;
			this.Connection.ProductArrayChanged += this.Connection_ProductArrayChanged;
			this.Connection.Disconnected += delegate(object sender, EventArgs e)
			{
				Log<CashShopService>.Logger.Fatal("CashShop Disconnected!");
				if (this.IsCashShopServiceUseable)
				{
					Scheduler.Schedule(base.Thread, Job.Create<bool>(new Action<bool>(this.connectToCashShop), true), 60000);
				}
			};
			this.Connection.ConnectionFail += delegate(object sender, EventArgs<Exception> e)
			{
				Log<CashShopService>.Logger.Fatal("CashShop Connection Failed...");
				Scheduler.Schedule(base.Thread, Job.Create<bool>(new Action<bool>(this.connectToCashShop), true), 60000);
			};
			this.Connection.ConnectionSucceed += delegate(object sender, EventArgs e)
			{
				Log<CashShopService>.Logger.Info("CashShop Connected.");
				this.IsCashShopServiceUseable = true;
			};
			Scheduler.Schedule(base.Thread, Job.Create<bool>(new Action<bool>(this.connectToCashShop), false), 0);
			if (ServiceCore.FeatureMatrix.IsEnable("BeautyShopItemSyncNISMS"))
			{
				try
				{
					string cashShopType = ServiceCore.FeatureMatrix.GetString("LanguageTag");
					this.CustomizeCouponInfoList = (from x in HeroesContentsLoader.GetTable<CustomizeItemInfo>()
					where (x.Weight == -1 || x.Weight == -2) && (x.CashShopType ?? cashShopType) == cashShopType
					select x).ToList<CustomizeItemInfo>();
					foreach (CustomizeItemInfo customizeItemInfo in this.CustomizeCouponInfoList)
					{
						BeautyShopCouponListElement item = new BeautyShopCouponListElement(customizeItemInfo.Category, customizeItemInfo.ItemClass, customizeItemInfo.Weight);
						this.BeautyShopCouponList.Add(item);
					}
					this.ItemClassInfoDic = (from x in HeroesContentsLoader.GetTable<ItemClassInfo>()
					where ServiceCore.FeatureMatrix.IsEnable(x.Feature)
					select x).ToDictionary_OverwriteDuplicated((ItemClassInfo x) => x.ItemClass);
				}
				catch (Exception ex)
				{
					Log<CashShopService>.Logger.Error(ex);
				}
			}
			WishListManager function = new WishListManager(this);
			base.RegisterFunction<WishListManager>(function);
		}

		private void connectToCashShop(bool retry)
		{
			if (this.Connection.IsConnected)
			{
				Log<CashShopService>.Logger.Info("Already Connected to cashshop : Ignored.");
				return;
			}
			try
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(ServiceCoreSettings.Default.CashShopIPAddress);
				int num = 0;
				if (num < hostAddresses.Length)
				{
					IPAddress address = hostAddresses[num];
					IPEndPoint ipendPoint = new IPEndPoint(address, ServiceCoreSettings.Default.CashShopPort);
					Log<CashShopService>.Logger.InfoFormat("Connecting to CashShop : {0}", ipendPoint.ToString());
					this.Connection.Connect(base.Thread, ipendPoint, ServiceCoreSettings.Default.CashShopServiceCode, ServiceCoreSettings.Default.CashShopServerNumber);
				}
			}
			catch (Exception ex)
			{
				Log<CashShopService>.Logger.Error(ex);
			}
		}

		public override int CompareAndSwapServiceID(long id, string category, int beforeID)
		{
			if (category != base.Category)
			{
				return -1;
			}
			int result;
			using (EntityDataContext entityDataContext = new EntityDataContext())
			{
				result = entityDataContext.AcquireService(new long?(id), base.Category, new int?(base.ID), new int?(beforeID));
			}
			return result;
		}

		protected override IEntity MakeEntity(long id, string category)
		{
			IEntity entity = base.MakeEntity(id, category);
			if (entity == null)
			{
				return null;
			}
			entity.Closed += this.entity_Closed;
			entity.Tag = new CashShopPeer(this, entity);
			return entity;
		}

		private void entity_Closed(object sender, EventArgs e)
		{
			try
			{
				EntityDataContext entityDataContext = new EntityDataContext();
				entityDataContext.AcquireService(new long?((sender as IEntity).ID), base.Category, new int?(-1), new int?(base.ID));
			}
			catch (Exception ex)
			{
				Log<CashShopService>.Logger.ErrorFormat("Entity_Closed [EntityID : {0}] [ServiceID : {1}] [Category : {2}] - {3} ", new object[]
				{
					(sender as IEntity).ID,
					base.ID,
					base.Category,
					ex
				});
			}
		}

		public List<CashShopCategoryListElement> Categories { get; private set; }

		public Dictionary<int, CashShopCategoryListElement> BeautyShopCategories { get; private set; }

		private void Connection_CategoryArrayChanged(object sender, EventArgs e)
		{
			if (ServiceCore.FeatureMatrix.IsEnable("BeautyShopItemSyncNISMS"))
			{
				string @string = ServiceCore.FeatureMatrix.GetString("BeautyShopCategoryByNISMS");
				string[] array = @string.Split(new char[]
				{
					';'
				});
				if (array.Count<string>() == 2)
				{
					this.BeautyShopCategoryName = array[0];
					this.BeautyShopIgnoreCategoryName = array[1];
				}
			}
			this.Categories = new List<CashShopCategoryListElement>();
			this.BeautyShopCategories = new Dictionary<int, CashShopCategoryListElement>();
			foreach (Category category in this.Connection.CategoryArray)
			{
				Log<CashShopService>.Logger.InfoFormat("category - {0} : {1} ", category.CategoryNo, category.CategoryName);
				this.Categories.Add(new CashShopCategoryListElement(category));
				if (category.CategoryName.Equals(this.BeautyShopCategoryName))
				{
					this.BeautyShopCategoryNo = category.CategoryNo;
					this.BeautyShopCategories.Add(category.CategoryNo, new CashShopCategoryListElement(category));
				}
				if (this.BeautyShopCategories.ContainsKey(category.ParentCategoryNo))
				{
					if (category.CategoryName.Equals(this.BeautyShopIgnoreCategoryName))
					{
						this.BeautyShopIgnoreCategoryNo = category.CategoryNo;
					}
					else
					{
						this.BeautyShopCategories.Add(category.CategoryNo, new CashShopCategoryListElement(category));
					}
				}
			}
			Log<CashShopService>.Logger.Info("Categories Changed!");
			foreach (int serviceID in base.LookUp.FindIndex("FrontendServiceCore.FrontendService"))
			{
				BroadcastPacket op = BroadcastPacket.Create<ChangedCashShopMessage>(new ChangedCashShopMessage());
				base.RequestOperation(serviceID, op);
			}
		}

		public List<CashShopProductListElement> Products { get; private set; }

		public List<CashShopProductListElement> BeautyShopProducts { get; private set; }

		public IDictionary<int, CashShopProductListElement> ProductByProductID { get; set; }

		public IDictionary<string, CashShopProductListElement> ProductByItemClass { get; set; }

		public IDictionary<CashShopItemKey, CashShopProductListElement> ProductByCashShopItemKey { get; set; }

		private void Connection_ProductArrayChanged(object sender, EventArgs e)
		{
			this.Products = new List<CashShopProductListElement>();
			this.BeautyShopProducts = new List<CashShopProductListElement>();
			this.ProductByProductID = new Dictionary<int, CashShopProductListElement>(this.Connection.ProductArray.Count);
			this.ProductByItemClass = new Dictionary<string, CashShopProductListElement>(this.Connection.ProductArray.Count);
			this.ProductByCashShopItemKey = new Dictionary<CashShopItemKey, CashShopProductListElement>(this.Connection.ProductArray.Count);
			try
			{
				foreach (Product product in this.Connection.ProductArray)
				{
					Log<CashShopService>.Logger.InfoFormat("product - {0} : {1} ", product.ProductNo, product.ProductID);
					CashShopProductListElement cashShopProductListElement = new CashShopProductListElement(product);
					this.Products.Add(cashShopProductListElement);
					if (this.ProductByProductID.ContainsKey(cashShopProductListElement.ProductNo))
					{
						Log<CashShopService>.Logger.ErrorFormat("Duplicate ProductNo[{0}]", cashShopProductListElement.ProductNo);
					}
					else
					{
						this.ProductByProductID.Add(cashShopProductListElement.ProductNo, cashShopProductListElement);
					}
					if (!this.ProductByItemClass.ContainsKey(cashShopProductListElement.ProductID))
					{
						this.ProductByItemClass.Add(cashShopProductListElement.ProductID, cashShopProductListElement);
					}
					CashShopItemKey key = new CashShopItemKey(cashShopProductListElement.ProductID, cashShopProductListElement.SalePrice, (int)cashShopProductListElement.ProductExpire);
					if (this.ProductByCashShopItemKey.ContainsKey(key))
					{
						Log<CashShopService>.Logger.InfoFormat("Duplicate CashShopItemKey[{0}/{1}/{2}]", cashShopProductListElement.ProductID, cashShopProductListElement.SalePrice, cashShopProductListElement.ProductExpire);
					}
					else
					{
						this.ProductByCashShopItemKey.Add(key, cashShopProductListElement);
					}
					if (this.BeautyShopCategories.ContainsKey(cashShopProductListElement.CategoryNo))
					{
						this.BeautyShopProducts.Add(cashShopProductListElement);
					}
				}
				Log<CashShopService>.Logger.Info("Product Changed!");
				foreach (int serviceID in base.LookUp.FindIndex("FrontendServiceCore.FrontendService"))
				{
					BroadcastPacket op = BroadcastPacket.Create<ChangedCashShopMessage>(new ChangedCashShopMessage());
					base.RequestOperation(serviceID, op);
				}
			}
			catch (Exception ex)
			{
				Log<CashShopService>.Logger.Error(ex);
			}
		}

		public string MakeOrderID()
		{
			if (this.orderIDHigh < DateTime.UtcNow)
			{
				this.orderIDHigh = base.MakeEntityID();
				this.orderIDLow = 0;
			}
			this.orderIDLow = (this.orderIDLow + 1) % 1000;
			StringBuilder stringBuilder = new StringBuilder();
			if (ServiceCore.FeatureMatrix.IsEnable("zhTW"))
			{
				stringBuilder.Append(this.orderIDHigh.ToString("yyyyMMddHHmmssfffffff"));
				stringBuilder.Append(this.orderIDLow.ToString("d3"));
			}
			else
			{
				stringBuilder.Append(this.orderIDHigh.ToString("yyMMddHHmmssfffffff"));
				stringBuilder.Append(this.orderIDLow.ToString("d3"));
			}
			return stringBuilder.ToString();
		}

		internal static CashShopService Instance { get; private set; }

		public static Service StartService(string ip, string portstr)
		{
			CashShopService.Instance = new CashShopService();
			ServiceInvoker.StartService(ip, portstr, CashShopService.Instance);
			CashShopService.StartReporting(CashShopService.Instance);
			return CashShopService.Instance;
		}

		private static void StartReporting(CashShopService serv)
		{
			if (!ServiceCore.FeatureMatrix.IsEnable("ServiceReporter"))
			{
				return;
			}
			int num = ServiceReporterSettings.Get("CashShopService.Interval", 60);
			ServiceReporter.Instance.Initialize("CashShopService");
			ServiceReporter.Instance.AddGathering("Stat", new ServiceReporter.GatheringDelegate<int>(serv.OnGatheringStat));
			ServiceReporter.Instance.Start(num * 1000);
		}

		private void OnGatheringStat(Dictionary<string, int> dict)
		{
			dict.InsertOrIncrease("Entity", (int)this.GetEntityCount());
			dict.InsertOrIncrease("Queue", (int)this.GetQueueLength());
		}

		internal bool IsCashShopStopped;

		internal bool IsCashShopServiceUseable;

		public List<CustomizeItemInfo> CustomizeCouponInfoList = new List<CustomizeItemInfo>();

		public List<BeautyShopCouponListElement> BeautyShopCouponList = new List<BeautyShopCouponListElement>();

		public IDictionary<string, ItemClassInfo> ItemClassInfoDic = new Dictionary<string, ItemClassInfo>();

		private DateTime orderIDHigh;

		private int orderIDLow;
	}
}
