using System;

namespace Nexon.Nisms.Packets
{
	public class InventoryPickupRollbackResponse
	{
		public Result Result { get; private set; }

		public int OrderNo { get; private set; }

		public int ProductNo { get; private set; }

		public string ExtendValue { get; private set; }

		internal InventoryPickupRollbackResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
			this.OrderNo = packet.ReadInt32();
			this.ProductNo = packet.ReadInt32();
			this.ExtendValue = packet.ReadString();
		}
	}
}
