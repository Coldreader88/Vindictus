using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.EntityGraph;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	internal sealed class EnableOperationTimeReport : Operation
	{
		public EnableOperationTimeReport()
		{
			this.target = new EntityGraphIdentifier
			{
				category = "Dummy",
				entityID = 0L
			};
		}

		public EnableOperationTimeReport(EntityGraphIdentifier t, bool e)
		{
			this.target = t;
			this.enable = e;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new EnableOperationTimeReport.Request(this);
		}

		public EntityGraphIdentifier target;

		public bool enable;

		private class Request : OperationProcessor
		{
			public Request(EnableOperationTimeReport op) : base(op)
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
