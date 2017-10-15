using System;
using System.Net;
using AdminClientServiceCore.Messages;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;

namespace HeroesCommandClient
{
	internal class HeroesAdminPeer
	{
		public event EventHandler<EventArgs> ConnectionSucceed;

		public event EventHandler<EventArgs<Exception>> ConnectionFailed;

		public event EventHandler<EventArgs> Disconnected;

		public event EventHandler<EventArgs<AdminReportClientCountMessage2>> UserCounted;

		public event EventHandler<EventArgs<AdminReportNotifyMessage>> Notified;

		public string Name { get; private set; }

		public IPEndPoint IP { get; private set; }

		public bool AutoReconnect { get; set; }

		public bool IsConnected
		{
			get
			{
				return this.client.Connected;
			}
		}

		static HeroesAdminPeer()
		{
			HeroesAdminPeer.MF.Register<HeroesAdminPeer>(AdminClientServiceOperationMessages.TypeConverters, "ProcessMessage");
		}

		public HeroesAdminPeer(string name, IPEndPoint ip)
		{
			this.Name = name;
			this.IP = ip;
			this.client = new TcpClient2();
			this.client.ConnectionSucceed += this.OnConnectionSucceed;
			this.client.ConnectionFail += this.OnConnectionFailed;
			this.client.Disconnected += this.OnDisconnected;
			this.client.PacketReceive += this.OnPacketReceive;
			this.cleaning = false;
			this.active = false;
		}

		public void Start()
		{
			this.Connect();
			this.active = true;
		}

		public void Stop()
		{
			if (this.active)
			{
				this.cleaning = true;
				this.firstConnected = false;
				this.client.Disconnect();
				return;
			}
			throw new InvalidOperationException("Already stopped");
		}

		private void Connect()
		{
			if (!this.client.Connected)
			{
				this.client.Connect(HeroesCommandBridge.Thread, this.IP, new MessageAnalyzer());
			}
		}

		private void Reconnect()
		{
			if (!this.cleaning && this.active)
			{
				this.Connect();
			}
		}

		private void OnConnectionSucceed(object sender, EventArgs e)
		{
			if (this.ConnectionSucceed != null)
			{
				this.ConnectionSucceed(this, e);
			}
			this.firstConnected = true;
			this.OnUserCountSchedule();
		}

		private void OnConnectionFailed(object sender, EventArgs<Exception> e)
		{
			if (this.ConnectionFailed != null)
			{
				this.ConnectionFailed(this, e);
			}
			if (this.firstConnected && this.AutoReconnect)
			{
				Scheduler.Schedule(HeroesCommandBridge.Thread, Job.Create(new Action(this.Reconnect)), 10000);
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
					Scheduler.Schedule(HeroesCommandBridge.Thread, Job.Create(new Action(this.Reconnect)), 10000);
				}
			}
			else
			{
				this.cleaning = false;
			}
			if (this.userCountSchedule != 0L)
			{
				Scheduler.Cancel(this.userCountSchedule);
				this.userCountSchedule = 0L;
			}
		}

		public void SendMessage<T>(T message)
		{
			if (this.client.Connected)
			{
				this.client.Transmit(SerializeWriter.ToBinary<T>(message));
			}
		}

		private void OnUserCountSchedule()
		{
			this.SendMessage<AdminRequestClientCountMessage2>(new AdminRequestClientCountMessage2());
			this.userCountSchedule = Scheduler.Schedule(HeroesCommandBridge.Thread, Job.Create(new Action(this.OnUserCountSchedule)), 30000);
		}

		private void OnPacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			HeroesAdminPeer.MF.Handle(packet, this);
		}

		private static void ProcessMessage(object rawMessage, object tag)
		{
			HeroesAdminPeer heroesAdminPeer = tag as HeroesAdminPeer;
			if (rawMessage is AdminReportClientCountMessage2 && heroesAdminPeer.UserCounted != null)
			{
				heroesAdminPeer.UserCounted(heroesAdminPeer, new EventArgs<AdminReportClientCountMessage2>(rawMessage as AdminReportClientCountMessage2));
			}
			if (rawMessage is AdminReportNotifyMessage && heroesAdminPeer.Notified != null)
			{
				AdminReportNotifyMessage value = rawMessage as AdminReportNotifyMessage;
				heroesAdminPeer.Notified(heroesAdminPeer, new EventArgs<AdminReportNotifyMessage>(value));
			}
		}

		public void RequestConsoleCommand(string cmd, string args)
		{
			this.SendMessage<AdminRequestConsoleCommandMessage>(new AdminRequestConsoleCommandMessage(cmd, args));
		}

		public void RequestUserCount()
		{
			this.SendMessage<AdminRequestClientCountMessage>(new AdminRequestClientCountMessage());
		}

		public void RequestServerStart()
		{
			this.SendMessage<AdminRequestServerStartMessage>(new AdminRequestServerStartMessage());
		}

		public void RequestNotify(string text)
		{
			this.SendMessage<AdminRequestNotifyMessage>(new AdminRequestNotifyMessage(text));
		}

		public void RequestItemFestival(string ItemClass, int Amount, string Message, bool IsCafe)
		{
			this.SendMessage<AdminItemFestivalEventMessage2>(new AdminItemFestivalEventMessage2
			{
				Amount = Amount,
				ItemClass = ItemClass,
				Message = Message,
				IsCafe = IsCafe
			});
		}

		public void RequestItemFestivalEx(string ItemClass, int Amount, bool IsExpire, DateTime? ExpireTime, bool IsCafe, string Message)
		{
			this.SendMessage<AdminItemFestivalEventMessage3>(new AdminItemFestivalEventMessage3
			{
				Amount = Amount,
				ItemClass = ItemClass,
				Message = Message,
				IsCafe = IsCafe,
				IsExprire = IsExpire,
				ExpireTime = ExpireTime
			});
		}

		public void RequestExtendCashItem(DateTime fromDate, int minutes)
		{
			this.SendMessage<AdminEntendCashItemExpire>(new AdminEntendCashItemExpire
			{
				FromDate = fromDate,
				Minutes = minutes
			});
		}

		public void RequestFreeToken(bool on)
		{
			this.SendMessage<AdminRequestFreeTokenMessage>(new AdminRequestFreeTokenMessage(on));
		}

		public void RequestShutDown(int time, string announce)
		{
			this.SendMessage<AdminRequestShutDownMessage>(new AdminRequestShutDownMessage(time, announce));
		}

		public void RequestStopService(string target, bool state)
		{
			this.SendMessage<AdminRequestEmergencyStopMessage>(new AdminRequestEmergencyStopMessage
			{
				TargetService = target,
				TargetState = state
			});
		}

		public void RequestKick(string uid, string cid)
		{
			if (cid == "")
			{
				this.SendMessage<AdminRequestKickMessage>(new AdminRequestKickMessage(uid, true));
				return;
			}
			this.SendMessage<AdminRequestKickMessage>(new AdminRequestKickMessage(cid, false));
		}

		public void RequestDSCheat(int on)
		{
			this.SendMessage<AdminRequestDSChetToggleMessage>(new AdminRequestDSChetToggleMessage(on));
		}

		private const int ReconnectInterval = 10000;

		private const int UserCountSchedulePeriod = 30000;

		private TcpClient client;

		private bool cleaning;

		private bool active;

		private bool firstConnected;

		private long userCountSchedule;

		private static MessageHandlerFactory MF = new MessageHandlerFactory();
	}
}
