using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingKickMessage : IMessage
	{
		public int Slot { get; set; }

		public int NexonSN { get; set; }
	}
}
