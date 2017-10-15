using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RelayServiceOperations
{
	[Serializable]
	public sealed class CloseConnection : Operation
	{
		public long FrontendID { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
