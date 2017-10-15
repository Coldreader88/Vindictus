using System;

namespace UnifiedNetwork.Cooperation
{
	[Serializable]
	public sealed class FailMessage
	{
		public FailMessage.ReasonCode Reason { get; set; }

		public FailMessage()
		{
			this.Message = "";
		}

		public string Message { get; set; }

		public FailMessage(string message)
		{
			this.Message = message;
		}

		public override string ToString()
		{
			return string.Format("FailMessage [{0}/{1}]", this.Reason, this.Message);
		}

		public enum ReasonCode
		{
			NotSupportedOperation,
			LogicalFail,
			ExceptionOccured,
			TimeOut
		}
	}
}
