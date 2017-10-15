using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TransferValuesResultMessage : IMessage
	{
		public long CID { get; set; }

		public string CommandResult { get; set; }

		public IDictionary<string, string> ResultValues { get; set; }
	}
}
