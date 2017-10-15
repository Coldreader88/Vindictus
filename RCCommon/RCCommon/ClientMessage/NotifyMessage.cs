using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("6060DC40-E209-4f13-AE58-7BE94DC541F8")]
	[Serializable]
	public sealed class NotifyMessage
	{
		public MessageType MessageType { get; private set; }

		public string Message { get; private set; }

		public DateTime Time { get; private set; }

		public NotifyMessage(MessageType type, string messge)
		{
			this.MessageType = type;
			this.Message = messge;
			this.Time = DateTime.Now;
		}
	}
}
