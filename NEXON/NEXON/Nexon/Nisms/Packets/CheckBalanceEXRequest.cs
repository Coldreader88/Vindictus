using System;

namespace Nexon.Nisms.Packets
{
	internal class CheckBalanceEXRequest
	{
		public string UserID { get; private set; }

		internal CheckBalanceEXRequest(string userID)
		{
			this.UserID = userID;
		}

		private int CalculateStructureSize()
		{
			return this.UserID.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.CheckBalanceEX);
			result.Write(this.UserID);
			return result;
		}
	}
}
