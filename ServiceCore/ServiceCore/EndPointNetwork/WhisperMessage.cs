using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WhisperMessage : IMessage
	{
		public string Sender { get; set; }

		public string Contents { get; set; }

		public override string ToString()
		{
			return string.Format("[WhisperMessage : to {0}]", this.Sender);
		}
	}
}
