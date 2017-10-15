using System;
using System.Net;

namespace Nexon.CafeAuth.Packets
{
	internal class Logout
	{
		public long SessionID { get; private set; }

		public string NexonID { get; private set; }

		public string CharacterID { get; private set; }

		public IPAddress RemoteAddress { get; private set; }

		public bool CanTry { get; private set; }

		public Logout(long sID, string nexonID, string characterID, IPAddress remoteAddress, bool canTry)
		{
			this.SessionID = sID;
			this.NexonID = nexonID;
			this.CharacterID = characterID;
			this.RemoteAddress = remoteAddress;
			this.CanTry = canTry;
		}

		private int CalculateStructureSize()
		{
			return this.LogoutType.CalculateStructureSize() + this.NexonID.CalculateStructureSize() + this.SessionID.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.Logout);
			result.Write(this.LogoutType);
			result.Write(this.NexonID);
			result.Write(this.SessionID);
			return result;
		}

		public readonly byte LogoutType = 1;
	}
}
