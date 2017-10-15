using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class CraftItem : Operation
	{
		public string ShopID { get; set; }

		public int Order { get; set; }

		public int Count { get; set; }

		public CraftItem(string shopID, int order, int count)
		{
			this.ShopID = shopID;
			this.Order = order;
			this.Count = count;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
