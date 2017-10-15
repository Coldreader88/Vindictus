using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryShopItemInfo : Operation
	{
		public string ShopID { get; set; }

		public Dictionary<short, ShopTimeRestrictedResult> RestrictionInfo
		{
			get
			{
				return this.restrictInfo;
			}
			set
			{
				this.restrictInfo = value;
			}
		}

		public QueryShopItemInfo(string shopID)
		{
			this.ShopID = shopID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryShopItemInfo.Request(this);
		}

		[NonSerialized]
		private Dictionary<short, ShopTimeRestrictedResult> restrictInfo;

		private class Request : OperationProcessor<QueryShopItemInfo>
		{
			public Request(QueryShopItemInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				base.Result = true;
				yield return null;
				base.Operation.RestrictionInfo = (base.Feedback as Dictionary<short, ShopTimeRestrictedResult>);
				yield break;
			}
		}
	}
}
