using System;
using Devcat.Core.Net.Message;
using HeroesChannelServer.Message;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer
{
	public class Megaphone : Chat
	{
		public Megaphone(long id, string chatter, string msg)
		{
			base.ID = id;
			base.Message = msg;
			base.Chatter = chatter;
			this.binaryMessage = new PacketMessage
			{
				Data = SerializeWriter.ToBinary<MegaphoneMessage>(new MegaphoneMessage
				{
					SenderName = chatter,
					Message = msg,
					MessageType = 1
				})
			};
		}
	}
}
