using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseMegaphoneMessage : IMessage
	{
		public long ItemID { get; set; }

		public int MessageType { get; set; }

		public string Message { get; set; }
	}
}
