using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class SpiritInjectionItem : Operation
	{
		public long SpritStoneID { get; set; }

		public long TargetItemID { get; set; }

		public SpiritInjectionItem(long spritStoneID, long targetItemID)
		{
			this.SpritStoneID = spritStoneID;
			this.TargetItemID = targetItemID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
