using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TirCoinIgnoreItemInfo
	{
		public TirCoinIgnoreItemInfo(string ItemClass, int Amount, int Duration, int Price)
		{
			this.ItemClass = ItemClass;
			this.Amount = Amount;
			this.Duration = Duration;
			this.Price = Price;
		}

		public string ItemClass { get; private set; }

		public int Amount { get; private set; }

		public int Duration { get; private set; }

		public int Price { get; private set; }
	}
}
