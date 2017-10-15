using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WishListDeleteMessage : IMessage
	{
		public long CID { get; set; }

		public IList<int> ProductNo { get; set; }

		public WishListDeleteMessage(long cid, IList<int> productno)
		{
			this.CID = cid;
			this.ProductNo = productno;
		}

		public override string ToString()
		{
			return string.Format("WishListDeleteMessage[ CID = {0}, ProductCount = {1} ]", this.CID, this.ProductNo.Count);
		}
	}
}
