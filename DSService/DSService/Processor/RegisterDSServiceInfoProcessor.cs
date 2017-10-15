using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class RegisterDSServiceInfoProcessor : OperationProcessor<RegisterDSServiceInfo>
	{
		public RegisterDSServiceInfoProcessor(DSService service, RegisterDSServiceInfo op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.DSWaitingSystem != null)
			{
				base.Finished = true;
				this.service.DSEntityMakerSystem.AddServiceInfo(base.Operation.ServiceID, base.Operation.CoreCount);
				yield return new OkMessage();
			}
			else
			{
				base.Finished = true;
				yield return new FailMessage("[RegisterDSServiceInfoProcessor] service.DSWaitingSystem");
			}
			yield break;
		}

		private DSService service;
	}
}
