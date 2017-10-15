using System;

namespace ServiceCore.ItemServiceOperations
{
	public static class MailTypeExtension
	{
		public static bool IsAutoBlockType(this byte type)
		{
			return ((MailType)type).IsAutoBlockType();
		}

		public static bool IsAutoBlockType(this MailType type)
		{
			return type == MailType.ItemMail || type == MailType.ItemChargeMail || type == MailType.ChargePayMail || type == MailType.AuctionPayMail || type == MailType.AuctionItemMail;
		}
	}
}
