using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class XignCodeReadyMessage : IMessage
	{
		public string UserID { get; set; }

		public XignCodeReadyMessage(string userID)
		{
			this.UserID = userID;
		}

		public override string ToString()
		{
			return "XignCodeReadyMessage";
		}
	}
}
