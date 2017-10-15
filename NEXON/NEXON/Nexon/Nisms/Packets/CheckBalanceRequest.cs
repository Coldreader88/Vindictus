using System;

namespace Nexon.Nisms.Packets
{
	internal class CheckBalanceRequest
	{
		public string UserID { get; private set; }

		internal CheckBalanceRequest(string userID)
		{
			this.UserID = userID;
		}

		private int CalculateStructureSize()
		{
			return this.UserID.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.CheckBalance);
			result.Write(this.UserID);
			return result;
		}
	}
}
