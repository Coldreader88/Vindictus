using System;
using System.Collections.Generic;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace GuildService.Processor
{
	internal class UpdateGuildInventoryProcessor : EntityProcessor<UpdateGuildInventory, GuildEntity>
	{
		public UpdateGuildInventoryProcessor(GuildService service, UpdateGuildInventory op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			base.Entity.Storage.StorageCount = base.Operation.Status.SlotCount;
			base.Entity.Storage.ApplyStorage(base.Operation.Status.Inventory);
			base.Entity.Storage.BroadCastInventoryInfo();
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}

		private GuildService service;
	}
}
