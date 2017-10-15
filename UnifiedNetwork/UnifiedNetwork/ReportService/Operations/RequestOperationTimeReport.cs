using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.EntityGraph;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	internal sealed class RequestOperationTimeReport : Operation
	{
		public ReportOperationTimeReportMessage Message
		{
			get
			{
				return this.result;
			}
		}

		public RequestOperationTimeReport()
		{
			this.targetEntity = new EntityGraphIdentifier
			{
				category = "Dummy",
				entityID = 0L
			};
			this.targetConnection = new EntityGraphIdentifier
			{
				category = "Dummy",
				entityID = 0L
			};
		}

		public RequestOperationTimeReport(EntityGraphIdentifier t, EntityGraphIdentifier c)
		{
			this.targetEntity = t;
			this.targetConnection = c;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RequestOperationTimeReport.Request(this);
		}

		public EntityGraphIdentifier targetEntity;

		public EntityGraphIdentifier targetConnection;

		[NonSerialized]
		private ReportOperationTimeReportMessage result;

		private class Request : OperationProcessor<RequestOperationTimeReport>
		{
			public Request(RequestOperationTimeReport op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.result = (base.Feedback as ReportOperationTimeReportMessage);
				yield break;
			}
		}
	}
}
