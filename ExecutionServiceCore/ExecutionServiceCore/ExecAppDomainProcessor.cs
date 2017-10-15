using System;
using System.Collections.Generic;
using ServiceCore.ExecutionServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ExecutionServiceCore
{
	internal class ExecAppDomainProcessor : OperationProcessor<ExecAppDomain>
	{
		public ExecAppDomainProcessor(ExecutionService service, ExecAppDomain operation) : base(operation)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Operation == null)
			{
				base.Result = false;
			}
			else
			{
				base.Finished = true;
				string message = null;
				bool isOk = false;
				if (base.Operation.AppDomainName == "")
				{
					isOk = true;
				}
				else if (base.Operation.ExecOption == 1)
				{
					isOk = this.service.StartAppDomain(base.Operation.AppDomainName, out message);
				}
				else
				{
					isOk = this.service.StopAppDomain(base.Operation.AppDomainName);
				}
				if (isOk)
				{
					yield return new List<string>(this.service.DomainList);
				}
			}
			yield break;
		}

		private ExecutionService service;
	}
}
