using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class ResetShipList : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
