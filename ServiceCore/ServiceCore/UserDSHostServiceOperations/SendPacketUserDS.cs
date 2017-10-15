using System;
using Devcat.Core.Net.Message;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.UserDSHostServiceOperations
{
	[Serializable]
	public sealed class SendPacketUserDS : Operation
	{
		public Packet Packet { get; private set; }

		static SendPacketUserDS()
		{
			MessageHandlerFactory.Register(Messages.TypeConverters);
		}

		private SendPacketUserDS(Packet packet)
		{
			this.Packet = packet;
		}

		public static SendPacketUserDS Create<T>(T serializeObject) where T : IMessage
		{
			Log<SendPacketUserDS>.Logger.InfoFormat("SendPacketUserDS create : [{0}]", serializeObject);
			Packet packet = SerializeWriter.ToBinary<T>(serializeObject);
			return new SendPacketUserDS(packet);
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
