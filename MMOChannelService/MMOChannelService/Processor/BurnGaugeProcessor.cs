using System;
using System.Collections.Generic;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Entity;

namespace MMOChannelService.Processor
{
	internal class BurnGaugeProcessor : EntityProcessor<BurnGauge, ChannelEntity>
	{
		public BurnGaugeProcessor(MMOChannelService service, BurnGauge op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			this.service.BurnGaugeBroadcast();
			yield break;
		}

		private MMOChannelService service;
	}
}
