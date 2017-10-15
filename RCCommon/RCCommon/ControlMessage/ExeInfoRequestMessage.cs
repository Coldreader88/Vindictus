using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("CF40C361-059F-47fc-A048-B21A23CAD8F3")]
	[Serializable]
	public sealed class ExeInfoRequestMessage
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public ExeInfoRequestMessage(int id, string processName)
		{
			this.ClientID = id;
			this.ProcessName = processName;
		}
	}
}
