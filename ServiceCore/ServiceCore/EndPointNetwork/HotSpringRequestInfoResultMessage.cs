using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HotSpringRequestInfoResultMessage : IMessage
	{
		public ICollection<HotSpringPotionEffectInfo> HotSpringPotionEffectInfos { get; set; }

		public int TownID { get; set; }

		public HotSpringRequestInfoResultMessage(ICollection<HotSpringPotionEffectInfo> hotSpringPotionEffectInfos, int townID)
		{
			this.HotSpringPotionEffectInfos = new List<HotSpringPotionEffectInfo>(hotSpringPotionEffectInfos);
			this.TownID = townID;
		}
	}
}
