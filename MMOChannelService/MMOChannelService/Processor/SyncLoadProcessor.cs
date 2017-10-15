using System;
using System.Collections.Generic;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Cooperation;

namespace MMOChannelService.Processor
{
	internal class SyncLoadProcessor : OperationProcessor<SyncLoad>
	{
		public SyncLoadProcessor(MMOChannelService service, SyncLoad op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.ID != base.Operation.ServiceID)
			{
				this.service.LoadManager.UpdateLoad(base.Operation.ServiceID, base.Operation.Load);
			}
			yield break;
		}

		private MMOChannelService service;
	}
}
