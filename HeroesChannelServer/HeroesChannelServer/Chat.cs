using System;
using Devcat.Core.Net.Message;
using HeroesChannelServer.Message;
using MMOServer;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer
{
	public class Chat : IComponent
	{
		public long ID { get; protected set; }

		public string Category
		{
			get
			{
				return "Chat";
			}
		}

		public bool IsTemporal
		{
			get
			{
				return true;
			}
		}

		public int SightDegree
		{
			get
			{
				return 1;
			}
		}

		public string Chatter { get; protected set; }

		public string Message { get; protected set; }

		protected Chat()
		{
		}

		public Chat(long id, string chatter, string msg)
		{
			this.ID = id;
			this.Message = msg;
			this.Chatter = chatter;
			this.binaryMessage = new PacketMessage
			{
				Data = SerializeWriter.ToBinary<NotifyChat>(new NotifyChat
				{
					Name = this.Chatter,
					Message = this.Message
				})
			};
		}

		public MMOServer.IMessage AppearMessage()
		{
			return this.binaryMessage;
		}

		public MMOServer.IMessage DifferenceMessage()
		{
			return null;
		}

		public MMOServer.IMessage DisappearMessage()
		{
			return null;
		}

		public void Flatten()
		{
		}

		protected internal PacketMessage binaryMessage;
	}
}
