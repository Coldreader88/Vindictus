using System;

namespace UnifiedNetwork.CacheSync
{
	[Serializable]
	internal sealed class AddObserver
	{
		public ObservableIdentifier ID { get; set; }

		public AddObserver(ObservableIdentifier id)
		{
			this.ID = id;
		}

		public AddObserver()
		{
		}
	}
}
