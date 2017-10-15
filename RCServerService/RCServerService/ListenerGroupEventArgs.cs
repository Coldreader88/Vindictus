using System;

namespace RemoteControlSystem.Server
{
	public class ListenerGroupEventArgs : EventArgs
	{
		public int ClientID { get; private set; }

		public string ProcessName { get; private set; }

		public int PID { get; private set; }

		public ListenerGroupEventArgs(int clientID, string processName, int pid)
		{
			this.ClientID = clientID;
			this.ProcessName = processName;
			this.PID = pid;
		}
	}
}
