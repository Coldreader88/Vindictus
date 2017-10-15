using System;
using Devcat.Core.Net.Message;
using HeroesChannelServer.Message;
using MMOServer;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer
{
	public class Kinetics : IComponent
	{
		public long ID { get; private set; }

		public string Category
		{
			get
			{
				return "Kinetics";
			}
		}

		public bool IsTemporal
		{
			get
			{
				return false;
			}
		}

		public int SightDegree
		{
			get
			{
				return 1;
			}
		}

		public ActionSync State
		{
			get
			{
				return this.state;
			}
			internal set
			{
				this.dirty = true;
				this.message = null;
				this.state = value;
			}
		}

		private PacketMessage BinaryMessage
		{
			get
			{
				if (this.message == null)
				{
					NotifyAction value = new NotifyAction
					{
						ID = this.ID,
						Action = this.State
					};
					this.message = new PacketMessage
					{
						Data = SerializeWriter.ToBinary<NotifyAction>(value)
					};
				}
				return this.message;
			}
		}

		public Kinetics(long id, ActionSync state)
		{
			this.ID = id;
			this.State = state;
		}

		public MMOServer.IMessage AppearMessage()
		{
			return this.BinaryMessage;
		}

		public MMOServer.IMessage DifferenceMessage()
		{
			if (this.dirty)
			{
				return this.AppearMessage();
			}
			return null;
		}

		public MMOServer.IMessage DisappearMessage()
		{
			return null;
		}

		public void Flatten()
		{
			this.dirty = false;
		}

		private ActionSync state;

		private bool dirty;

		private PacketMessage message;
	}
}
