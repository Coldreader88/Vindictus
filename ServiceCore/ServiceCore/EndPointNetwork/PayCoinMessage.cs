using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PayCoinMessage : IMessage
	{
		public CoinType CoinType { get; set; }

		public int ReceiverSlot { get; set; }

		public int CoinSlot { get; set; }

		public bool IsInsert { get; set; }

		public override string ToString()
		{
			return string.Format("PayCoinMessage[ {0} x {1} , To {2}/{3} ]", new object[]
			{
				this.CoinType.ToString(),
				this.IsInsert ? 1 : -1,
				this.ReceiverSlot,
				this.CoinSlot
			});
		}
	}
}
