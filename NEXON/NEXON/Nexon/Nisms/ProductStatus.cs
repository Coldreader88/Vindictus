using System;

namespace Nexon.Nisms
{
	[Flags]
	public enum ProductStatus : short
	{
		OnSale = 1,
		SoldOut = 2,
		Discount = 4,
		Event = 8,
		PrePurchase = 16,
		NewRelease = 32,
		TopSeller = 64
	}
}
