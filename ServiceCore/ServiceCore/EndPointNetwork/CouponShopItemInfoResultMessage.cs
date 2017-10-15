using System;
using System.Collections.Generic;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CouponShopItemInfoResultMessage : IMessage
	{
		public short ShopVersion { get; set; }

		public IDictionary<short, ShopTimeRestrictedResult> RestrictionCountDic { get; set; }

		public CouponShopItemInfoResultMessage(short shopVersion, IDictionary<short, ShopTimeRestrictedResult> restrictionCountDic)
		{
			this.ShopVersion = shopVersion;
			this.RestrictionCountDic = restrictionCountDic;
		}

		public override string ToString()
		{
			return string.Format("CouponShopItemInfoResultMessage[ {0}  / {1} ]", this.ShopVersion, this.RestrictionCountDic.Count);
		}
	}
}
