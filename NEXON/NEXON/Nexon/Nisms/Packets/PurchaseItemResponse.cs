using System;
using System.Collections.Generic;

namespace Nexon.Nisms.Packets
{
	public class PurchaseItemResponse
	{
		public string OrderID { get; set; }

		public Result Result { get; set; }

		public int OrderNo { get; set; }

		public ICollection<PurchaseItemResponse.Product> ProductArray { get; set; }

		public class Product
		{
			public int ProductNo { get; set; }

			public short OrderQuantity { get; set; }

			public string ExtendValue { get; set; }
		}
	}
}
