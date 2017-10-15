using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AnswerPlayerReconnectMessage : IMessage
	{
		public long CID { get; set; }

		public string PlayerName { get; set; }

		public bool Result { get; set; }
	}
}
