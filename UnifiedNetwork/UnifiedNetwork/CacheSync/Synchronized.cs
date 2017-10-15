using System;

namespace UnifiedNetwork.CacheSync
{
	[Serializable]
	internal sealed class Synchronized
	{
		public int ID { get; set; }

		public Synchronized(int id)
		{
			this.ID = id;
		}

		public Synchronized()
		{
		}
	}
}
