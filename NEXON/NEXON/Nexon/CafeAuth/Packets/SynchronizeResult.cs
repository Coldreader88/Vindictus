using System;
using System.Collections.Generic;

namespace Nexon.CafeAuth.Packets
{
	internal class SynchronizeResult
	{
		public byte IsMonitering { get; private set; }

		public Dictionary<long, byte> SessionResult { get; private set; }

		public SynchronizeResult(byte isMonitering, Dictionary<long, byte> result)
		{
			this.IsMonitering = isMonitering;
			this.SessionResult = result;
		}

		private int CalculateStructureSize()
		{
			if (this.SessionResult == null)
			{
				return this.IsMonitering.CalculateStructureSize() + 0.CalculateStructureSize();
			}
			return this.IsMonitering.CalculateStructureSize() + this.SessionResult.Count.CalculateStructureSize() + this.SessionResult.Count * (0L.CalculateStructureSize() + 0.CalculateStructureSize());
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.Synchronize);
			result.Write(this.IsMonitering);
			if (this.SessionResult == null)
			{
				result.Write(0);
			}
			else
			{
				result.Write(this.SessionResult.Count);
				foreach (KeyValuePair<long, byte> keyValuePair in this.SessionResult)
				{
					result.Write(keyValuePair.Key);
					result.Write(keyValuePair.Value);
				}
			}
			return result;
		}
	}
}
