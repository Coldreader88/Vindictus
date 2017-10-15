using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WishListDeleteResponseMessage : IMessage
	{
		public WishListResult Result { get; set; }

		public long CID { get; set; }

		public WishListDeleteResponseMessage(WishListResult result, long cid)
		{
			this.Result = result;
			this.CID = cid;
		}

		public WishListDeleteResponseMessage()
		{
		}

		public override string ToString()
		{
			return string.Format("WishListDeleteResponseMessgae[ Result = {0}, CID = {1}, ProductCount = {2} ]", this.Result, this.CID);
		}
	}
}
