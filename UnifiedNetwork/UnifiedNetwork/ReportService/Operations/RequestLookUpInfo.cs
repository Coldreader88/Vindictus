using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.EntityGraph;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	internal sealed class RequestLookUpInfo : Operation
	{
		public new ReportLookUpInfoMessage Result
		{
			get
			{
				return this.result;
			}
		}

		public RequestLookUpInfo()
		{
			this.target = new EntityGraphIdentifier();
		}

		public RequestLookUpInfo(EntityGraphIdentifier t)
		{
			this.target = t;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RequestLookUpInfo.Request(this);
		}

		public EntityGraphIdentifier target;

		[NonSerialized]
		private ReportLookUpInfoMessage result;

		private class Request : OperationProcessor<RequestLookUpInfo>
		{
			public Request(RequestLookUpInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.result = (base.Feedback as ReportLookUpInfoMessage);
				yield break;
			}
		}
	}
}
