using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GameResourceRequestMessage : IMessage
	{
		public int RequestType { get; set; }

		public string RequestParam { get; set; }

		public GameResourceRequestMessage(int type, string param)
		{
			this.RequestType = type;
			this.RequestParam = param;
		}

		public override string ToString()
		{
			return string.Format("GameResourceRequestMessage {0}, {1}", this.RequestType, this.RequestParam);
		}

		public enum REQUEST_TYPE
		{
			DB3 = 1,
			BSP
		}
	}
}
