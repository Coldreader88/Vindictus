using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TownCampfireEffectCSMessage : IMessage
	{
		public int Level { get; set; }

		public int Type { get; set; }
	}
}
