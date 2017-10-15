using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BuyItemResultMessage : IMessage
	{
		public int Result { get; set; }

		public string ItemClass { get; set; }

		public int ItemCount { get; set; }

		public string PriceItemClass { get; set; }

		public int PriceItemCount { get; set; }

		public int RestrictionItemOrder { get; set; }

		public int RestrictionItemCount { get; set; }

		public BuyItemResultMessage(BuyItemResultMessage.BuyItemResult result, string itemClass, int itemCount, string priceItemClass, int priceItemCount)
		{
			this.Result = (int)result;
			this.ItemClass = itemClass;
			this.ItemCount = itemCount;
			this.PriceItemClass = priceItemClass;
			this.PriceItemCount = priceItemCount;
			this.RestrictionItemOrder = -1;
			this.RestrictionItemCount = -1;
		}

		public BuyItemResultMessage(BuyItemResultMessage.BuyItemResult result, string itemClass, int itemCount, string priceItemClass, int priceItemCount, int restrictionItemOrder, int restrictionItemCount)
		{
			this.Result = (int)result;
			this.ItemClass = itemClass;
			this.ItemCount = itemCount;
			this.PriceItemClass = priceItemClass;
			this.PriceItemCount = priceItemCount;
			this.RestrictionItemOrder = restrictionItemOrder;
			this.RestrictionItemCount = restrictionItemCount;
		}

		public override string ToString()
		{
			return string.Format("BuyItemResultMessage[ result = {0} / {1}:{2} / {3}:{4} / {5}:{6}]", new object[]
			{
				this.Result,
				this.ItemClass,
				this.ItemCount,
				this.PriceItemClass,
				this.PriceItemCount,
				this.RestrictionItemOrder,
				this.RestrictionItemCount
			});
		}

		public enum BuyItemResult
		{
			Unknown,
			Succeeded,
			Failed,
			Failed_UniqueConstraint
		}
	}
}
