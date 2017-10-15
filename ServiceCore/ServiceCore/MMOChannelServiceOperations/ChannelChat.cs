using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MMOChannelServiceOperations
{
	[Serializable]
	public sealed class ChannelChat : Operation
	{
		public long CID { get; set; }

		public string Message { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
