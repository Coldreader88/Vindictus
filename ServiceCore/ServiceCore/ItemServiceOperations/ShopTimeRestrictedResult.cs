using System;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ShopTimeRestrictedResult
	{
		public int BuyableCount { get; set; }

		public long NextResetTicksDiff { get; set; }

		public ShopTimeRestrictedResult(int buyableCount, long nextResetTicksDiff)
		{
			this.BuyableCount = buyableCount;
			this.NextResetTicksDiff = nextResetTicksDiff;
		}
	}
}
