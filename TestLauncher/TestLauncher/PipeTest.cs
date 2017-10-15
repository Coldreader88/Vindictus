using System;
using System.Net;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Threading;
using UnifiedNetwork.PipedNetwork;

namespace TestLauncher
{
	internal class PipeTest
	{
		public static void Start()
		{
			PipeTest.suite = new PipeTest();
			PipeTest.suite.thread = new JobProcessor();
			PipeTest.suite.thread.ExceptionOccur += delegate(object sender, EventArgs<Exception> e)
			{
				Console.Error.WriteLine(e.Value);
			};
			PipeTest.suite.thread.Start();
			PipeTest.suite.acceptor = new Acceptor(PipeTest.suite.thread);
			PipeTest.suite.acceptor.OnAccepted += PipeTest.suite.acceptor_OnAccepted;
			PipeTest.suite.acceptor.Start(42);
			PipeTest.suite.acceptor.RegisterPipeMessageGroup(PipeTestMessages.TypeConverters);
			PipeTest.suite.connector = Peer.CreateClient(PipeTest.suite.acceptor, new TcpClient());
			PipeTest.suite.connector.Connected += PipeTest.suite.connector_Connected;
			PipeTest.suite.connector.Connect(new IPEndPoint(IPAddress.Loopback, 42));
		}

		private void connector_Connected(Peer obj)
		{
			Pipe pipe = obj.InitPipe();
			if (pipe.ID % 100 == 1)
			{
				Console.WriteLine("{0} pipe open...", pipe.ID);
			}
			Scheduler.Schedule(this.thread, Job.Create<Pipe, int>(new Action<Pipe, int>(this.Send), pipe, 0), 1);
			Scheduler.Schedule(this.thread, Job.Create<Peer>(new Action<Peer>(this.connector_Connected), obj), 1);
		}

		private void acceptor_OnAccepted(Peer peer)
		{
			Console.WriteLine("peer accepted...");
			peer.PipeOpen += this.peer_PipeOpen;
		}

		private void Send(Pipe pipe, int var)
		{
			pipe.Send<IntMessage>(new IntMessage
			{
				Value = var
			});
			if (pipe.ID % 100 == 1 && var % 100 == 0)
			{
				Console.WriteLine("{1} send {0}...", var, pipe.ID);
			}
			if (var == 300)
			{
				pipe.SendClose();
				pipe.Close();
				return;
			}
			Scheduler.Schedule(this.thread, Job.Create<Pipe, int>(new Action<Pipe, int>(this.Send), pipe, var + 1), 1);
		}

		private void peer_PipeOpen(Peer peer, Pipe pipe)
		{
			int var = 0;
			pipe.MessageReceived += delegate(object msg)
			{
				IntMessage intMessage = msg as IntMessage;
				if (pipe.ID % 100 == 1 && intMessage.Value % 100 == 0)
				{
					Console.WriteLine("{1} recv {0}...", var, pipe.ID);
				}
				if (intMessage.Value != var)
				{
					Console.Error.WriteLine("var not match : {0} != {1}", intMessage.Value, var);
				}
				var = intMessage.Value + 1;
			};
		}

		private JobProcessor thread;

		private Acceptor acceptor;

		private Peer connector;

		private static PipeTest suite;
	}
}
