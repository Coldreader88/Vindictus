using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WishListInsertMessage : IMessage
	{
		public long CID { get; set; }

		public ICollection<int> List_Wish { get; set; }

		public WishListInsertMessage(long cid, ICollection<int> list_wish)
		{
			this.CID = cid;
			this.List_Wish = list_wish;
		}

		public override string ToString()
		{
			return string.Format("WishListInsertMessage[ CID = {0}, ProductCount = {1} ]", this.CID, this.List_Wish.Count);
		}
	}
}
