using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ControlMessage
{
	[Guid("3A65EFB1-C65A-4a8b-8312-331F43E87317")]
	[Serializable]
	public sealed class FunctionReplyMessage
	{
		public ArraySegment<byte> Packet { get; private set; }

		public int ClientID { get; private set; }

		public FunctionReplyMessage(ArraySegment<byte> packet, int id)
		{
			this.Packet = packet;
			this.ClientID = id;
		}
	}
}
