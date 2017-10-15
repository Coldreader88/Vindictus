using System;
using ServiceCore.ItemServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class UpdateGuildInventory : Operation
	{
		public GuildInventoryStatus Status { get; set; }

		public UpdateGuildInventory(GuildInventoryStatus status)
		{
			this.Status = status;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
