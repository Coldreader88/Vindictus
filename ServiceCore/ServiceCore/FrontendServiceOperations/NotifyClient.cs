using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class NotifyClient : Operation
	{
		public HeroesString Message { get; set; }

		public SystemMessageCategory Category { get; set; }

		public bool IsTownOnly { get; set; }

		public int AnnounceLevel { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
