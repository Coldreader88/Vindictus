using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BurnJackpotMessage : IMessage
	{
		public long CID { get; set; }

		public BurnJackpotMessage(long cid)
		{
			this.CID = cid;
		}
	}
}
