using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MMOChannelServiceOperations
{
	[Serializable]
	public sealed class BurnGauge : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
