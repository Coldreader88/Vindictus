using System;
using System.Collections.Generic;
using ServiceCore.ExecutionServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ExecutionServiceCore
{
	internal class QueryServiceProcessor : OperationProcessor<QueryService>
	{
		public QueryServiceProcessor(ExecutionService service, QueryService operation) : base(operation)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			yield return new List<string>(this.service.Services);
			yield break;
		}

		private ExecutionService service;
	}
}
