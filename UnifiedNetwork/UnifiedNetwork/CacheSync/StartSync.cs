using System;

namespace UnifiedNetwork.CacheSync
{
	[Serializable]
	internal sealed class StartSync
	{
		public int ID { get; set; }

		public StartSync(int id)
		{
			this.ID = id;
		}

		public StartSync()
		{
		}
	}
}
