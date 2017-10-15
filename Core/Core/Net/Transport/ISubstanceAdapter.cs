using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	public interface ISubstanceAdapter
	{
		IEnumerable<Packet> InstantAppearMessage { get; }

		IEnumerable<Packet> InstantDisappearMessage { get; }
	}
}
