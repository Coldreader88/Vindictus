using System;

namespace Nexon.Nisms.Packets
{
	internal class InitializeResponse
	{
		public string ServiceCode { get; private set; }

		public byte ServerNumber { get; private set; }

		public Result Result { get; private set; }

		internal InitializeResponse(ref Packet packet)
		{
			this.ServiceCode = packet.ReadString();
			this.ServerNumber = packet.ReadByte();
			this.Result = (Result)packet.ReadInt32();
		}
	}
}
