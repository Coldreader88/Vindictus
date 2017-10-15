using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseFreeTitleMessage : IMessage
	{
		public long ItemID { get; set; }

		public string FreeTitleName { get; set; }
	}
}
