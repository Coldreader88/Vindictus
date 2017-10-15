using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class RequestMakeDSEntityProcessor : OperationProcessor<RequestMakeDSEntity>
	{
		public RequestMakeDSEntityProcessor(DSService service, RequestMakeDSEntity op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Operation.DSType == DSType.Pvp)
			{
				this.service.DSEntityMakerSystem.Enqueue(base.Operation.ID, base.Operation.DSType, base.Operation.PVPServiceID);
			}
			else
			{
				this.service.DSEntityMakerSystem.Enqueue(base.Operation.ID, base.Operation.DSType);
			}
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}

		private DSService service;
	}
}
