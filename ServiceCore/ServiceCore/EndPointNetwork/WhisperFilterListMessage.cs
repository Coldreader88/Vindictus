using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class WhisperFilterListMessage : IMessage
	{
		public IDictionary<string, int> Filter { get; set; }
	}
}
