using System;
using System.Collections.Generic;
using System.Text;

namespace Nexon.CafeAuth.Packets
{
	[Serializable]
	public sealed class SynchronizeRequest
	{
		public byte IsMonitering { get; internal set; }

		public List<long> SessionList { get; internal set; }

		internal SynchronizeRequest(ref Packet packet)
		{
			this.IsMonitering = packet.ReadByte();
			int num = packet.ReadInt32();
			this.SessionList = new List<long>();
			for (int i = 0; i < num; i++)
			{
				this.SessionList.Add(packet.ReadInt64());
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ SessionIDs = {");
			foreach (long value in this.SessionList)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(" ");
			}
			stringBuilder.Append("}");
			return base.ToString();
		}
	}
}
