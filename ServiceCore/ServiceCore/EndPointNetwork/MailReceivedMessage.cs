using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MailReceivedMessage : IMessage
	{
		public string FromName { get; set; }

		public byte MailType { get; set; }

		public MailReceivedMessage(string FromName, byte MailType)
		{
			this.FromName = FromName;
			this.MailType = MailType;
		}

		public override string ToString()
		{
			return string.Format("MailReceivedMessage[ FromName = {0} MailType = {1} ]", this.FromName, this.MailType);
		}
	}
}
