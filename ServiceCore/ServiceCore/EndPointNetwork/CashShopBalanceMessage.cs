using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopBalanceMessage : IMessage
	{
		public int Balance { get; set; }

		public int Refundless { get; set; }

		public CashShopBalanceMessage(int balance, int refundless)
		{
			this.Balance = balance;
			this.Refundless = refundless;
		}

		public override string ToString()
		{
			return string.Format("CashShopBalanceMessage ( {0}, {1} )", this.Balance, this.Refundless);
		}
	}
}
