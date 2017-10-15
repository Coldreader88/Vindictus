using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("FEFB9F8A-26E2-4d79-9619-6ACF5CE3F3FC")]
	[Serializable]
	public class AdminBroadcastConsoleCommandMessage
	{
		public bool isServerCommand { get; set; }

		public string commandString { get; set; }

		public override string ToString()
		{
			return string.Format("AdminBroadcastConsoleCommandMessage [{0}][{1}]", this.isServerCommand ? "Server" : "Client", this.commandString);
		}
	}
}
