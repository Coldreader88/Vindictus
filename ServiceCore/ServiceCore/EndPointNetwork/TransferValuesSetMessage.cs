using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TransferValuesSetMessage : IMessage
	{
		public long CID { get; set; }

		public string Command { get; set; }

		public IDictionary<string, string> Values { get; set; }
	}
}
