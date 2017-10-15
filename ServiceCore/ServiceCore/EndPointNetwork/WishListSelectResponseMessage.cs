using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WishListSelectResponseMessage : IMessage
	{
		public WishListResult Result { get; set; }

		public long CID { get; set; }

		public ICollection<WishItemInfo> ProductInfo { get; set; }

		public WishListSelectResponseMessage(WishListResult result, long cid, ICollection<WishItemInfo> productinfo)
		{
			this.Result = result;
			this.CID = cid;
			this.ProductInfo = productinfo;
		}

		public WishListSelectResponseMessage()
		{
		}

		public override string ToString()
		{
			return string.Format("WishListSelectResponseMessage[ Result = {0}, CID = {1}, ProductCount = {2} ]", this.Result, this.CID, this.ProductInfo.Count);
		}
	}
}
