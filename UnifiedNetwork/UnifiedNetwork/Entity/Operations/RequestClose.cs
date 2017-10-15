using System;

namespace UnifiedNetwork.Entity.Operations
{
	[Serializable]
	public sealed class RequestClose
	{
		public bool IsForced { get; set; }
	}
}
