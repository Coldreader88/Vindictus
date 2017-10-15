using System;

namespace UnifiedNetwork.CacheSync
{
	[Serializable]
	internal sealed class AddOk
	{
		public ObservableIdentifier ObservableID { get; set; }

		public int ProxyID { get; set; }

		public AddOk(ObservableIdentifier oid, int pid)
		{
			this.ObservableID = oid;
			this.ProxyID = pid;
		}

		public AddOk()
		{
		}
	}
}
