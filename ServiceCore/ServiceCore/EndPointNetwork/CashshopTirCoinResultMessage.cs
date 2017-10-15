using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashshopTirCoinResultMessage : IMessage
	{
		public bool isSuccess { get; set; }

		public bool isBeautyShop { get; set; }

		public int successCount { get; set; }

		public List<TirCoinIgnoreItemInfo> IgnoreItems { get; set; }

		public override string ToString()
		{
			return string.Format("CashshopTirCoinResultMessage[ ]", new object[0]);
		}
	}
}
