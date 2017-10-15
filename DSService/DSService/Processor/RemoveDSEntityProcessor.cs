using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class RemoveDSEntityProcessor : OperationProcessor<RemoveDSEntity>
	{
		public RemoveDSEntityProcessor(DSService service, RemoveDSEntity op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			this.service.RemoveDSEntity(base.Operation.DSID);
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}

		private DSService service;
	}
}
