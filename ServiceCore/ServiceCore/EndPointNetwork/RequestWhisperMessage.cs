using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestWhisperMessage : IMessage
	{
		public string Receiver { get; set; }

		public string Contents { get; set; }

		public override string ToString()
		{
			return string.Format("[RequestWhisperMessage : to {0}]", this.Receiver);
		}
	}
}
