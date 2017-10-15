using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class FreeTitleNameCheckMessage : IMessage
	{
		public long ItemID { get; set; }

		public string FreeTitleName { get; set; }
	}
}
