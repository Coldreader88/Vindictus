using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HotSpringAddPotionMessage : IMessage
	{
		public int Channel { get; set; }

		public int TownID { get; set; }

		public long ItemID { get; set; }
	}
}
