using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;

namespace DSService.Processor
{
	internal class QueryDSInfoProcessor : OperationProcessor
	{
		public QueryDSInfoProcessor(DSService service, QueryDSInfo op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			DSProcessInfo statics = this.service.GetDSServiceState();
			yield return statics.IP;
			yield return statics.WaitingQueueInfo.Count;
			foreach (KeyValuePair<string, Dictionary<string, int>> queue in statics.WaitingQueueInfo)
			{
				KeyValuePair<string, Dictionary<string, int>> keyValuePair = queue;
				yield return keyValuePair.Key;
				KeyValuePair<string, Dictionary<string, int>> keyValuePair2 = queue;
				yield return keyValuePair2.Value.Count;
				KeyValuePair<string, Dictionary<string, int>> keyValuePair3 = queue;
				foreach (KeyValuePair<string, int> info in keyValuePair3.Value)
				{
					KeyValuePair<string, int> keyValuePair4 = info;
					yield return keyValuePair4.Key;
					KeyValuePair<string, int> keyValuePair5 = info;
					yield return keyValuePair5.Value;
				}
			}
			yield return statics.ProcessList.Count;
			foreach (Dictionary<string, string> proc in statics.ProcessList)
			{
				yield return proc.Count;
				foreach (KeyValuePair<string, string> prop in proc)
				{
					KeyValuePair<string, string> keyValuePair6 = prop;
					yield return keyValuePair6.Key;
					KeyValuePair<string, string> keyValuePair7 = prop;
					yield return keyValuePair7.Value;
				}
			}
			yield break;
		}

		private DSService service;
	}
}
