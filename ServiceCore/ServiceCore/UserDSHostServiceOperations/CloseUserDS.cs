using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.UserDSHostServiceOperations
{
	[Serializable]
	public class CloseUserDS : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
