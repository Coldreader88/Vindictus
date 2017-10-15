using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexon.Nisms
{
	[Serializable]
	public class Product
	{
		public int ProductNo { get; private set; }

		public int RelationProductNo { get; private set; }

		public short ProductExpire { get; private set; }

		public short ProductPieces { get; private set; }

		public string ProductID { get; private set; }

		public Guid ProductGUID { get; private set; }

		public PaymentType PaymentType { get; private set; }

		public string ProductType { get; private set; }

		public int SalePrice { get; private set; }

		public int CategoryNo { get; private set; }

		public ProductStatus ProductStatus { get; private set; }

		public ICollection<BonusProduct> BonusProduct { get; private set; }

		internal Product(ref Packet packet)
		{
			this.ProductNo = packet.ReadInt32();
			this.RelationProductNo = packet.ReadInt32();
			this.ProductExpire = packet.ReadInt16();
			this.ProductPieces = packet.ReadInt16();
			this.ProductID = packet.ReadString();
			this.ProductGUID = packet.ReadGuid();
			this.PaymentType = (PaymentType)packet.ReadInt32();
			this.ProductType = packet.ReadString();
			this.SalePrice = packet.ReadInt32();
			this.CategoryNo = packet.ReadInt32();
			this.ProductStatus = (ProductStatus)packet.ReadInt16();
			byte b = packet.ReadByte();
			this.BonusProduct = new List<BonusProduct>((int)b);
			foreach (int num in Enumerable.Range(0, (int)b))
			{
				BonusProduct item = new BonusProduct(ref packet);
				this.BonusProduct.Add(item);
			}
		}
	}
}
