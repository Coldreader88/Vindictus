using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.ReportService.Operations;

namespace UnifiedNetwork.ReportService.Processors
{
	internal class RequestShutDownEntityProcessor : OperationProcessor<RequestShutDownEntity>
	{
		public RequestShutDownEntityProcessor(Service service, RequestShutDownEntity op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Operation.target.isNumeric && base.Operation.target.serviceID == this.service.ID)
			{
				this.service.ShutDownEntity(base.Operation.target);
			}
			yield return null;
			yield break;
		}

		private Service service;
	}
}
