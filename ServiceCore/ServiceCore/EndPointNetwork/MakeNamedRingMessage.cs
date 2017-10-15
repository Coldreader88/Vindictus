using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MakeNamedRingMessage : IMessage
	{
		public long ItemID { get; set; }

		public string UserName { get; set; }
	}
}
