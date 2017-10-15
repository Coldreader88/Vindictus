using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RelayServiceOperations
{
	[Serializable]
	public sealed class Transmit : Operation
	{
		public long? FrontendID { get; set; }

		public ICollection<Packet> Packets { get; private set; }

		public Transmit(ICollection<Packet> packets)
		{
			this.Packets = packets;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
