using System;
using Devcat.Core.Net.Message;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class SendPacket : Operation
	{
		public Packet Packet { get; private set; }

		static SendPacket()
		{
			MessageHandlerFactory.Register(Messages.TypeConverters);
		}

		private SendPacket(Packet packet)
		{
			this.Packet = packet;
		}

		public static SendPacket Create<T>(T serializeObject) where T : IMessage
		{
			Log<SendPacket>.Logger.InfoFormat("SendPacket create : [{0}]", serializeObject);
			Packet packet = SerializeWriter.ToBinary<T>(serializeObject);
			return new SendPacket(packet);
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
