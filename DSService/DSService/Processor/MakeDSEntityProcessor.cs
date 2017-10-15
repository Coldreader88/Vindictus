using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class MakeDSEntityProcessor : OperationProcessor<MakeDSEntity>
	{
		public MakeDSEntityProcessor(DSService service, MakeDSEntity op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			this.service.MakeDSEntity(base.Operation.DSID);
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}

		private DSService service;
	}
}
