using System;
using System.Collections.Generic;
using CashShopService.WishListSystem;
using ServiceCore.CashShopServiceOperation;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class WishListSelectProcessor : EntityProcessor<WishListSelect, CashShopPeer>
	{
		public WishListSelectProcessor(CashShopService service, WishListSelect op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			List<WishItemInfo> retlist;
			WishListResult rt = this.service.GetFunction<WishListManager>().WishListGet(base.Operation.CID, out retlist);
			base.Result = true;
			base.Finished = true;
			yield return rt;
			yield return retlist;
			yield break;
		}

		private CashShopService service;
	}
}
