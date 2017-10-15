using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class DestroySystemItems : Operation
	{
		public ICollection<long> ItemIDs { get; set; }

		public DestroySystemItems(ICollection<long> itemIDs)
		{
			this.ItemIDs = itemIDs;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
