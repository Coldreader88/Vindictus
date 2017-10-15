using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.RelayServiceOperations
{
	[Serializable]
	public sealed class Reserve : Operation
	{
		public long FrontendID { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
