using System;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class CashShopItem
	{
		public CashShopItem(string itemClass, int c1, int c2, int c3, int price, int expire)
		{
			this.Key = new CashShopItemKey(itemClass, price, expire);
			this.ItemClass = itemClass;
			this.Price = price;
			this.Expire = expire;
			this.Attribute0 = c1.ToString();
			this.Attribute1 = c2.ToString();
			this.Attribute2 = c3.ToString();
			this.Attribute3 = "";
			this.Attribute4 = "";
		}

		public CashShopItemKey Key { get; set; }

		public string ItemClass { get; set; }

		public int Price { get; set; }

		public int Expire { get; set; }

		public string Attribute0 { get; set; }

		public string Attribute1 { get; set; }

		public string Attribute2 { get; set; }

		public string Attribute3 { get; set; }

		public string Attribute4 { get; set; }
	}
}
