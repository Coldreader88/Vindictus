using System;

namespace Nexon.Nisms.Packets
{
	public class InventoryPickupResponse
	{
		public Result Result { get; private set; }

		public int OrderNo { get; private set; }

		public int ProductNo { get; private set; }

		public short OrderQuantity { get; private set; }

		public string ExtendValue { get; private set; }

		internal InventoryPickupResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
			this.OrderNo = packet.ReadInt32();
			this.ProductNo = packet.ReadInt32();
			this.OrderQuantity = packet.ReadInt16();
			this.ExtendValue = packet.ReadString();
		}
	}
}
