using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("3227E588-1D10-4e5e-9CDF-19B192584890")]
	[Serializable]
	public sealed class ClientAddedMessage
	{
		public int ID { get; private set; }

		public RCClient Client { get; private set; }

		public ClientAddedMessage(int clientId, RCClient client)
		{
			this.ID = clientId;
			this.Client = client;
		}
	}
}
