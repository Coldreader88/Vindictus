using System;
using System.Net.Sockets;

namespace Devcat.Core.Net
{
	public class TcpClient2 : TcpClient
	{
		protected override IAsyncSocket CreateAsyncSocket(Socket socket, IPacketAnalyzer packetAnalyzer)
		{
			return new AsyncSocket2(socket, packetAnalyzer);
		}

		public static void Init()
		{
			AsyncSocket2.Init();
		}
	}
}
