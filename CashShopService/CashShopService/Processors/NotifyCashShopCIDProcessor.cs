using System;
using System.Collections.Generic;
using ServiceCore.CashShopServiceOperation;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class NotifyCashShopCIDProcessor : EntityProcessor<NotifyCashShopCID, CashShopPeer>
	{
		public NotifyCashShopCIDProcessor(CashShopService service, NotifyCashShopCID op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			base.Entity.CID = base.Operation.CID;
			base.Entity.Ready();
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}
	}
}
