using System;
using Devcat.Core.Net.Message;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class BroadcastPacket : Operation
	{
		public Packet Packet { get; private set; }

		static BroadcastPacket()
		{
			MessageHandlerFactory.Register(Messages.TypeConverters);
		}

		private BroadcastPacket(Packet packet)
		{
			this.Packet = packet;
		}

		public static BroadcastPacket Create<T>(T serializeObject) where T : IMessage
		{
			Log<BroadcastPacket>.Logger.InfoFormat("BroadcastPacket create : [{0}]", serializeObject);
			Packet packet = SerializeWriter.ToBinary<T>(serializeObject);
			return new BroadcastPacket(packet);
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
