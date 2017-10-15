using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.EntityGraph;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	internal sealed class RequestUnderingList : Operation
	{
		public bool isIncluded
		{
			get
			{
				return this.includedEID != -1L;
			}
		}

		public ReportUnderingListMessage Message
		{
			get
			{
				return this.result;
			}
		}

		public RequestUnderingList()
		{
			this.target = new EntityGraphIdentifier
			{
				category = "Dummy",
				entityID = 0L
			};
		}

		public RequestUnderingList(EntityGraphIdentifier t)
		{
			this.target = t;
		}

		public RequestUnderingList(EntityGraphIdentifier t, long ieid)
		{
			this.target = t;
			this.includedEID = ieid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RequestUnderingList.Request(this);
		}

		public EntityGraphIdentifier target;

		public long includedEID = -1L;

		[NonSerialized]
		private ReportUnderingListMessage result;

		private class Request : OperationProcessor<RequestUnderingList>
		{
			public Request(RequestUnderingList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.result = (base.Feedback as ReportUnderingListMessage);
				yield break;
			}
		}
	}
}
