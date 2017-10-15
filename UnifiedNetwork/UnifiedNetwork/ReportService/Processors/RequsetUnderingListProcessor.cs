using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.EntityGraph;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.ReportService.Operations;

namespace UnifiedNetwork.ReportService.Processors
{
	internal class RequsetUnderingListProcessor : OperationProcessor<RequestUnderingList>
	{
		public RequsetUnderingListProcessor(Service service, RequestUnderingList op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Operation.target.isCategoric)
			{
				if (!(base.Operation.target.category == this.service.Category))
				{
					yield return null;
				}
				else
				{
					EntityGraphIdentifier[] result = this.service.ReportConnectedNodeList(base.Operation.target);
					yield return new ReportUnderingListMessage(result);
				}
			}
			if (base.Operation.target.isNumeric)
			{
				if (base.Operation.target.serviceID != this.service.ID)
				{
					yield return null;
				}
				else if (base.Operation.isIncluded)
				{
					EntityGraphIdentifier[] result2 = this.service.ReportConnectedNodeList(base.Operation.includedEID);
					yield return new ReportUnderingListMessage(result2);
				}
				else
				{
					EntityGraphIdentifier[] result3 = this.service.ReportConnectedNodeList(base.Operation.target);
					yield return new ReportUnderingListMessage(result3);
				}
			}
			yield break;
		}

		private Service service;
	}
}
