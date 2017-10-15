using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class QueryHousingList : Operation
	{
		public List<long> HousingList
		{
			get
			{
				return this.housingList;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryHousingList.Request(this);
		}

		[NonSerialized]
		private List<long> housingList;

		private class Request : OperationProcessor<QueryHousingList>
		{
			public Request(QueryHousingList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.housingList = (base.Feedback as List<long>);
				if (base.Operation.housingList == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
