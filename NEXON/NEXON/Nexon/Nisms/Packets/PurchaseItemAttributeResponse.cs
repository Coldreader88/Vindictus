using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexon.Nisms.Packets
{
	public class PurchaseItemAttributeResponse
	{
		public string OrderID { get; private set; }

		public Result Result { get; private set; }

		public int OrderNo { get; private set; }

		public ICollection<PurchaseItemAttributeResponse.Product> ProductArray { get; private set; }

		internal PurchaseItemAttributeResponse(ref Packet packet)
		{
			this.OrderID = packet.ReadString();
			this.Result = (Result)packet.ReadInt32();
			this.OrderNo = packet.ReadInt32();
			byte b = packet.ReadByte();
			this.ProductArray = new List<PurchaseItemAttributeResponse.Product>((int)b);
			foreach (int num in Enumerable.Range(0, (int)b))
			{
				PurchaseItemAttributeResponse.Product item = new PurchaseItemAttributeResponse.Product(ref packet);
				this.ProductArray.Add(item);
			}
		}

		public PurchaseItemResponse ItemResponse
		{
			get
			{
				PurchaseItemResponse purchaseItemResponse = new PurchaseItemResponse
				{
					OrderID = this.OrderID,
					OrderNo = this.OrderNo,
					Result = this.Result,
					ProductArray = new List<PurchaseItemResponse.Product>()
				};
				foreach (PurchaseItemAttributeResponse.Product product in this.ProductArray)
				{
					PurchaseItemResponse.Product item = new PurchaseItemResponse.Product
					{
						ProductNo = product.ProductNo,
						OrderQuantity = product.OrderQuantity,
						ExtendValue = product.ExtendValue
					};
					purchaseItemResponse.ProductArray.Add(item);
				}
				return purchaseItemResponse;
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
