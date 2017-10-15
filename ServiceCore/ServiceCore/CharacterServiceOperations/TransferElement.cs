using System;
using System.Collections.Generic;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class TransferElement
	{
		public long CID { get; private set; }

		public IDictionary<string, string> OuterValues { get; set; }

		public TransferElement(long cid)
		{
			this.CID = cid;
			this.OuterValues = new Dictionary<string, string>();
			this.InnerValues = new Dictionary<string, object>();
		}

		public IDictionary<string, object> InnerValues;
	}
}
