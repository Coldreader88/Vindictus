using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[MutexCommand]
	[Guid("9B1CB661-0E8E-4d21-B892-B436A8CBBB2B")]
	[Serializable]
	public sealed class ServerInfoMessage
	{
		public string ServerIP { get; private set; }

		public int ServerPort { get; private set; }

		public ServerInfoMessage(string serverIP, int serverPort)
		{
			this.ServerIP = serverIP;
			this.ServerPort = serverPort;
		}
	}
}
