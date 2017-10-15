using System;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;

namespace RemoteControlSystem.Client
{
	public class ControlClient
	{
		public ControlClient()
		{
			this.disconnecting = false;
			this.errorLogConnectFail = false;
		}

		public void Start(JobProcessor jobProcessor)
		{
			if (this.active)
			{
				return;
			}
			this.disconnecting = false;
			this.active = true;
			this.thread = jobProcessor;
			this.tcpClient = new TcpClient();
			this.tcpClient.ConnectionSucceed += this.OnConnect;
			this.tcpClient.ConnectionFail += this.OnConnectFail;
			this.tcpClient.Disconnected += this.OnClose;
			this.Connect();
		}

		public void Connect()
		{
			if (!this.tcpClient.Connected)
			{
				this.tcpClient.Connect(this.thread, Base.ServerIPAddress, Base.ServerPort, new MessageAnalyzer());
			}
		}

		public void Reconnect()
		{
			Thread.Sleep(1000);
			this.Connect();
		}

		public void Stop()
		{
			if (!this.active)
			{
				return;
			}
			this.disconnecting = true;
			this.active = false;
			try
			{
				this.tcpClient.Disconnect();
			}
			catch (Exception)
			{
			}
		}

		public void CloseOpp()
		{
			this.Stop();
		}

		protected void OnConnect(object sender, EventArgs args)
		{
			if (this.errorLogConnectFail)
			{
				Log<RCClientService>.Logger.InfoFormat("Connected to RC Server where {0} : {1}", Base.ServerIPAddress, Base.ServerPort);
			}
			this.errorLogConnectFail = false;
			Base.ClientControlManager.RegisterClient(this.tcpClient);
		}

		protected void OnConnectFail(object sender, EventArgs<Exception> args)
		{
			if (this.disconnecting)
			{
				return;
			}
			if (!this.errorLogConnectFail)
			{
				this.errorLogConnectFail = true;
				Log<RCClientService>.Logger.ErrorFormat("Connection closed from RC Server {0} {1}.", Base.ServerIPAddress, Base.ServerPort, args.Value.ToString());
			}
			this.thread.Enqueue(Job.Create(new Action(this.Reconnect)));
		}

		protected void OnClose(object sender, EventArgs args)
		{
			if (Base.ClientControlManager != null)
			{
				Base.ClientControlManager.UnregisterClient(this.tcpClient);
			}
			if (this.disconnecting)
			{
				this.disconnecting = false;
				this.errorLogConnectFail = false;
				return;
			}
			Log<RCClientService>.Logger.Error("Connection closed from RC Server.");
			this.thread.Enqueue(Job.Create(new Action(this.Reconnect)));
		}

		private bool disconnecting;

		private bool errorLogConnectFail;

		private bool active;

		private TcpClient tcpClient;

		private JobProcessor thread;
	}
}
