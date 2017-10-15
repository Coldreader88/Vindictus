using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.ExecutionServiceOperations
{
	[Serializable]
	public sealed class QueryService : Operation
	{
		public IEnumerable<string> ServiceClasses
		{
			get
			{
				return this.serviceClasses;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryService.Request(this);
		}

		[NonSerialized]
		private IEnumerable<string> serviceClasses;

		private class Request : OperationProcessor<QueryService>
		{
			public Request(QueryService op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.serviceClasses = (base.Feedback as IList<string>);
				if (base.Operation.serviceClasses == null)
				{
					Log<QueryService>.Logger.ErrorFormat("쿼리했는데 이상한게 날아왔습니다.", new object[0]);
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
