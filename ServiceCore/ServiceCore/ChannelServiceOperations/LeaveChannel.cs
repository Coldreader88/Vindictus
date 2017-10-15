using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ChannelServiceOperations
{
	[Serializable]
	public class LeaveChannel : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
