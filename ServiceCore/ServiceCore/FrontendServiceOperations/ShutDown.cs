using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class ShutDown : Operation
	{
		public int Delay { get; set; }

		public string Announce { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
