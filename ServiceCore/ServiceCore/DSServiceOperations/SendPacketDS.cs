using System;
using Devcat.Core.Net.Message;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.DSServiceOperations
{
	[Serializable]
	public sealed class SendPacketDS : Operation
	{
		public Packet Packet { get; private set; }

		static SendPacketDS()
		{
			MessageHandlerFactory.Register(Messages.TypeConverters);
		}

		private SendPacketDS(Packet packet)
		{
			this.Packet = packet;
		}

		public static SendPacketDS Create<T>(T serializeObject) where T : IMessage
		{
			Log<SendPacketDS>.Logger.InfoFormat("SendPacketDS create : [{0}]", serializeObject);
			Packet packet = SerializeWriter.ToBinary<T>(serializeObject);
			return new SendPacketDS(packet);
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
