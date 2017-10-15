using System;

namespace Nexon.Nisms.Packets
{
	internal class InventoryClearRequest
	{
		public string GameID { get; private set; }

		public string ProcessMessage { get; private set; }

		internal InventoryClearRequest(string gameID, string processMessage)
		{
			this.GameID = gameID;
			this.ProcessMessage = processMessage;
		}

		private int CalculateStructureSize()
		{
			return this.GameID.CalculateStructureSize() + this.ProcessMessage.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.InventoryClear);
			result.Write(this.GameID);
			result.Write(this.ProcessMessage);
			return result;
		}
	}
}
