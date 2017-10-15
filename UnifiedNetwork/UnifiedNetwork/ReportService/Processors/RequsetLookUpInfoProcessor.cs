using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.ReportService.Operations;

namespace UnifiedNetwork.ReportService.Processors
{
	internal class RequsetLookUpInfoProcessor : OperationProcessor<RequestLookUpInfo>
	{
		public RequsetLookUpInfoProcessor(Service service, RequestLookUpInfo op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Operation.target.isCategoric)
			{
				if (!base.Operation.target.category.Equals(this.service.Category))
				{
					yield return null;
				}
				else
				{
					yield return new ReportLookUpInfoMessage(this.service.ReportExtendedLookUpInfo(), this.service.ReportUnderingCounts());
				}
			}
			if (base.Operation.target.isNumeric)
			{
				if (!base.Operation.target.serviceID.Equals(this.service.ID))
				{
					yield return null;
				}
				else
				{
					yield return new ReportLookUpInfoMessage(this.service.ReportExtendedLookUpInfo(), this.service.ReportUnderingCounts());
				}
			}
			yield break;
		}

		private Service service;
	}
}
