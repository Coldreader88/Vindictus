using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("333F9B14-D4A0-4274-8B5A-9C1DDDB32DF5")]
	[Serializable]
	public sealed class ControlReplyMessage
	{
		public int ID { get; private set; }

		public ArraySegment<byte> Packet { get; private set; }

		public ControlReplyMessage(int clientId, ArraySegment<byte> packet)
		{
			this.ID = clientId;
			this.Packet = packet;
		}
	}
}
