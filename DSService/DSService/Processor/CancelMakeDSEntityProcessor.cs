using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class CancelMakeDSEntityProcessor : OperationProcessor<CancelMakeDSEntity>
	{
		public CancelMakeDSEntityProcessor(DSService service, CancelMakeDSEntity op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			this.service.DSEntityMakerSystem.Dequeue(base.Operation.ID, base.Operation.DSType);
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}

		private DSService service;
	}
}
