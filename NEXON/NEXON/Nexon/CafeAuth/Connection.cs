using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Threading;
using Nexon.CafeAuth.Packets;
using Utility;

namespace Nexon.CafeAuth
{
	public class Connection : IDisposable
	{
		public Connection(GameSN gameSN)
		{
			this.connection.ConnectionSucceed += this.OnConnectionSucceeded;
			this.connection.ConnectionFail += this.OnConnectionFail;
			this.connection.Disconnected += this.OnDisconnected;
			this.connection.ExceptionOccur += this.OnExceptionOccur;
			this.connection.PacketReceive += this.connection_PacketReceive;
			this.gameSN = gameSN;
		}

		public bool Connected
		{
			get
			{
				return this.connection.Connected;
			}
		}

		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.connection.RemoteEndPoint;
			}
		}

		public event EventHandler<EventArgs> ConnectionSucceeded;

		public event EventHandler<EventArgs<Exception>> ConnectionFail;

		public event EventHandler<EventArgs> Disconnected;

		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		public event Action<Initialize> InitializeSent;

		public event Action<InitializeResponse> InitializeResponsed;

		public event Action<MessageRequest> MessageReceived;

		public event Action<SynchronizeRequest> SynchronizeRequested;

		public event Action<TerminateRequest> TerminateRequested;

		public event Action<LoginResponse> LoginRecoveryRequested;

		public event Action<string> RetryLoginRequested;

		public void Dispose()
		{
			if (this.connection != null)
			{
				this.connection.ConnectionSucceed -= this.OnConnectionSucceeded;
				this.connection.ConnectionFail -= this.OnConnectionFail;
				this.connection.Disconnected -= this.OnDisconnected;
				this.connection.ExceptionOccur -= this.OnExceptionOccur;
				this.connection.PacketReceive -= this.connection_PacketReceive;
				if (this.connection.Connected)
				{
					this.connection.Disconnect();
				}
				this.connection = null;
			}
		}

		public void Connect(JobProcessor thread, IPEndPoint endPoint, byte domainSN, string domainName)
		{
			this.thread = thread;
			this.domainSN = domainSN;
			this.domainName = domainName;
			this.connection.Connect(this.thread, endPoint.Address, endPoint.Port, new PacketAnalyzer());
		}

		private void OnConnectionSucceeded(object sender, EventArgs e)
		{
			this.RequestInitialize();
			if (this.ConnectionSucceeded != null)
			{
				this.ConnectionSucceeded(sender, e);
			}
		}

		private void OnConnectionFail(object sender, EventArgs<Exception> e)
		{
			if (this.ConnectionFail != null)
			{
				this.ConnectionFail(sender, e);
			}
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			this.queries.Clear();
			this.lostQueries.Clear();
			if (this.Disconnected != null)
			{
				this.Disconnected(sender, e);
			}
		}

		private void OnExceptionOccur(object sender, EventArgs<Exception> e)
		{
			if (this.ExceptionOccur != null)
			{
				this.ExceptionOccur(sender, e);
			}
		}

		private void connection_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			switch (packet.PacketType)
			{
			case PacketType.Initialize:
			{
				InitializeResponse arg = new InitializeResponse(ref packet);
				this.thread.Enqueue(Job.Create<InitializeResponse>(new Action<InitializeResponse>(this.ProcessInitialize), arg));
				return;
			}
			case PacketType.Login:
			{
				LoginResponse arg2 = new LoginResponse(ref packet);
				this.thread.Enqueue(Job.Create<LoginResponse>(new Action<LoginResponse>(this.ProcessLogin), arg2));
				return;
			}
			case PacketType.Logout:
				break;
			case PacketType.Terminate:
			{
				TerminateRequest arg3 = new TerminateRequest(ref packet);
				this.thread.Enqueue(Job.Create<TerminateRequest>(new Action<TerminateRequest>(this.ProcessTerminate), arg3));
				break;
			}
			case PacketType.Message:
			{
				MessageRequest arg4 = new MessageRequest(ref packet);
				this.thread.Enqueue(Job.Create<MessageRequest>(new Action<MessageRequest>(this.ProcessMessage), arg4));
				return;
			}
			case PacketType.Synchronize:
			{
				SynchronizeRequest arg5 = new SynchronizeRequest(ref packet);
				this.thread.Enqueue(Job.Create<SynchronizeRequest>(new Action<SynchronizeRequest>(this.ProcessSync), arg5));
				return;
			}
			default:
				return;
			}
		}

		private void Transmit(Packet packet)
		{
			this.connection.Transmit<Packet>(packet);
		}

		private AsyncResult Transmit(Packet packet, string nexonID, string characterID, AsyncCallback callback, object state)
		{
			AsyncResult asyncResult = new AsyncResult(nexonID, characterID, callback, state);
			if (!this.Connected || this.queries.ContainsKey(asyncResult.NexonID))
			{
				if (!this.Connected)
				{
					Log<Connection>.Logger.Info("Discard Login Packet : Not Connected");
				}
				else
				{
					Log<Connection>.Logger.InfoFormat("Duplicate Login [ nxid = {0} ]", nexonID);
				}
				asyncResult.Complete(true);
				return asyncResult;
			}
			this.queries.Add(asyncResult.NexonID, asyncResult);
			this.Transmit(packet);
			Scheduler.Schedule(this.thread, Job.Create<string>(new Action<string>(this.TerminateLogin), nexonID), this.ackTimeoutLimit);
			Log<Connection>.Logger.InfoFormat("TryLogin [ nxid = {0} ]", nexonID);
			return asyncResult;
		}

		public void RequestInitialize()
		{
			this.thread.Enqueue(Job.Create(new Action(this.Initialize)));
		}

		private void ProcessInitialize(InitializeResponse response)
		{
			if (response.Result == InitializeResult.Success)
			{
				Scheduler.Schedule(this.thread, Job.Create(new Action(this.Heartbeat)), new TimeSpan(0, 0, 30));
			}
			if (this.InitializeResponsed != null)
			{
				this.InitializeResponsed(response);
			}
		}

		private void ProcessLogin(LoginResponse response)
		{
			AsyncResult asyncResult;
			if (this.queries.TryGetValue(response.NexonID, out asyncResult))
			{
				Log<Connection>.Logger.InfoFormat("Login for [ nxid = {0} sessionNo = {1} ]", response.NexonID, response.SessionNo);
				this.queries.Remove(response.NexonID);
				asyncResult.Response = response;
				asyncResult.Complete();
				return;
			}
			if (this.lostQueries.Contains(response.NexonID))
			{
				Log<Connection>.Logger.InfoFormat("LateLogin for [ nxid = {0} sessionNo = {1} ]", response.NexonID, response.SessionNo);
				this.lostQueries.Remove(response.NexonID);
				this.LoginRecoveryRequested(response);
				return;
			}
			Log<Connection>.Logger.InfoFormat("Discarded Login for [ nxid = {0} sessionNo = {1} ]", response.NexonID, response.SessionNo);
		}

		private void TerminateLogin(string nxID)
		{
			AsyncResult asyncResult;
			if (this.queries.TryGetValue(nxID, out asyncResult))
			{
				Log<Connection>.Logger.InfoFormat("Login Timeout for [ nxid = {0} ]", nxID);
				this.queries.Remove(nxID);
				this.lostQueries.Add(nxID);
				Scheduler.Schedule(this.thread, Job.Create<string>(new Action<string>(this.RetryLogin), nxID), this.retryLoginLimit);
				asyncResult.Response = null;
				asyncResult.Complete();
			}
		}

		private void RetryLogin(string nxID)
		{
			if (this.lostQueries.Contains(nxID))
			{
				this.lostQueries.Remove(nxID);
				if (this.RetryLoginRequested != null)
				{
					this.RetryLoginRequested(nxID);
				}
			}
		}

		private void ProcessMessage(MessageRequest request)
		{
			if (this.MessageReceived != null)
			{
				this.MessageReceived(request);
			}
		}

		private void ProcessSync(SynchronizeRequest request)
		{
			if (this.SynchronizeRequested != null)
			{
				this.SynchronizeRequested(request);
			}
		}

		private void ProcessTerminate(TerminateRequest request)
		{
			if (this.TerminateRequested != null)
			{
				this.TerminateRequested(request);
			}
		}

		private void Initialize()
		{
			Initialize initialize = new Initialize(this.gameSN, this.domainSN, this.domainName);
			this.Transmit(initialize.Serialize());
			if (this.InitializeSent != null)
			{
				this.InitializeSent(initialize);
			}
		}

		private void Heartbeat()
		{
			Alive alive = new Alive();
			this.Transmit(alive.Serialize());
			Scheduler.Schedule(this.thread, Job.Create(new Action(this.Heartbeat)), new TimeSpan(0, 0, 30));
		}

		public IAsyncResult BeginLogin(string nexonID, string characterID, IPAddress localAddress, IPAddress remoteAddress, bool canTry, bool isTrial, MachineID machineID, ICollection<int> gameRoomClients, AsyncCallback callback, object state)
		{
			Login login = new Login(nexonID, characterID, localAddress, remoteAddress, canTry, isTrial, machineID, gameRoomClients);
			return this.Transmit(login.Serialize(), nexonID, characterID, callback, state);
		}

		public LoginResponse EndLogin(IAsyncResult asyncResult)
		{
			if (!(asyncResult is AsyncResult))
			{
				return null;
			}
			AsyncResult asyncResult2 = (AsyncResult)asyncResult;
			return asyncResult2.Response as LoginResponse;
		}

		public void Logout(long SID, string nexonID, string characterID, IPAddress remoteAddress, bool canTry)
		{
			Logout logout = new Logout(SID, nexonID, characterID, remoteAddress, canTry);
			this.Transmit(logout.Serialize());
			AsyncResult asyncResult;
			if (this.queries.TryGetValue(nexonID, out asyncResult))
			{
				this.queries.Remove(nexonID);
				asyncResult.Response = null;
				asyncResult.Complete();
			}
		}

		public void SynchronizeReply(byte isMonitering, Dictionary<long, byte> result)
		{
			SynchronizeResult synchronizeResult = new SynchronizeResult(isMonitering, result);
			this.Transmit(synchronizeResult.Serialize());
		}

		private TcpClient connection = new TcpClient();

		private JobProcessor thread;

		public GameSN gameSN;

		private byte domainSN;

		private string domainName;

		private IDictionary<string, AsyncResult> queries = new Dictionary<string, AsyncResult>();

		private ICollection<string> lostQueries = new HashSet<string>();

		internal readonly int ackTimeoutLimit = 10000;

		internal readonly int retryLoginLimit = 600000;
	}
}
