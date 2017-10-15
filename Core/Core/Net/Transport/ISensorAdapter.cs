using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	[Obsolete("Obsoleted. You don't need to use this. Directly use IPacketTransmitter.")]
	public interface ISensorAdapter : IPacketTransmitter, ITransmitter<Packet>, ITransmitter<IEnumerable<Packet>>
	{
	}
}
