using System;

namespace Nexon.Nisms.Packets
{
	internal class CategoryInquiryRequest
	{
		internal CategoryInquiryRequest()
		{
		}

		private int CalculateStructureSize()
		{
			return 0;
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.CategoryInquiry);
			return result;
		}
	}
}
