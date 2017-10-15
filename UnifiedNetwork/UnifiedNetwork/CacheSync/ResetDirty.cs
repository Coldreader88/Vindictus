using System;

namespace UnifiedNetwork.CacheSync
{
	[Serializable]
	internal sealed class ResetDirty
	{
		public int ID { get; set; }

		public ResetDirty(int id)
		{
			this.ID = id;
		}

		public ResetDirty()
		{
		}
	}
}
