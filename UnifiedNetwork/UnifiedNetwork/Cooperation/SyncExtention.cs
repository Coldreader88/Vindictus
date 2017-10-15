using System;

namespace UnifiedNetwork.Cooperation
{
	public static class SyncExtention
	{
		public static void Sync(this ISynchronizable sync, Action<ISynchronizable> callback)
		{
			sync.OnFinished += callback;
			sync.OnSync();
		}
	}
}
