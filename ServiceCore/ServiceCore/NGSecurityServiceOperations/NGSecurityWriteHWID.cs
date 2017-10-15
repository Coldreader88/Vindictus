using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.NGSecurityServiceOperations
{
	[Serializable]
	public sealed class NGSecurityWriteHWID : Operation
	{
		public long CID { get; set; }

		public int Type { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
