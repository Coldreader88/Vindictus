using System;
using System.Collections.Generic;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Entity;

namespace MMOChannelService.Processor
{
	internal class BurnJackpotNotifyProcessor : EntityProcessor<BurnJackpotNotify, ChannelEntity>
	{
		public BurnJackpotNotifyProcessor(MMOChannelService service, BurnJackpotNotify op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			this.service.BurnJackpotBroadcast(base.Operation.CID);
			yield break;
		}

		private MMOChannelService service;
	}
}
