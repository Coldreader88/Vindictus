using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CustomizeItemRequestInfo
	{
		public string ItemClass { get; set; }

		public string Category { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }

		public int Duration { get; set; }

		public string Price { get; set; }

		public long CouponItemID { get; set; }

		public CustomizeItemRequestInfo(string itemClass, string category, int color1, int color2, int color3, int duration, string price, long couponItemID)
		{
			this.ItemClass = itemClass;
			this.Category = category;
			this.Color1 = color1;
			this.Color2 = color2;
			this.Color3 = color3;
			this.Duration = duration;
			this.Price = price;
			this.CouponItemID = couponItemID;
		}

		public override string ToString()
		{
			return string.Format("{0}({1}/{2}/{3})", new object[]
			{
				this.ItemClass,
				this.Color1,
				this.Color2,
				this.Color3
			});
		}
	}
}
