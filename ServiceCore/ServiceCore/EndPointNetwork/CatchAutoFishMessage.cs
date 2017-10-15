using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CatchAutoFishMessage : IMessage
	{
		public int SerialNumber { get; set; }

		public int Tag { get; set; }

		public int CatchTimeInSeconds { get; set; }
	}
}
