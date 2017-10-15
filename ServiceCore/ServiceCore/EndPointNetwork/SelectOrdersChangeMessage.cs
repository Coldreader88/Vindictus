using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SelectOrdersChangeMessage : IMessage
	{
		public ICollection<byte> SelectOrders { get; set; }
	}
}
