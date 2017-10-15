using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.NGSecurityServiceOperations
{
	[Serializable]
	public sealed class NGSecurityCallback : Operation
	{
		public IntPtr UserPtr { get; set; }

		public int NotifyType { get; set; }

		public byte[] Message { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
