using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyChatSendMessage : IMessage
	{
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		public PartyChatSendMessage(string message)
		{
			this.message = message;
		}

		public override string ToString()
		{
			return string.Format("PartyChatSendMessage[ message = {0} ]", this.message);
		}

		private string message;
	}
}
