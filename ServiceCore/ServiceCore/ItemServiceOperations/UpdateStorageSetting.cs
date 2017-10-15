using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class UpdateStorageSetting : Operation
	{
		public int StorageNo { get; set; }

		public string StorageName { get; set; }

		public int StorageTag { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
