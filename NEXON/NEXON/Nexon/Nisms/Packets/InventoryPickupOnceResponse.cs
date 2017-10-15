using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexon.Nisms.Packets
{
	public class InventoryPickupOnceResponse
	{
		public Result Result { get; private set; }

		public int OrderNo { get; private set; }

		public int ProductNo { get; private set; }

		public ProductKind ProductKind { get; private set; }

		public string ProductName { get; private set; }

		public string ProductID { get; private set; }

		public short ProductExpire { get; private set; }

		public short ProductPieces { get; private set; }

		public short OrderQuantity { get; private set; }

		public string ProductAttribute0 { get; private set; }

		public string ProductAttribute1 { get; private set; }

		public string ProductAttribute2 { get; private set; }

		public string ProductAttribute3 { get; private set; }

		public string ProductAttribute4 { get; private set; }

		public string ExtendValue { get; private set; }

		public ICollection<SubProduct> SubProduct { get; private set; }

		public ICollection<BonusProduct> BonusProduct { get; private set; }

		internal InventoryPickupOnceResponse(ref Packet packet)
		{
			this.Result = (Result)packet.ReadInt32();
			this.OrderNo = packet.ReadInt32();
			this.ProductNo = packet.ReadInt32();
			this.ProductKind = (ProductKind)packet.ReadByte();
			this.ProductName = packet.ReadString();
			this.ProductID = packet.ReadString();
			this.ProductExpire = packet.ReadInt16();
			this.ProductPieces = packet.ReadInt16();
			this.OrderQuantity = packet.ReadInt16();
			this.ProductAttribute0 = packet.ReadString();
			this.ProductAttribute1 = packet.ReadString();
			this.ProductAttribute2 = packet.ReadString();
			this.ProductAttribute3 = packet.ReadString();
			this.ProductAttribute4 = packet.ReadString();
			this.ExtendValue = packet.ReadString();
			int num = packet.ReadInt32();
			this.SubProduct = new List<SubProduct>(num);
			foreach (int num2 in Enumerable.Range(0, num))
			{
				SubProduct item = new SubProduct(ref packet);
				this.SubProduct.Add(item);
			}
			int num3 = packet.ReadInt32();
			this.BonusProduct = new List<BonusProduct>(num3);
			foreach (int num4 in Enumerable.Range(0, num3))
			{
				BonusProduct item2 = new BonusProduct(ref packet);
				this.BonusProduct.Add(item2);
			}
		}
	}
}
