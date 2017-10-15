using System;
using System.Net;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.NGSecurityServiceOperations
{
	[Serializable]
	public sealed class NGSecuritySet : Operation
	{
		public string characterName { get; set; }

		public IPAddress ipAddress { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
