using System;

namespace UnifiedNetwork.Entity.Operations
{
	[Serializable]
	internal sealed class BindEntity
	{
		public long ID { get; set; }

		public string Category { get; set; }

		public bool OwnerConnection { get; set; }
	}
}
