using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class UseExtraSharedStorage : Operation
	{
		public long ItemID { get; set; }

		public byte StorageID { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
