using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class RegisterDSEntityProcessor : OperationProcessor<RegisterDSEntity>
	{
		public RegisterDSEntityProcessor(DSService service, RegisterDSEntity op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.DSWaitingSystem != null)
			{
				base.Finished = true;
				int idStart;
				int processCount;
				DSType dsType = this.service.DSWaitingSystem.DSStorage.RegisterDSService(base.Operation.ServiceID, base.Operation.CoreCount, base.Operation.GiantRaidMachine, out idStart, out processCount);
				yield return idStart;
				yield return processCount;
				yield return dsType;
			}
			else
			{
				base.Finished = true;
				yield return new FailMessage("[RegisterDSEntityProcessor] service.DSWaitingSystem");
			}
			yield break;
		}

		private DSService service;
	}
}
