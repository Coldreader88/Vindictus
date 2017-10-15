using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class HotSpringRequestPotionEffect : Operation
	{
		public int Channel { get; set; }

		public int TownID { get; set; }

		public string PotionItemClass { get; set; }

		public HotSpringRequestPotionEffect(int channel, int townID, string potionItemClass)
		{
			this.Channel = channel;
			this.TownID = townID;
			this.PotionItemClass = potionItemClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
