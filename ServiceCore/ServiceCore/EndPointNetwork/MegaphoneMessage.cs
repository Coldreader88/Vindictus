using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MegaphoneMessage : IMessage
	{
		public int MessageType { get; set; }

		public string SenderName { get; set; }

		public string Message { get; set; }
	}
}
