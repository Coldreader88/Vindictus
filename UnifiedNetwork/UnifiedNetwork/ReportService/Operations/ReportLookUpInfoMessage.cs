using System;
using System.Collections.Generic;
using System.Net;
using Utility;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class ReportLookUpInfoMessage
	{
		public ReportLookUpInfoMessage()
		{
			this.serverlist = new ServerPair[0];
			this.EntityCount = 0;
		}

		public ReportLookUpInfoMessage(MultiDictionary<string, int> s)
		{
			List<ServerPair> list = new List<ServerPair>();
			foreach (KeyValuePair<string, int> keyValuePair in s)
			{
				list.Add(new ServerPair
				{
					category = keyValuePair.Key,
					code = keyValuePair.Value
				});
			}
			this.serverlist = list.ToArray();
		}

		public ReportLookUpInfoMessage(Dictionary<int, KeyValuePair<string, IPEndPoint>> dic, int count)
		{
			this.EntityCount = count;
			List<ServerPair> list = new List<ServerPair>();
			foreach (KeyValuePair<int, KeyValuePair<string, IPEndPoint>> keyValuePair in dic)
			{
				list.Add(new ServerPair
				{
					code = keyValuePair.Key,
					category = keyValuePair.Value.Key,
					location = keyValuePair.Value.Value.ToString()
				});
			}
			this.serverlist = list.ToArray();
		}

		public override string ToString()
		{
			return string.Format("ReportServiceListMessage [ ]", new object[0]);
		}

		public ServerPair[] serverlist;

		public int EntityCount;
	}
}
