using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class KickMessage : IMessage
	{
		public int Slot { get; set; }

		public int NexonSN { get; set; }

		public override string ToString()
		{
			return string.Format("KickMessage[ Slot : {0} NexonSN : {1}]", this.Slot, this.NexonSN);
		}
	}
}
