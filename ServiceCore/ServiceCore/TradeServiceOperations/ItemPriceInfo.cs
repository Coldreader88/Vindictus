using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.TradeServiceOperations
{
	[Serializable]
	public sealed class ItemPriceInfo : Operation
	{
		public int PageNum { get; set; }

		public Dictionary<string, PriceRange> PartialPrices
		{
			get
			{
				return this.Prices;
			}
		}

		public bool IsRemain
		{
			get
			{
				return this.Remain;
			}
		}

		public ItemPriceInfo(int num)
		{
			this.PageNum = num;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new ItemPriceInfo.Request(this);
		}

		[NonSerialized]
		private Dictionary<string, PriceRange> Prices;

		[NonSerialized]
		public bool Remain;

		private class Request : OperationProcessor<ItemPriceInfo>
		{
			public Request(ItemPriceInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is Dictionary<string, PriceRange>)
				{
					base.Operation.Prices = (base.Feedback as Dictionary<string, PriceRange>);
					yield return null;
					base.Operation.Remain = (bool)base.Feedback;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
