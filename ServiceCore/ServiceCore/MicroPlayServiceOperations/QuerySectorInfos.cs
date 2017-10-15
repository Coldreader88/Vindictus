using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class QuerySectorInfos : Operation
	{
		public QuestSectorInfos SectorInfos { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new QuerySectorInfos.Request(this);
		}

		private class Request : OperationProcessor<QuerySectorInfos>
		{
			public Request(QuerySectorInfos op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is QuestSectorInfos)
				{
					base.Operation.SectorInfos = (base.Feedback as QuestSectorInfos);
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
