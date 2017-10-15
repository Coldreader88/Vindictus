using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("9C6A3CE2-426C-4ada-B13D-8E5F01F1A354")]
	[Serializable]
	public sealed class PerformanceUpdateMessage
	{
		public IEnumerable<KeyValuePair<string, RCProcess.SPerformance>> Performance
		{
			get
			{
				return this.performance;
			}
		}

		public PerformanceUpdateMessage()
		{
			this.performance = new Dictionary<string, RCProcess.SPerformance>();
		}

		public void Add(string name, RCProcess.SPerformance data)
		{
			this.performance.Add(name, data);
		}

		private Dictionary<string, RCProcess.SPerformance> performance;
	}
}
