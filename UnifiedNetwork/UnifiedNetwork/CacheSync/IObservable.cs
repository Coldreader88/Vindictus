using System;

namespace UnifiedNetwork.CacheSync
{
	public interface IObservable
	{
		ObservableIdentifier ID { get; }

		void Updated();

		void Close();

		event EventHandler CacheUpdated;
	}
}
