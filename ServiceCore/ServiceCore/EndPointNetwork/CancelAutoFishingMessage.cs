using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CancelAutoFishingMessage : IMessage
	{
		public int SerialNumber { get; set; }

		public int Tag { get; set; }
	}
}
