using System;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	internal static class VirtualPacketFlag
	{
		public static TransrouterMessage GetMessage(long flag)
		{
			return (TransrouterMessage)((int)flag >> 24 & 255);
		}

		public static int GetTargetID(long flag)
		{
			return (int)flag & 16777215;
		}

		public static int GetSecondaryTargetID(Packet packet)
		{
			return BitConverter.ToInt32(packet.Array, packet.Offset) & 16777215;
		}

		public unsafe static void SetSecondaryTargetID(Packet packet, int id)
		{
			fixed (byte* ptr = &packet.Array[packet.Offset])
			{
				*(int*)ptr = id;
			}
		}

		public static int GenerateFlag(TransrouterMessage message, int targetID)
		{
			return (int)(message | (TransrouterMessage)(targetID & -256));
		}

		public static int GenerateClientFlag(int serverID)
		{
			return serverID | -1068630016;
		}
	}
}
