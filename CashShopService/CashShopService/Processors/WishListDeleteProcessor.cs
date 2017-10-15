using System;
using System.Collections.Generic;
using CashShopService.WishListSystem;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class WishListDeleteProcessor : EntityProcessor<WishListDelete, CashShopPeer>
	{
		public WishListDeleteProcessor(CashShopService service, WishListDelete op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			foreach (int productno in base.Operation.List_ProductNo)
			{
				WishListResult rt = this.service.GetFunction<WishListManager>().WishListDelete(base.Operation.CID, productno);
				if (rt != WishListResult.SUCCESS)
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
