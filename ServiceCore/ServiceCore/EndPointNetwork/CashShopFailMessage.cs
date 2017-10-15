using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopFailMessage : IMessage
	{
		public CashShopFailMessage(int reason)
		{
			this.Reason = reason;
		}

		private int Reason;
	}
}
