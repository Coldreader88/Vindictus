using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("7A290D75-36C4-4a91-92BC-0EC30EB925A9")]
	[Serializable]
	public sealed class ChildProcessLogReplyMessage
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public int ProcessID { get; private set; }

		public List<RCProcess.ChildProcessLog> Log { get; private set; }

		public ChildProcessLogReplyMessage(int id, string processName, int pid, List<RCProcess.ChildProcessLog> log)
		{
			this.ClientID = id;
			this.ProcessName = processName;
			this.ProcessID = pid;
			this.Log = log;
		}
	}
}
