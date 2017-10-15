using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.HackShieldServiceOperations
{
	[Serializable]
	public sealed class TcProtectCheck : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
