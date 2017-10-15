using System;

namespace Nexon.Nisms.Packets
{
	internal class InventoryInquiryRequest
	{
		public string GameID { get; private set; }

		public ShowInventory ShowInventory { get; private set; }

		public int PageIndex { get; private set; }

		public int RowPerPage { get; private set; }

		internal InventoryInquiryRequest(string gameID, ShowInventory showInventory, int pageIndex, int rowPerPage)
		{
			this.GameID = gameID;
			this.ShowInventory = showInventory;
			this.PageIndex = pageIndex;
			this.RowPerPage = rowPerPage;
		}

		private int CalculateStructureSize()
		{
			return this.GameID.CalculateStructureSize() + ((byte)this.ShowInventory).CalculateStructureSize() + this.PageIndex.CalculateStructureSize() + this.RowPerPage.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.InventoryInquiry);
			result.Write(this.GameID);
			result.Write((byte)this.ShowInventory);
			result.Write(this.PageIndex);
			result.Write(this.RowPerPage);
			return result;
		}
	}
}
