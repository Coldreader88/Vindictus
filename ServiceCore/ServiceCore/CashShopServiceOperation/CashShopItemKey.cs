using System;

namespace ServiceCore.CashShopServiceOperation
{
	[Serializable]
	public sealed class CashShopItemKey
	{
		public string ItemClass { get; set; }

		public int Price { get; set; }

		public int Expire { get; set; }

		public CashShopItemKey(string itemClass, int price, int expire)
		{
			this.ItemClass = itemClass;
			this.Price = price;
			this.Expire = expire;
		}

		public override int GetHashCode()
		{
			return this.ItemClass.GetHashCode() ^ this.Price.GetHashCode() ^ this.Expire.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			CashShopItemKey cashShopItemKey = obj as CashShopItemKey;
			return cashShopItemKey != null && (this.ItemClass == cashShopItemKey.ItemClass && this.Price == cashShopItemKey.Price) && this.Expire == cashShopItemKey.Expire;
		}

		public override string ToString()
		{
			return string.Format("{0}/{1}/{2}", this.ItemClass, this.Price, this.Expire);
		}
	}
}
