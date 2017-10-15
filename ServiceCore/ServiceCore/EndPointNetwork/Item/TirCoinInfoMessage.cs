using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class TirCoinInfoMessage : IMessage
	{
		public IDictionary<byte, int> TirCoinInfo { get; set; }

		public TirCoinInfoMessage(IDictionary<byte, int> tircoinInfo)
		{
			this.TirCoinInfo = tircoinInfo;
		}

		public override string ToString()
		{
			return string.Format("TirCoinInfoMessage[ TirCoinInfo x {0} ]", this.TirCoinInfo.Count);
		}
	}
}
