using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationService.Operations;

namespace UnifiedNetwork.LocationService.Processors
{
	internal class QueryProcessor : OperationProcessor<Query>
	{
		public QueryProcessor(LocationService service, Query op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			ICollection<ServiceInfo> result = this.service.Query(base.Operation.GameCode, base.Operation.ServerCode, base.Operation.FullName);
			base.Finished = true;
			yield return result;
			yield break;
		}

		private LocationService service;
	}
}
