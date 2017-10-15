using System;

namespace Nexon.Nisms.Packets
{
	public class InventoryClearResponse
	{
		public Result Result { get; private set; }

		internal InventoryClearResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
		}
	}
}
