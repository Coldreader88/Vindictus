using System;

namespace UnifiedNetwork.CacheSync
{
	[Serializable]
	internal sealed class AddFail
	{
		public ObservableIdentifier ID { get; set; }

		public AddFail(ObservableIdentifier id)
		{
			this.ID = id;
		}

		public AddFail()
		{
		}
	}
}
