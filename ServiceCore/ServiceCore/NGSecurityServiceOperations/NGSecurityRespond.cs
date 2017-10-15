using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.NGSecurityServiceOperations
{
	[Serializable]
	public sealed class NGSecurityRespond : Operation
	{
		public byte[] message { get; set; }

		public ulong checksum { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
