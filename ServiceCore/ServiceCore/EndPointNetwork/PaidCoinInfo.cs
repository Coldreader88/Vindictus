using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PaidCoinInfo
	{
		public int Slot { get; set; }

		public ICollection<int> SilverCoin { get; set; }

		public int PlatinumCoinOwner { get; set; }

		public CoinType PlatinumCoinType { get; set; }
	}
}
