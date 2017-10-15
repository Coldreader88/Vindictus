using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UserPunishNotifyMessage : IMessage
	{
		public byte Type { get; set; }

		public string Message { get; set; }

		public long RemainSeconds { get; set; }

		public UserPunishNotifyMessage(UserPunishNotifyType Type, string Message, long RemainSeconds)
		{
			this.Type = (byte)Type;
			this.Message = Message;
			this.RemainSeconds = RemainSeconds;
		}
	}
}
