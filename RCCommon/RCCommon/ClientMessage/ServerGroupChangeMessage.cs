using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("04ED6C1F-9421-4ae7-A767-8B2051C9EBEE")]
	[Serializable]
	public sealed class ServerGroupChangeMessage
	{
		public string ServerGroup { get; private set; }

		public ServerGroupChangeMessage(string servergroup)
		{
			this.ServerGroup = servergroup;
		}
	}
}
