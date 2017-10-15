using System;

namespace Nexon.Nisms.Packets
{
	internal class InventoryPickupRollbackRequest
	{
		public int OrderNo { get; private set; }

		public int ProductNo { get; private set; }

		public string ExtendValue { get; private set; }

		internal InventoryPickupRollbackRequest(int orderNo, int productNo, string extendValue)
		{
			this.OrderNo = orderNo;
			this.ProductNo = productNo;
			this.ExtendValue = extendValue;
		}

		private int CalculateStructureSize()
		{
			return this.OrderNo.CalculateStructureSize() + this.ProductNo.CalculateStructureSize() + this.ExtendValue.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.InventoryPickupRollback);
			result.Write(this.OrderNo);
			result.Write(this.ProductNo);
			result.Write(this.ExtendValue);
			return result;
		}
	}
}
