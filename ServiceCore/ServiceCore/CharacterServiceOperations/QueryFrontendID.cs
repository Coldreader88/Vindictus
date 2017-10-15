using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryFrontendID : Operation
	{
		public long FrontendID
		{
			get
			{
				return this.fid;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryFrontendID.Request(this);
		}

		[NonSerialized]
		private long fid;

		private class Request : OperationProcessor<QueryFrontendID>
		{
			public Request(QueryFrontendID op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is long)
				{
					base.Operation.fid = (long)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
