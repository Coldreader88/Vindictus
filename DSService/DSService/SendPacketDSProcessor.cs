using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace DSService
{
	internal class SendPacketDSProcessor : EntityProcessor<SendPacketDS, DSEntity>
	{
		public SendPacketDSProcessor(DSService service, SendPacketDS op) : base(op)
		{
		}

		public override IEnumerable<object> Run()
		{
			base.Finished = true;
			if (base.Entity != null && base.Entity.TcpClient != null)
			{
				base.Entity.TcpClient.Transmit(base.Operation.Packet);
			}
			yield return new OkMessage();
			yield break;
		}
	}
}
