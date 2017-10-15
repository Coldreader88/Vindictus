using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexon.Nisms.Packets
{
	internal class ProductInquiryResponse
	{
		public Result Result { get; private set; }

		public DateTime ReleaseTicks { get; private set; }

		public int TotalProductCount { get; private set; }

		public ICollection<Product> ProductArray { get; private set; }

		internal ProductInquiryResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
			this.ReleaseTicks = packet.ReadDateTime();
			this.TotalProductCount = packet.ReadInt32();
			int num = packet.ReadInt32();
			this.ProductArray = new List<Product>(num);
			foreach (int num2 in Enumerable.Range(0, num))
			{
				Product item = new Product(ref packet);
				this.ProductArray.Add(item);
			}
		}
	}
}
