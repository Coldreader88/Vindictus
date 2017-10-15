using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopInventoryMessage : IMessage
	{
		public ICollection<CashShopInventoryElement> Inventory { get; private set; }

		public CashShopInventoryMessage(ICollection<CashShopInventoryElement> inven)
		{
			this.Inventory = inven;
		}
	}
}
