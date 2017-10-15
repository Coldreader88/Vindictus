using System;

namespace Nexon.Nisms.Packets
{
	internal class InventoryInquiryReadRequest
	{
		public int OrderNo { get; private set; }

		public int ProductNo { get; private set; }

		internal InventoryInquiryReadRequest(int orderNo, int productNo)
		{
			this.OrderNo = orderNo;
			this.ProductNo = productNo;
		}

		private int CalculateStructureSize()
		{
			return this.OrderNo.CalculateStructureSize() + this.ProductNo.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.InventoryInquiryRead);
			result.Write(this.OrderNo);
			result.Write(this.ProductNo);
			return result;
		}
	}
}
