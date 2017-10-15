using System;

namespace UnifiedNetwork.CacheSync
{
	[Serializable]
	internal sealed class SetDirty
	{
		public int ID { get; set; }

		public SetDirty(int id)
		{
			this.ID = id;
		}

		public SetDirty()
		{
		}
	}
}
