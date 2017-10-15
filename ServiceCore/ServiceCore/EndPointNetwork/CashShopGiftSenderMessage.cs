using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CashShopGiftSenderMessage : IMessage
	{
		public int OrderNo { get; set; }

		public string SenderName { get; set; }

		public CashShopGiftSenderMessage(int orderNo, string senderName)
		{
			this.OrderNo = orderNo;
			this.SenderName = senderName;
		}

		public override string ToString()
		{
			return string.Format("CashShopGiftSenderMessage[ SenderName = {0} ]", this.SenderName);
		}
	}
}
