using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.XignCodeServiceOperations
{
	[Serializable]
	public sealed class XignCodeSecureData : Operation
	{
		public byte[] SecureData { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
