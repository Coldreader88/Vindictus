using System;

namespace UnifiedNetwork.Entity.Operations
{
	[Serializable]
	public sealed class Identify
	{
		public long ID { get; set; }

		public string Category { get; set; }
	}
}
