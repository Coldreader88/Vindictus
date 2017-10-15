using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Entity;
using Utility;

namespace CashShopService.WishListSystem
{
	public class WishListManager
	{
		public WishListManager(Service service)
		{
			this.Service = service;
		}

		public WishListResult WishListGet(long cid, out List<WishItemInfo> retlist)
		{
			retlist = new List<WishItemInfo>();
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					foreach (WishListGetResult wishListGetResult in cashShopProcessDataContext.WishListGet(new long?(cid)))
					{
						retlist.Add(new WishItemInfo(wishListGetResult.CID, wishListGetResult.ProductNo, wishListGetResult.ProductName));
					}
					if (retlist.Count == 0)
					{
						return WishListResult.NOITEM;
					}
				}
			}
			catch (Exception ex)
			{
				Log<WishListManager>.Logger.ErrorFormat("Exception Occured while executing WishListGet sp - %s", ex);
				return WishListResult.FAILUNKNOWN;
			}
			return WishListResult.SUCCESS;
		}

		public WishListResult WishListInsert(long cid, int productno, string productname)
		{
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					if (cashShopProcessDataContext.WishListInsert(new long?(cid), new int?(productno), productname) == 0)
					{
						Log<WishListManager>.Logger.ErrorFormat("error Occured while executing WishListInsert sp - over count [ %ld ]", cid);
						return WishListResult.OVERCOUNT;
					}
				}
			}
			catch (Exception ex)
			{
				Log<WishListManager>.Logger.ErrorFormat("Exception Occured while executing WishListInsert sp - %s", ex);
				return WishListResult.FAILUNKNOWN;
			}
			return WishListResult.SUCCESS;
		}

		public WishListResult WishListDelete(long cid, int productno)
		{
			try
			{
				using (CashShopProcessDataContext cashShopProcessDataContext = new CashShopProcessDataContext())
				{
					cashShopProcessDataContext.WishListDelete(new long?(cid), new int?(productno));
				}
			}
			catch (Exception ex)
			{
				Log<WishListManager>.Logger.ErrorFormat("Exception Occured while executing WishListDel sp - %s", ex);
				return WishListResult.FAILUNKNOWN;
			}
			return WishListResult.SUCCESS;
		}

		public Service Service;
	}
}
