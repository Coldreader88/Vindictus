using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HotSpringRequestPotionEffectMessage : IMessage
	{
		public int Channel { get; set; }

		public int TownID { get; set; }

		public string PotionItemClass { get; set; }
	}
}
