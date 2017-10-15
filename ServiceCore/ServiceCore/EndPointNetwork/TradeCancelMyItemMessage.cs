using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradeCancelMyItemMessage : IMessage
	{
		public long TID { get; set; }

		public int UniqueNumber { get; set; }
	}
}
