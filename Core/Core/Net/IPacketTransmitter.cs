using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net
{
	public interface IPacketTransmitter : ITransmitter<Packet>, ITransmitter<IEnumerable<Packet>>
	{
	}
}
