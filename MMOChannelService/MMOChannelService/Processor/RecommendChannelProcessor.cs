using System;
using System.Collections.Generic;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Cooperation;

namespace MMOChannelService.Processor
{
	internal class RecommendChannelProcessor : OperationProcessor<RecommendChannel>
	{
		public RecommendChannelProcessor(MMOChannelService service, RecommendChannel op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			int serviceID = this.service.LoadManager.GetRecommendServiceID(this.service.CurrentLoad);
			if (serviceID == -1)
			{
				base.Finished = true;
				yield return this.service.ID;
				yield return this.service.RecommendChannel();
			}
			else
			{
				base.Finished = true;
				yield return serviceID;
				yield return -1L;
			}
			yield break;
		}

		private MMOChannelService service;
	}
}
