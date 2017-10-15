using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DirectPurchaseTiticoreItemMessage : IMessage
	{
		public int ProductNo { get; set; }

		public bool IsCredit { get; set; }
	}
}
