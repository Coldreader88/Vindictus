using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopProductListMessage : IMessage
	{
		public CashShopProductListMessage(ICollection<CashShopProductListElement> productList)
		{
			this.list = productList;
		}

		private ICollection<CashShopProductListElement> list;
	}
}
