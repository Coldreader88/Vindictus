using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("7A290D75-36C4-4a91-92BC-0EC30EB925A9")]
	[Serializable]
	public sealed class ChildProcessLogRequestMessage
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public int ProcessID { get; private set; }

		public ChildProcessLogRequestMessage(int id, string processName, int pid)
		{
			this.ClientID = id;
			this.ProcessName = processName;
			this.ProcessID = pid;
		}
	}
}
