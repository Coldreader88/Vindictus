using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TradeCancelMyItemResultMessage : IMessage
	{
		public int UniqueNumber { get; set; }

		public int Result { get; set; }
	}
}
