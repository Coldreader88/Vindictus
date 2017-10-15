using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("E25A5F20-1630-453f-83FE-64FA878084C6")]
	[Serializable]
	public sealed class ChildProcessLogMessage
	{
		public int ClientID { get; set; }

		public string ProcessName { get; private set; }

		public int ProcessID { get; private set; }

		public RCProcess.ChildProcessLog Log { get; private set; }

		public ChildProcessLogMessage(string processName, int pid, RCProcess.ChildProcessLog log)
		{
			this.ProcessName = processName;
			this.ProcessID = pid;
			this.Log = log;
		}
	}
}
