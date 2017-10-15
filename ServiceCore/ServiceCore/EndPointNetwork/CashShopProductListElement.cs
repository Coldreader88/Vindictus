using System;
using Nexon.Nisms;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopProductListElement
	{
		public CashShopProductListElement(Product v)
		{
			this.ProductNo = v.ProductNo;
			this.ProductExpire = v.ProductExpire;
			this.ProductPieces = v.ProductPieces;
			this.ProductID = v.ProductID;
			this.ProductGUID = v.ProductGUID.ToString();
			this.PaymentType = (int)v.PaymentType;
			this.ProductType = v.ProductType;
			this.SalePrice = v.SalePrice;
			this.CategoryNo = v.CategoryNo;
			this.Status = (int)v.ProductStatus;
		}

		public int ProductNo;

		public short ProductExpire;

		public short ProductPieces;

		public string ProductID;

		public string ProductGUID;

		public int PaymentType;

		public string ProductType;

		public int SalePrice;

		public int CategoryNo;

		public int Status;
	}
}
