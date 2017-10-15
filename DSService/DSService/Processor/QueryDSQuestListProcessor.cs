using System;
using System.Collections.Generic;
using DSService.WaitingQueue;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class QueryDSQuestListProcessor : OperationProcessor<QueryDSQuestList>
	{
		public QueryDSQuestListProcessor(DSService service, QueryDSQuestList op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			DSWaitingSystem ws = DSService.Instance.DSWaitingSystem;
			if (ws != null)
			{
				base.Finished = true;
				List<string> list = new List<string>();
				foreach (KeyValuePair<string, DSWaitingQueue> keyValuePair in ws.QueueDict)
				{
					if (keyValuePair.Value.IsGiantRaid)
					{
						list.Add(keyValuePair.Key);
					}
				}
				yield return list;
			}
			else
			{
				base.Finished = true;
				yield return new FailMessage("[QueryDSQuestListProcessor] ws");
			}
			yield break;
		}

		private DSService service;
	}
}
