using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class CounterEvent : Operation
	{
		public int TotalCount { get; set; }

		public int Level { get; set; }

		public string EventName { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
