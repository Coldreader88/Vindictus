using System;
using AdminClient.Properties;
using AdminClientServiceCore.Messages;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;

namespace AdminClient
{
	public class AdminClientNode
	{
		public event EventHandler<EventArgs> ConnectionSucceed;

		public event EventHandler<EventArgs> ConnectionFailed;

		public event EventHandler<EventArgs> Disconnected;

		public bool Valid
		{
			get
			{
				return this.valid;
			}
		}

		public AdminClientNode(AdminClientForm p)
		{
			this.parent = p;
			this.valid = false;
			this.peer.Connect(p.Thread, Settings.Default.ConnectIP, (int)Settings.Default.ConnectPort, new MessageAnalyzer());
			this.peer.ConnectionSucceed += this.AtConnectionSucceed;
			this.peer.ConnectionFail += new EventHandler<EventArgs<Exception>>(this.AtConnectionFailed);
			this.peer.Disconnected += this.OnDisconnected;
			this.peer.PacketReceive += this.OnPacketReceive;
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
			this.Disconnected(sender, e);
		}

		private void OnPacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
            Packet packet = new Packet(e.Value);
            this.parent.MF.Handle(packet, this);
        }

		public void ProcessMessage(object message)
		{
			if (message is AdminReportClientcountMessage)
			{
				AdminReportClientcountMessage m = message as AdminReportClientcountMessage;
				this.parent.Invoke(new Action(delegate
				{
					this.parent.UpdateUserCountText(m.Value, m.Total, m.Waiting, m.States);
				}));
			}
			if (message is AdminReportEmergencyStopMessage)
			{
				AdminReportEmergencyStopMessage m = message as AdminReportEmergencyStopMessage;
				this.parent.Invoke(new Action(delegate
				{
					this.parent.UpdateStoppedServices(m.ServiceList);
				}));
			}
		}

		public void RequestUserCount()
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestClientCountMessage>(new AdminRequestClientCountMessage()));
		}

		public void RequestNotify(string text)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestNotifyMessage>(new AdminRequestNotifyMessage(text)));
		}

		public void RequestConsoleCommand(string text, bool isServerCommand)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminBroadcastConsoleCommandMessage>(new AdminBroadcastConsoleCommandMessage
			{
				commandString = text,
				isServerCommand = isServerCommand
			}));
		}

		public void RequestServerCommand(string text, string arg)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<AdminRequestConsoleCommandMessage>(new AdminRequestConsoleCommandMessage(text, arg)));
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

		private TcpClient peer = new TcpClient();

		private AdminClientForm parent;

		private bool valid;
	}
}
