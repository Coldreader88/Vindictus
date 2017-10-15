using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class NotifyEnhance : Operation
	{
		public string characterName { get; set; }

		public bool isSuccess { get; set; }

		public int nextEnhanceLevel { get; set; }

		public TooltipItemInfo item { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
