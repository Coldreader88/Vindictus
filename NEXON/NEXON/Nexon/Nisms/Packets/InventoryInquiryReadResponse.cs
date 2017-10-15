using System;

namespace Nexon.Nisms.Packets
{
	public class InventoryInquiryReadResponse
	{
		public Result Result { get; private set; }

		internal InventoryInquiryReadResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
		}
	}
}
