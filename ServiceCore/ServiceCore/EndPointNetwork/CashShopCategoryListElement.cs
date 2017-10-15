using System;
using Nexon.Nisms;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopCategoryListElement
	{
		public CashShopCategoryListElement(Category v)
		{
			this.CategoryNo = v.CategoryNo;
			this.CategoryName = v.CategoryName;
			this.ParentCategoryNo = v.ParentCategoryNo;
			this.DisplayNo = v.DisplayNo;
		}

		public int CategoryNo;

		public string CategoryName;

		public int ParentCategoryNo;

		public int DisplayNo;
	}
}
