using System;
using AdminClientServiceCore.Messages;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using ExecutionSupporter.Properties;

namespace ExecutionSupporter
{
	public class AdminClientNode
	{
		public event EventHandler<EventArgs> ConnectionSucceed;

		public event EventHandler<EventArgs> ConnectionFailed;

		public event EventHandler<EventArgs> Disconnected;

		private ExecutionSupportCore Parent { get; set; }

		public bool Valid
		{
			get
			{
				return this.valid;
			}
		}

		public AdminClientNode(ExecutionSupportCore core)
		{
			this.Parent = core;
			this.valid = false;
			this.peer.Connect(this.Parent.JobProcessor, Settings.Default.AdminServiceAddr, (int)Settings.Default.AdminServicePort, new MessageAnalyzer());
			this.peer.ConnectionSucceed += this.AtConnectionSucceed;
			this.peer.ConnectionFail += new EventHandler<EventArgs<Exception>>(this.AtConnectionFailed);
			this.peer.Disconnected += this.OnDisconnected;
			this.peer.PacketReceive += this.OnPacketReceive;
		}

		public bool IsConnected
		{
			get
			{
				return this.peer.Connected;
			}
		}

		public void TryConnect()
		{
			if (!this.peer.Connected)
			{
				try
				{
					this.peer.Connect(this.Parent.JobProcessor, Settings.Default.AdminServiceAddr, (int)Settings.Default.AdminServicePort, new MessageAnalyzer());
				}
				catch
				{
				}
			}
		}

		private void AtConnectionFailed(object sender, EventArgs e)
		{
			this.valid = false;
			this.ConnectionFailed(sender, e);
		}

		private void AtConnectionSucceed(object sender, EventArgs e)
		{
			this.valid = true;
			this.ConnectionSucceed(sender, e);
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			this.valid = false;
			this.Disconnected(sender, e);
		}

		private void OnPacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			this.Parent.MachineManager.MF.Handle(packet, this);
		}

		public void ProcessMessage(object message)
		{
			if (message is AdminReportClientcountMessage)
			{
				this.Parent.LogManager.SetUserCount(message as AdminReportClientcountMessage);
				return;
			}
			if (message is AdminReportNotifyMessage)
			{
				AdminReportNotifyMessage adminReportNotifyMessage = message as AdminReportNotifyMessage;
				this.Parent.NotifyServerMessage(adminReportNotifyMessage.Code, adminReportNotifyMessage.Message);
			}
		}

		public void RequestConsoleCommand(string cmd, string args)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestConsoleCommandMessage>(new AdminRequestConsoleCommandMessage(cmd, args)));
		}

		public void RequestUserCount()
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestClientCountMessage>(new AdminRequestClientCountMessage()));
		}

		public void RequestServerStart()
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestServerStartMessage>(new AdminRequestServerStartMessage()));
		}

		public void RequestNotify(string text)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestNotifyMessage>(new AdminRequestNotifyMessage(text)));
		}

		public void RequestItemFestival(string ItemClass, int Amount, string Message, bool IsCafe)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminItemFestivalEventMessage>(new AdminItemFestivalEventMessage
			{
				Amount = Amount,
				ItemClass = ItemClass,
				Message = Message,
				IsCafe = IsCafe
			}));
		}

		public void RequestFreeToken(bool on)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestFreeTokenMessage>(new AdminRequestFreeTokenMessage(on)));
		}

		public void RequestShutDown(int time, string announce)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestShutDownMessage>(new AdminRequestShutDownMessage(time, announce)));
		}

		public void RequestStopService(string target, bool state)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestEmergencyStopMessage>(new AdminRequestEmergencyStopMessage
			{
				TargetService = target,
				TargetState = state
			}));
		}

		public void RequestKick(string uid, string cid)
		{
			if (cid == "")
			{
				this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestKickMessage>(new AdminRequestKickMessage(uid, true)));
				return;
			}
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestKickMessage>(new AdminRequestKickMessage(cid, false)));
		}

		public void RequestDSCheat(int on)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestDSChetToggleMessage>(new AdminRequestDSChetToggleMessage(on)));
		}

		private TcpClient peer = new TcpClient();

		private bool valid;
	}
}
