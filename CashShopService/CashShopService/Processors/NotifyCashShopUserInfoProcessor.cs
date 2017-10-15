using System;
using System.Collections.Generic;
using System.Net;
using ServiceCore.CashShopServiceOperation;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace CashShopService.Processors
{
	public class NotifyCashShopUserInfoProcessor : EntityProcessor<NotifyCashShopUserInfo, CashShopPeer>
	{
		public NotifyCashShopUserInfoProcessor(CashShopService service, NotifyCashShopUserInfo op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			base.Entity.NexonID = base.Operation.Name;
			base.Entity.NexonSN = base.Operation.NexonSN;
			base.Entity.FID = base.Operation.FID;
			base.Entity.UserAge = base.Operation.Age;
			if (base.Operation.CID != -1L)
			{
				base.Entity.CID = base.Operation.CID;
			}
			base.Entity.RemoteIPAddress = base.Operation.RemoteIP;
			if (base.Entity.RemoteIPAddress == null)
			{
				base.Entity.RemoteIPAddress = IPAddress.None;
			}
			base.Entity.Ready();
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}
	}
}
