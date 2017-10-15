using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("8BCCDBD0-27F1-4be0-88A6-8E928FD5E60F")]
	[Serializable]
	public sealed class FunctionRequestMessage
	{
		public ArraySegment<byte> Packet { get; private set; }

		public int ClientID { get; private set; }

		public FunctionRequestMessage(ArraySegment<byte> packet, int id)
		{
			this.Packet = packet;
			this.ClientID = id;
		}
	}
}
