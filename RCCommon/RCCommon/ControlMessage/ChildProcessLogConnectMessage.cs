using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("7058553C-1A82-4544-A9FE-AC885020A058")]
	[Serializable]
	public sealed class ChildProcessLogConnectMessage
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public int ProcessID { get; private set; }

		public ChildProcessLogConnectMessage(int id, string processName, int pid)
		{
			this.ClientID = id;
			this.ProcessName = processName;
			this.ProcessID = pid;
		}
	}
}
