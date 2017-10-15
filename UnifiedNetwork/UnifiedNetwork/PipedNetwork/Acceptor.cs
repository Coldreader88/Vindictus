using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using Utility;

namespace UnifiedNetwork.PipedNetwork
{
	public class Acceptor
	{
		internal JobProcessor Thread { get; private set; }

		internal MessageHandlerFactory MessageHandler { get; private set; }

		internal MessageHandlerFactory PipeMessageHandler { get; private set; }

		public IPEndPoint EndPointAddress
		{
			get
			{
				return this.acceptor.LocalEndPoint;
			}
		}

		public event Action<Peer> OnAccepted;

		public Acceptor(JobProcessor thread) : this(new TcpServer(), thread)
		{
		}

		public Acceptor(ITcpServer tcpServer, JobProcessor thread)
		{
			this.acceptor = tcpServer;
			this.Thread = thread;
			this.MessageHandler = new MessageHandlerFactory();
			this.PipeMessageHandler = new MessageHandlerFactory();
			this.RegisterNonPipeMessageGroup(Message.TypeConverters);
			this.acceptor.ClientAccept += this.acceptor_ClientAccept;
			this.acceptor.ExceptionOccur += this.acceptor_ExceptionOccur;
			this.acceptor.DoOnHugeMessage = delegate(int size, string msg)
			{
				string log = string.Format("<HugeMessage> [{2}]Service Acceptor get huge size packet {0} :{1}", size, msg, this.EndPointAddress);
				try
				{
					FileLog.Log("HugeMessage.log", log);
				}
				catch (Exception)
				{
				}
			};
			this.acceptor.WriteLogFunc = delegate(string msg)
			{
				string log = string.Format("[Acceptor][{0}]{1}", this.acceptor.LocalEndPoint, msg);
				try
				{
					FileLog.Log("Peer.log", log);
				}
				catch (Exception)
				{
				}
			};
		}

		public void Start(int port)
		{
			this.acceptor.Start(this.Thread, port);
		}

		public void Stop()
		{
			this.acceptor.Stop();
		}

		public void RegisterNonPipeMessageGroup(IDictionary<Type, int> typeConverters)
		{
			this.MessageHandler.Register<Peer>(typeConverters, "ProcessNonPipeMessage");
		}

		public void RegisterPipeMessageGroup(IDictionary<Type, int> typeConverters)
		{
			this.PipeMessageHandler.Register<Pipe>(typeConverters, "ProcessMessage");
		}

		private void acceptor_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			Log<Acceptor>.Logger.Error("exception occured in PipedNetwork.Acceptor", e.Value);
		}

		private void acceptor_ClientAccept(object sender, AcceptEventArgs e)
		{
			e.PacketAnalyzer = new MessageAnalyzer();
			e.JobProcessor = this.Thread;
			Peer obj = Peer.CreateFromServer(this, e.Client);
			if (this.OnAccepted != null)
			{
				this.OnAccepted(obj);
			}
		}

		private ITcpServer acceptor;
	}
}
