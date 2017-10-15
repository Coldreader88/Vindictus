using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WishListInsertResponseMessage : IMessage
	{
		public WishListResult Result { get; set; }

		public long CID { get; set; }

		public WishListInsertResponseMessage(WishListResult result, long cid)
		{
			this.Result = result;
			this.CID = cid;
		}

		public WishListInsertResponseMessage()
		{
		}

		public override string ToString()
		{
			return string.Format("WishListInsertResponseMessage[ Result = {0}, CID = {1}, ProductCount = {2} ]", this.Result, this.CID);
		}
	}
}
