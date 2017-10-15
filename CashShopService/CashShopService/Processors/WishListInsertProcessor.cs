using System;
using System.Collections.Generic;
using CashShopService.WishListSystem;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class WishListInsertProcessor : EntityProcessor<WishListInsert, CashShopPeer>
	{
		public WishListInsertProcessor(CashShopService service, WishListInsert op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			foreach (int productno in base.Operation.List_Wish)
			{
				CashShopProductListElement product;
				if (!this.service.ProductByProductID.TryGetValue(productno, out product))
				{
					base.Result = false;
					base.Finished = true;
					yield return WishListResult.NOITEM;
					yield break;
				}
				WishListResult rt = this.service.GetFunction<WishListManager>().WishListInsert(base.Operation.CID, productno, product.ProductID);
				if (rt == WishListResult.OVERCOUNT)
				{
					base.Result = true;
					base.Finished = true;
					yield return rt;
					yield break;
				}
			}
			base.Result = true;
			base.Finished = true;
			yield return WishListResult.SUCCESS;
			yield break;
		}

		private CashShopService service;
	}
}
