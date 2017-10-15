using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TransferValues
	{
		public long CID { get; private set; }

		public IDictionary<string, string> Values { get; set; }

		public TransferValues(long cid, IDictionary<string, string> vals)
		{
			this.CID = cid;
			this.Values = vals;
		}
	}
}
