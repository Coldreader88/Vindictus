using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ServerMessage
{
	[Guid("41F8DE92-BAEB-4cd8-90C6-96D159848BC3")]
	[Serializable]
	public sealed class ClientInitMessage
	{
		public RCClient Client { get; private set; }

		public ClientInitMessage(RCClient client)
		{
			this.Client = client.Clone();
		}
	}
}
