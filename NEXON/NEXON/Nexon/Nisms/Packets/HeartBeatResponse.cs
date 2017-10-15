using System;

namespace Nexon.Nisms.Packets
{
	internal class HeartBeatResponse
	{
		public Result Result { get; private set; }

		internal HeartBeatResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
		}
	}
}
