using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class QueryDSServiceInfoProcessor : OperationProcessor<QueryDSServiceInfo>
	{
		public QueryDSServiceInfoProcessor(DSService service, QueryDSServiceInfo op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.DSEntityMakerSystem != null)
			{
				yield return this.service.GetDSState();
			}
			else
			{
				yield return null;
			}
			base.Finished = true;
			yield break;
		}

		private DSService service;
	}
}
