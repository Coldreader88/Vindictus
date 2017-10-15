using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.EntityGraph;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	internal sealed class RequestShutDownEntity : Operation
	{
		public RequestShutDownEntity()
		{
			this.target = new EntityGraphIdentifier
			{
				category = "Dummy",
				entityID = 0L
			};
		}

		public RequestShutDownEntity(EntityGraphIdentifier t)
		{
			this.target = t;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RequestShutDownEntity.Request(this);
		}

		public EntityGraphIdentifier target;

		private class Request : OperationProcessor
		{
			public Request(RequestShutDownEntity op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				yield break;
			}
		}
	}
}
