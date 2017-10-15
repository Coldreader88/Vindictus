using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class ArrangeGuildStorageItem : Operation
	{
		public long RequestingCID { get; set; }

		public long ItemID { get; set; }

		public int Slot { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
