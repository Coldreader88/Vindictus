using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BeautyShopCouponListElement
	{
		public BeautyShopCouponListElement(string category, string itemClass, int weight)
		{
			this.ItemClass = itemClass;
			this.Category = category;
			this.Weight = weight;
		}

		public string Category;

		public string ItemClass;

		public int Weight;
	}
}
