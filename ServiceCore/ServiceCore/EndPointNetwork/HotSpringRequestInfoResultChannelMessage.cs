using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HotSpringRequestInfoResultChannelMessage : IMessage
	{
		public ICollection<HotSpringPotionEffectInfo> HotSpringPotionEffectInfos { get; set; }

		public int TownID { get; set; }

		public HotSpringRequestInfoResultChannelMessage(ICollection<HotSpringPotionEffectInfo> hotSpringPotionEffectInfos, int townID)
		{
			this.HotSpringPotionEffectInfos = new List<HotSpringPotionEffectInfo>(hotSpringPotionEffectInfos);
			this.TownID = townID;
		}
	}
}
