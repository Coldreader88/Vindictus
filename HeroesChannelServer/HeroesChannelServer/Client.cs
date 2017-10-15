using System;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using MMOServer;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;
using Utility;

namespace HeroesChannelServer
{
	public class Client
	{
		public long ID { get; private set; }

		public event EventHandler<EventArgs> Disconnected;

		public Player Player { get; private set; }

		public Client(Server parent, TcpClient client, MessageHandlerFactory mf)
		{
			this.parent = parent;
			this.client = client;
			this.client.Tag = this;
			this.mf = mf;
			this.client.PacketReceive += this.client_PacketReceive;
			this.client.Disconnected += delegate(object sender, EventArgs args)
			{
				if (this.Player != null)
				{
					this.Leave(this.Player.Channel);
				}
			};
			this.client.Disconnected += delegate(object sender, EventArgs args)
			{
				if (this.Disconnected != null)
				{
					this.Disconnected(sender, args);
				}
			};
		}

		private void client_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			if (e != null)
			{
				this.mf.Handle(new Packet(e.Value), this);
			}
		}

		public void ProcessMessage(object message)
		{
			if (this.ID == 0L && !(message is Identify))
			{
				Log<Client>.Logger.ErrorFormat("client should send Identify first : [{0}]", message);
				this.client.Transmit(SerializeWriter.ToBinary<IdentifyFailed>(new IdentifyFailed("GameUI_Heroes_Channel_Error_IdentifyFail")));
				this.client.Disconnect();
				return;
			}
			if (message is Identify)
			{
				Identify identify = message as Identify;
				if (!this.parent.VerifyKey(identify.ID, identify.Key))
				{
					this.client.Transmit(SerializeWriter.ToBinary<IdentifyFailed>(new IdentifyFailed("GameUI_Heroes_Channel_Error_KeyMismatch")));
					this.client.Disconnect();
					return;
				}
				this.ID = identify.ID;
				this.parent.IdentifyClient(this);
			}
			if (this.Player != null && message is ServiceCore.EndPointNetwork.IMessage)
			{
				this.Player.ProcessMessage(message as ServiceCore.EndPointNetwork.IMessage);
			}
		}

		public void Close()
		{
			this.ID = -1L;
			this.client.Disconnect();
		}

		public void Enter(Channel channel, long pid, ActionSync action, CharacterSummary look)
		{
			if (this.Player != null)
			{
				this.Leave(this.Player.Channel);
			}
			JobProcessor thread = channel.Tag as JobProcessor;
			this.Player = new Player(this.ID, channel, thread);
			this.Player.SendMessage += delegate(object sender, EventArgs<Packet> args)
			{
				this.client.Transmit(args.Value);
			};
			this.Player.Enter(pid, look, action);
		}

		public bool Leave(Channel channel)
		{
			if (this.Player == null)
			{
				return false;
			}
			if (this.Player.Channel != channel)
			{
				return false;
			}
			this.Player.LeaveChannel();
			this.Player = null;
			return true;
		}

		private Server parent;

		private TcpClient client;

		private MessageHandlerFactory mf;
	}
}
