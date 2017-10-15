using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PurchaseCharacterSlotMessage : IMessage
	{
		public int ProductNo { get; set; }

		public bool IsPremiumSlot { get; set; }

		public bool IsCredit { get; set; }

		public override string ToString()
		{
			return "PurchaseCharacterSlotMessage[]";
		}
	}
}
