using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class HotSpringAddPotion : Operation
	{
		public int Channel { get; set; }

		public int TownID { get; set; }

		public long ItemID { get; set; }

		public HotSpringAddPotion(int channel, int townID, long itemID)
		{
			this.Channel = channel;
			this.TownID = townID;
			this.ItemID = itemID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
