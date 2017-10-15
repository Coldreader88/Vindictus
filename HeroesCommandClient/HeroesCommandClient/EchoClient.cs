using System;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Collections.Concurrent;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using HeroesCommandClient.Properties;

namespace HeroesCommandClient
{
	internal class EchoClient
	{
		public bool AutoReconnect { get; set; }

		public event EventHandler<EventArgs> ConnectionSucceed;

		public event EventHandler<EventArgs<Exception>> ConnectionFailed;

		public event EventHandler<EventArgs> Disconnected;

		public EchoClient()
		{
			this.tcpClient = new TcpClient2();
			this.thread = new JobProcessor();
			this.tcpClient.ConnectionSucceed += this.OnConnectionSucceed;
			this.tcpClient.ConnectionFail += this.OnConnectionFailed;
			this.tcpClient.ConnectionFail += delegate(object sender, EventArgs<Exception> e)
			{
			};
			this.tcpClient.Disconnected += this.OnDisconnected;
			this.logQueue = new ConcurrentQueue<string>();
		}

		public void Start()
		{
			this.thread.Start();
			this.Connect();
			this.active = true;
		}

		private void Connect()
		{
			if (!this.tcpClient.Connected)
			{
				this.tcpClient.Connect(this.thread, Settings.Default.EchoServerIP, Settings.Default.EchoServerPort, new MessageAnalyzer());
			}
		}

		public void Reconnect()
		{
			Thread.Sleep(1000);
			this.Connect();
		}

		public void Stop()
		{
			if (this.active)
			{
				this.cleaning = true;
				this.tcpClient.Disconnect();
				this.active = false;
				this.thread.Stop();
				return;
			}
			throw new InvalidOperationException("Already stopped");
		}

		private void OnConnectionSucceed(object sender, EventArgs e)
		{
			if (this.ConnectionSucceed != null)
			{
				this.ConnectionSucceed(this, e);
			}
			this.firstConnected = true;
			this.SendLog();
		}

		private void OnConnectionFailed(object sender, EventArgs<Exception> e)
		{
			if (this.ConnectionFailed != null)
			{
				this.ConnectionFailed(this, e);
			}
			if (this.firstConnected && this.AutoReconnect)
			{
				Scheduler.Schedule(this.thread, Job.Create(new Action(this.Reconnect)), 10000);
			}
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			if (!this.cleaning)
			{
				if (this.Disconnected != null)
				{
					this.Disconnected(this, e);
				}
				if (this.AutoReconnect)
				{
					Scheduler.Schedule(this.thread, Job.Create(new Action(this.Reconnect)), 10000);
					return;
				}
			}
			else
			{
				this.cleaning = false;
			}
		}

		public void SendLog(string key, string title, string content)
		{
			this.logQueue.Enqueue(string.Format("{0}||{1}||{2}", key, title, content));
			Thread.Sleep(0);
			if (this.logQueue.Count > 0)
			{
				this.thread.Enqueue(Job.Create(new Action(this.SendLog)));
			}
		}

		private void SendLog()
		{
			string value;
			while (this.tcpClient.Connected && this.logQueue.TryDequeue(out value))
			{
				this.tcpClient.Transmit(SerializeWriter.ToBinary<string>(value));
			}
		}

		private const int ReconnectInterval = 10000;

		private TcpClient tcpClient;

		private JobProcessor thread;

		private bool cleaning;

		private bool active;

		private bool firstConnected;

		private ConcurrentQueue<string> logQueue;
	}
}
