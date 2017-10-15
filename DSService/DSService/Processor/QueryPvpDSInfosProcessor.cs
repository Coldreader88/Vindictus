using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class QueryPvpDSInfosProcessor : OperationProcessor<QueryPvpDSInfos>
	{
		public QueryPvpDSInfosProcessor(DSService service, QueryPvpDSInfos op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.DSWaitingSystem != null)
			{
				base.Finished = true;
				yield return this.service.DSWaitingSystem.DSStorage.PvpDSMap;
			}
			else
			{
				base.Finished = true;
				yield return new FailMessage("[QueryPvpDSInfosProcessor] service.DSWaitingSystem");
			}
			yield break;
		}

		private DSService service;
	}
}
