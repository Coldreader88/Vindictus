using System;

namespace Nexon.CafeAuthJPN.Packets
{
	internal class Alive
	{
		private int CalculateStructureSize()
		{
			return 0;
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.Alive);
			return result;
		}
	}
}
