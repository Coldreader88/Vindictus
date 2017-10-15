using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopCategoryListMessage : IMessage
	{
		public CashShopCategoryListMessage(ICollection<CashShopCategoryListElement> categoryList)
		{
			this.list = categoryList;
		}

		private ICollection<CashShopCategoryListElement> list;
	}
}
