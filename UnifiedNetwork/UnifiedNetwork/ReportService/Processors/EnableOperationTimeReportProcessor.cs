using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.ReportService.Operations;

namespace UnifiedNetwork.ReportService.Processors
{
	internal class EnableOperationTimeReportProcessor : OperationProcessor<EnableOperationTimeReport>
	{
		public EnableOperationTimeReportProcessor(Service service, EnableOperationTimeReport op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Operation.target.isCategoric)
			{
				yield return null;
			}
			if (base.Operation.target.isNumeric)
			{
				if (!base.Operation.target.serviceID.Equals(this.service.ID))
				{
					yield return null;
				}
				else
				{
					if (base.Operation.enable)
					{
						OperationProxy.EnableTimeReport();
					}
					else
					{
						this.service.ClearOperationTimeReport();
						OperationProxy.DisableTimeReport();
					}
					yield return null;
				}
			}
			yield break;
		}

		private Service service;
	}
}
