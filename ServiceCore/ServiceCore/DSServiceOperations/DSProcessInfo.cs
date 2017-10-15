using System;
using System.Collections.Generic;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class DSProcessInfo
	{
		public string IP { get; set; }

		public DSProcessInfo(string ip)
		{
			this.ProcessList = new List<Dictionary<string, string>>();
			this.WaitingQueueInfo = new Dictionary<string, Dictionary<string, int>>();
			this.IP = ip;
		}

		public void AddProcessInfo(Dictionary<string, string> info)
		{
			this.ProcessList.Add(info);
		}

		public void SetQueueInfo(Dictionary<string, Dictionary<string, int>> info)
		{
			this.WaitingQueueInfo = info;
		}

		public Dictionary<string, Dictionary<string, int>> WaitingQueueInfo;

		public List<Dictionary<string, string>> ProcessList;
	}
}
