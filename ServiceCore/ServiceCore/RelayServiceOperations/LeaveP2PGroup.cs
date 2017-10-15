using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RelayServiceOperations
{
	[Serializable]
	public sealed class LeaveP2PGroup : Operation
	{
		public long FrontendID { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
