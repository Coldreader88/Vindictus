using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryCouponShopItemInfo : Operation
	{
		public short ShopVersion { get; set; }

		public IDictionary<short, ShopTimeRestrictedResult> RestrictionInfo
		{
			get
			{
				return this.restrictInfo;
			}
		}

		public QueryCouponShopItemInfo(short shopVersion)
		{
			this.ShopVersion = shopVersion;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCouponShopItemInfo.Request(this);
		}

		[NonSerialized]
		private IDictionary<short, ShopTimeRestrictedResult> restrictInfo;

		private class Request : OperationProcessor<QueryCouponShopItemInfo>
		{
			public Request(QueryCouponShopItemInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IDictionary<short, ShopTimeRestrictedResult>)
				{
					base.Result = true;
					base.Operation.restrictInfo = (base.Feedback as IDictionary<short, ShopTimeRestrictedResult>);
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
