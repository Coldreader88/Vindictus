using System;

namespace UnifiedNetwork.Cooperation
{
	public interface ISynchronizable
	{
		void OnSync();

		event Action<ISynchronizable> OnFinished;

		bool Result { get; }
	}
}
