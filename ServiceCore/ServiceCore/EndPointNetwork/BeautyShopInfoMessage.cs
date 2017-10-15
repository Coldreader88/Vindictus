using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BeautyShopInfoMessage : IMessage
	{
		private ICollection<CashShopCategoryListElement> CategoryList { get; set; }

		private ICollection<CashShopProductListElement> ProductList { get; set; }

		private ICollection<BeautyShopCouponListElement> CouponList { get; set; }

		public BeautyShopInfoMessage(ICollection<CashShopCategoryListElement> categoryList, ICollection<CashShopProductListElement> productList, ICollection<BeautyShopCouponListElement> couponList)
		{
			this.CategoryList = categoryList;
			this.ProductList = productList;
			this.CouponList = couponList;
		}
	}
}
