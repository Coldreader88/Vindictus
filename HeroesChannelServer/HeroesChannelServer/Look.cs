using System;
using Devcat.Core.Net.Message;
using HeroesChannelServer.Message;
using MMOServer;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer
{
	public class Look : IComponent
	{
		public long ID { get; set; }

		public string Category
		{
			get
			{
				return "Look";
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

		public CharacterSummary Data
		{
			get
			{
				return this.data;
			}
			internal set
			{
				this.dirty = true;
				this.message = null;
				this.data = value;
			}
		}

		private PacketMessage BinaryMessage
		{
			get
			{
				if (this.message == null)
				{
					NotifyLook value = new NotifyLook
					{
						ID = this.ID,
						Look = this.Data
					};
					this.message = new PacketMessage
					{
						Data = SerializeWriter.ToBinary<NotifyLook>(value)
					};
				}
				return this.message;
			}
		}

		public Look(long id, CharacterSummary data)
		{
			this.ID = id;
			this.data = data;
		}

		public MMOServer.IMessage AppearMessage()
		{
			return this.BinaryMessage;
		}

		public MMOServer.IMessage DifferenceMessage()
		{
			if (this.dirty)
			{
				return this.BinaryMessage;
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

		private CharacterSummary data;

		private bool dirty;

		private PacketMessage message;
	}
}
