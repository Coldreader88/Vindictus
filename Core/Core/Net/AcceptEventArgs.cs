using System;
using Devcat.Core.Threading;

namespace Devcat.Core.Net
{
	[Serializable]
	public sealed class AcceptEventArgs : EventArgs
	{
		public TcpClient Client { get; private set; }

		public JobProcessor JobProcessor { get; set; }

		public IPacketAnalyzer PacketAnalyzer { get; set; }

		public bool Cancel { get; set; }

		public AcceptEventArgs(TcpClient tcpClient)
		{
			this.Client = tcpClient;
		}
	}
}
