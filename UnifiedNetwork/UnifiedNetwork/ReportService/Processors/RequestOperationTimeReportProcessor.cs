using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.ReportService.Operations;

namespace UnifiedNetwork.ReportService.Processors
{
	internal class RequestOperationTimeReportProcessor : OperationProcessor<RequestOperationTimeReport>
	{
		public RequestOperationTimeReportProcessor(Service service, RequestOperationTimeReport op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Operation.targetEntity.isCategoric || !base.Operation.targetEntity.serviceID.Equals(this.service.ID))
			{
				yield return null;
			}
			yield return new ReportOperationTimeReportMessage(this.service.ReportOperationTimeReport(base.Operation.targetEntity, base.Operation.targetConnection));
			yield break;
		}

		private Service service;
	}
}
