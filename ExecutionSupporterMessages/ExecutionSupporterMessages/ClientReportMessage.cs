using System;
using System.Collections.Generic;

namespace ExecutionSupporterMessages
{
	[Serializable]
	public sealed class ClientReportMessage
	{
		public int CoreCount { get; set; }

		public float cpuUsage { get; set; }

		public float memUsage { get; set; }

		public float memLimit { get; set; }

		public List<ProcessReport> ProcessList { get; set; }

		public string LatestServerFile { get; set; }

		public DateTime LatestServerFileTime { get; set; }

		public string LatestClientFile { get; set; }

		public DateTime LatestClientFileTime { get; set; }
	}
}
