using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BurnItemInfo : IMessage
	{
		public long ItemID { get; set; }

		public int Count { get; set; }
	}
}
