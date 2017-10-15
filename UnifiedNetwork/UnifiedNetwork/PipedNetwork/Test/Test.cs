using System;
using System.Net;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using Utility;

namespace UnifiedNetwork.PipedNetwork.Test
{
	public class Test
	{
		public Test()
		{
			this.thread = new JobProcessor();
			this.thread.Start();
			this.host1 = new Acceptor(this.thread);
			this.host2 = new Acceptor(this.thread);
			this.host2.OnAccepted += delegate(Peer peer)
			{
				peer.PipeOpen += delegate(Peer _, Pipe pipe)
				{
					pipe.PacketReceived += delegate(Packet packet)
					{
						Log<Test>.Logger.DebugFormat("receive {0}", packet.Length);
					};
				};
			};
			this.host1.Start(10000);
			this.host2.Start(10001);
			Peer peer2 = Peer.CreateClient(this.host1, new TcpClient());
			peer2.Connected += delegate(Peer peer)
			{
				peer.InitPipe().SendPacket(SerializeWriter.ToBinary<Test.TestMessage>(new Test.TestMessage
				{
					Y = "asd"
				}));
			};
			peer2.Connect(new IPEndPoint(IPAddress.Loopback, 10001));
		}

		private JobProcessor thread;

		private Acceptor host1;

		private Acceptor host2;

		[Serializable]
		private sealed class TestMessage
		{
			public int X { get; set; }

			public string Y { get; set; }
		}
	}
}
