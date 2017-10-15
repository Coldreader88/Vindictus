using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;
using HeroesChannelServer.Message;
using MMOServer;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer
{
	public class HotSpringInfo : IComponent
	{
		public long ID { get; protected set; }

		public string Category
		{
			get
			{
				return "HotSpringInfo";
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

		public ICollection<HotSpringPotionEffectInfo> HotSpringPotionEffectInfos { get; set; }

		protected HotSpringInfo()
		{
		}

		public HotSpringInfo(ICollection<HotSpringPotionEffectInfo> hotSpringPotionEffectInfos, int townID)
		{
			this.HotSpringPotionEffectInfos = hotSpringPotionEffectInfos;
			this.binaryMessage = new PacketMessage
			{
				Data = SerializeWriter.ToBinary<HotSpringRequestInfoResultChannelMessage>(new HotSpringRequestInfoResultChannelMessage(this.HotSpringPotionEffectInfos, townID))
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
