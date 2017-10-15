using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexon.Nisms.Packets
{
	public class PurchaseGiftAttributeResponse
	{
		public string OrderID { get; private set; }

		public Result Result { get; private set; }

		public int OrderNo { get; private set; }

		public ICollection<PurchaseGiftAttributeResponse.Product> ProductArray { get; private set; }

		internal PurchaseGiftAttributeResponse(ref Packet packet)
		{
			this.OrderID = packet.ReadString();
			this.Result = (Result)packet.ReadInt32();
			this.OrderNo = packet.ReadInt32();
			byte b = packet.ReadByte();
			this.ProductArray = new List<PurchaseGiftAttributeResponse.Product>((int)b);
			foreach (int num in Enumerable.Range(0, (int)b))
			{
				PurchaseGiftAttributeResponse.Product item = new PurchaseGiftAttributeResponse.Product(ref packet);
				this.ProductArray.Add(item);
			}
		}

		public class Product
		{
			public int ProductNo { get; private set; }

			public short OrderQuantity { get; private set; }

			public string ExtendValue { get; private set; }

			internal Product(ref Packet packet)
			{
				this.ProductNo = packet.ReadInt32();
				this.OrderQuantity = packet.ReadInt16();
				this.ExtendValue = packet.ReadString();
			}
		}
	}
}
