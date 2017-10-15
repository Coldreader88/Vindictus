using System;
using Devcat.Core.Net.Message;
using MMOServer;

namespace HeroesChannelServer.Message
{
	public class PacketMessage : IMessage
	{
		public Packet Data { get; set; }
	}
}
