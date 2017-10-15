using System;
using UnifiedNetwork.Entity;

namespace UnifiedNetwork.CacheSync
{
	public interface IObserver
	{
		event Action<IObserver, Action<IObserver>> SyncBegun;

		event Action<IObserver, Action<IObserver>> SetDirty;

		void Close();

		bool IsDirty { get; }

		object Cache { get; set; }

		IEntityProxy Connection { get; }

		void ForceSync(Action<IObserver> callback);

		void EndSync(bool success);
	}
}
