using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WishListSelectMessage : IMessage
	{
		public long CID { get; set; }

		public WishListSelectMessage(long cid)
		{
			this.CID = cid;
		}

		public override string ToString()
		{
			return string.Format("WishListSelectMessage[ CID = {0} ]", this.CID);
		}
	}
}
