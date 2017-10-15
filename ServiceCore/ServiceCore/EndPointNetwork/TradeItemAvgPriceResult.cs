using System;

namespace ServiceCore.EndPointNetwork
{
	public class TradeItemAvgPriceResult
	{
		public int Min { get; private set; }

		public int Max { get; private set; }

		public long BuyCount { get; private set; }

		public long Sales { get; private set; }

		public TradeItemAvgPriceResult(int min, int max, long buyCount, long sales)
		{
			this.Min = min;
			this.Max = max;
			this.BuyCount = buyCount;
			this.Sales = sales;
		}
	}
}
