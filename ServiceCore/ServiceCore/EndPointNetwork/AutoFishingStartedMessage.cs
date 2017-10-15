using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AutoFishingStartedMessage : IMessage
	{
		public int SerialNumber { get; set; }

		public int Tag { get; set; }

		public string Model { get; set; }

		public int ActionTimeInSeconds { get; set; }

		public int TimeoutInSeconds { get; set; }

		public bool IsCaughtAtHit { get; set; }

		public bool IsCaughtAtMiss { get; set; }

		public bool IsCaughtAtTimeout { get; set; }
	}
}
