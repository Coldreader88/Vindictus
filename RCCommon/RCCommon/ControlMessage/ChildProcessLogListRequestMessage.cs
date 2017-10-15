using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("2D0D1F46-6813-4b80-8F56-0CB71B56AF55")]
	[Serializable]
	public sealed class ChildProcessLogListRequestMessage
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public ChildProcessLogListRequestMessage(int id, string processName)
		{
			this.ClientID = id;
			this.ProcessName = processName;
		}
	}
}
