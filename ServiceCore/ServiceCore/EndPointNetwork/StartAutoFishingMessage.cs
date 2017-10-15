using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StartAutoFishingMessage : IMessage
	{
		public int SerialNumber { get; set; }

		public int Tag { get; set; }

		public string Argument { get; set; }
	}
}
