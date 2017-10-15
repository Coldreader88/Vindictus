using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class FraudInspect : Operation
	{
		public int? NexonSN { get; set; }

		public string NexonID { get; set; }

		public string HWID { get; set; }

		public string OSVersion { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
