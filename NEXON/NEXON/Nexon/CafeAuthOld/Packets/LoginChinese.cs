using System;
using System.Net;

namespace Nexon.CafeAuthOld.Packets
{
	internal class LoginChinese
	{
		public string NexonID { get; private set; }

		public string CharacterID { get; private set; }

		public IPAddress RemoteAddress { get; private set; }

		public bool CanTry { get; private set; }

		public LoginChinese(string nexonID, string characterID, IPAddress remoteAddress, bool canTry)
		{
			this.NexonID = nexonID;
			this.CharacterID = characterID;
			this.RemoteAddress = remoteAddress;
			this.CanTry = canTry;
		}

		private int CalculateStructureSize()
		{
			return 0.CalculateStructureSize() + this.NexonID.CalculateStructureSize() + this.CharacterID.CalculateStructureSize() + this.RemoteAddress.CalculateStructureSize() + this.CanTry.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.LoginLogout);
			result.Write(0);
			result.Write(this.NexonID);
			result.Write(this.CharacterID);
			result.Write(this.RemoteAddress);
			result.Write(this.CanTry);
			return result;
		}
	}
}
