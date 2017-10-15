using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MMOChannelServiceOperations
{
	[Serializable]
	public sealed class ChannelHotSpringInfo : Operation
	{
		public long CID { get; set; }

		public ICollection<HotSpringPotionEffectInfo> HotSpringPotionEffectInfos { get; set; }

		public int TownID { get; set; }

		public ChannelHotSpringInfo(long cid, ICollection<HotSpringPotionEffectInfo> hotSpringPotionEffectInfos, int townID)
		{
			this.CID = cid;
			this.HotSpringPotionEffectInfos = hotSpringPotionEffectInfos;
			this.TownID = townID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
