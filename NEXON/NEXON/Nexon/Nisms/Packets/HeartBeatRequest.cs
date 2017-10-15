using System;

namespace Nexon.Nisms.Packets
{
	internal class HeartBeatRequest
	{
		public DateTime ReleaseTicks { get; private set; }

		internal HeartBeatRequest(DateTime releaseTicks)
		{
			this.ReleaseTicks = releaseTicks;
		}

		private int CalculateStructureSize()
		{
			return this.ReleaseTicks.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.HeartBeat);
			result.Write(this.ReleaseTicks);
			return result;
		}
	}
}
