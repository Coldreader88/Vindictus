using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WhisperFailMessage : IMessage
	{
		public WhisperFailMessage(string toName, string reason)
		{
			this.ToName = toName;
			this.Reason = reason;
		}

		public string ToName { get; set; }

		public string Reason { get; set; }

		public override string ToString()
		{
			return string.Format("[WhisperFailMessage : to {0}, reason {1}]", this.ToName, this.Reason);
		}
	}
}
