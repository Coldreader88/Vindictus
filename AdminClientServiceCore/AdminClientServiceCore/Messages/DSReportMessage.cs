using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("51149312-1124-4e04-8223-645C14692019")]
	[Serializable]
	public sealed class DSReportMessage
	{
		public Dictionary<string, Dictionary<string, int>> CountInfo { get; set; }

		public Dictionary<string, List<Dictionary<string, string>>> ProcessList { get; set; }
	}
}
