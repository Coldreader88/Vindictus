using System;
using System.Net;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.NGSecurityServiceOperations
{
	[Serializable]
	public sealed class NGSecurityLogIn : Operation
	{
		public int NexonSN { get; set; }

		public long CID { get; set; }

		public string Name { get; set; }

		public IPAddress IP { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
