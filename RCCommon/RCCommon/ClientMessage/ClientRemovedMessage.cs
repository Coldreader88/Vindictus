using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("F1B71A36-C588-47e2-BF2C-40438E92A21E")]
	[Serializable]
	public sealed class ClientRemovedMessage
	{
		public int ID { get; private set; }

		public ClientRemovedMessage(int clientId)
		{
			this.ID = clientId;
		}
	}
}
