using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PayCoinCompletedMessage : IMessage
	{
		public ICollection<PaidCoinInfo> Coininfos { get; set; }

		public PayCoinCompletedMessage(ICollection<PaidCoinInfo> coininfos)
		{
			this.Coininfos = coininfos;
		}

		public override string ToString()
		{
			return string.Format("PayCoinCompletedMessage[ ]", new object[0]);
		}
	}
}
