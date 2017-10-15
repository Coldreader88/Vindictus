using System;
using System.Net;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;

namespace TestLauncher
{
	internal class TransmitTest
	{
		public static void Start()
		{
			TransmitTest.suite = new TransmitTest();
			TransmitTest.suite.thread = new JobProcessor();
			TransmitTest.suite.thread.Start();
			TransmitTest.suite.server = new TcpServer();
			TransmitTest.suite.server.ClientAccept += TransmitTest.server_ClientAccept;
			TransmitTest.suite.server.Start(TransmitTest.suite.thread, 42);
			TransmitTest.suite.client = new TcpClient();
			TransmitTest.suite.client.PacketReceive += TransmitTest.client_PacketReceive;
			TransmitTest.suite.client.Tag = 0;
			TransmitTest.suite.client.Connect(TransmitTest.suite.thread, IPAddress.Loopback, 42, new MessageAnalyzer());
		}

		private static void Send(JobProcessor thread, TcpClient client)
		{
			Packet packet = new Packet(new ArraySegment<byte>(new byte[10000]));
			int num = (int)client.Tag;
			packet.InstanceId = (long)num;
			client.Transmit(packet);
			client.Tag = num + 1;
			if (num % 100 == 0)
			{
				Console.WriteLine("Packet {0} transmit...", num);
			}
			Scheduler.Schedule(thread, Job.Create<JobProcessor, TcpClient>(new Action<JobProcessor, TcpClient>(TransmitTest.Send), thread, client), 0);
		}

		private static void server_ClientAccept(object sender, AcceptEventArgs e)
		{
			e.PacketAnalyzer = new MessageAnalyzer();
			e.Client.Tag = 0;
			e.Client.PacketReceive += TransmitTest.client_PacketReceive;
			Console.WriteLine("client accepted");
			TransmitTest.suite.thread.Enqueue(Job.Create<JobProcessor, TcpClient>(new Action<JobProcessor, TcpClient>(TransmitTest.Send), TransmitTest.suite.thread, e.Client));
		}

		private static void client_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			TcpClient tcpClient = sender as TcpClient;
			Packet packet = new Packet(e.Value);
			int num = (int)packet.InstanceId;
			if (num % 100 == 0)
			{
				Console.WriteLine("Packet {0} received...", num);
			}
			if (num != (int)tcpClient.Tag)
			{
				Console.Error.WriteLine("value not match : {0} != {1}", num, tcpClient.Tag);
			}
			tcpClient.Tag = num + 1;
		}

		private TcpClient client;

		private TcpServer server;

		private JobProcessor thread;

		private static TransmitTest suite;
	}
}
