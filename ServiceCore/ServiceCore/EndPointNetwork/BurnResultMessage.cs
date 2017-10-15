using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BurnResultMessage : IMessage
	{
		public string Message { get; set; }

		public int Gauge { get; set; }

		public BurnResultMessage(string message, int gauge)
		{
			this.Message = message;
			this.Gauge = gauge;
		}
	}
}
