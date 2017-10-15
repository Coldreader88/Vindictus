using System;

namespace Nexon.Nisms.Packets
{
	internal class InventoryPickupRequest
	{
		public int OrderNo { get; private set; }

		public int ProductNo { get; private set; }

		public short OrderQuantity { get; private set; }

		public string ExtendValue { get; private set; }

		internal InventoryPickupRequest(int orderNo, int productNo, short orderQuantity, string extendValue)
		{
			this.OrderNo = orderNo;
			this.ProductNo = productNo;
			this.OrderQuantity = orderQuantity;
			this.ExtendValue = extendValue;
		}

		private int CalculateStructureSize()
		{
			return this.OrderNo.CalculateStructureSize() + this.ProductNo.CalculateStructureSize() + this.OrderQuantity.CalculateStructureSize() + this.ExtendValue.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.InventoryPickup);
			result.Write(this.OrderNo);
			result.Write(this.ProductNo);
			result.Write(this.OrderQuantity);
			result.Write(this.ExtendValue);
			return result;
		}
	}
}
