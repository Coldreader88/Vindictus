using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("F26859C7-ACF1-469d-AA10-D74831FC645A")]
	[Serializable]
	public sealed class ChildProcessLogListReplyMessage
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public List<int> Processes { get; private set; }

		public ChildProcessLogListReplyMessage(int clientID, string processName, List<int> processes)
		{
			this.ClientID = clientID;
			this.ProcessName = processName;
			this.Processes = processes;
		}
	}
}
