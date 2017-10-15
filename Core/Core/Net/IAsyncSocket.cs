using System;
using System.Collections.Generic;

namespace Devcat.Core.Net
{
	public interface IAsyncSocket
	{
		event EventHandler<EventArgs<ArraySegment<byte>>> PacketReceive;

		event EventHandler<EventArgs> SocketClose;

		event EventHandler<EventArgs<Exception>> SocketException;

		long TotalSent { get; }

		int TotalSentCount { get; }

		long TotalReceived { get; }

		int TotalReceivedCount { get; }

		bool Connected { get; }

		byte[] RemoteAddress { get; }

		int RemotePort { get; }

		void Activate();

		void Shutdown();

		void Send(ArraySegment<byte> data);

		void Send(IEnumerable<ArraySegment<byte>> dataList);
	}
}
