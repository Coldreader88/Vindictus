using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class TransferHostToUserDS : Operation
	{
		public GameInfo HostInfo { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
