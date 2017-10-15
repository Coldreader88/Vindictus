using System;
using System.Collections.Generic;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShopItemInfoResultMessage : IMessage
	{
		public string ShopID { get; set; }

		public Dictionary<short, ShopTimeRestrictedResult> RestrictionCountDic { get; set; }

		public ShopItemInfoResultMessage(string shopID, Dictionary<short, ShopTimeRestrictedResult> restrictionCountDic)
		{
			this.ShopID = shopID;
			this.RestrictionCountDic = restrictionCountDic;
		}

		public override string ToString()
		{
			return string.Format("ShopItemInfoResultMessage[ result = {0} / {1} ]", this.ShopID, this.RestrictionCountDic.Count);
		}
	}
}
