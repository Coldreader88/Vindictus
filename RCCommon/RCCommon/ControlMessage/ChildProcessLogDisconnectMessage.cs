using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("2A67E722-615A-4c67-9326-6D2F3DC76BCC")]
	[Serializable]
	public sealed class ChildProcessLogDisconnectMessage
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public int ProcessID { get; private set; }

		public ChildProcessLogDisconnectMessage(int id, string processName, int pid)
		{
			this.ClientID = id;
			this.ProcessName = processName;
			this.ProcessID = pid;
		}
	}
}
