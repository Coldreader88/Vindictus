using System;

namespace Nexon.Nisms.Packets
{
	internal class ProductInquiryRequest
	{
		public int PageIndex { get; set; }

		public int RowPerPage { get; set; }

		internal ProductInquiryRequest(int pageIndex, int rowPerPage)
		{
			this.PageIndex = pageIndex;
			this.RowPerPage = rowPerPage;
		}

		private int CalculateStructureSize()
		{
			return this.PageIndex.CalculateStructureSize() + this.RowPerPage.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.ProductInquiryVersion2);
			result.Write(this.PageIndex);
			result.Write(this.RowPerPage);
			return result;
		}
	}
}
