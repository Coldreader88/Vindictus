using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class _CheatSetCafeStatusMessage : IMessage
	{
		public int CafeLevel { get; set; }

		public int CafeType { get; set; }

		public int SecureCode { get; set; }
	}
}
