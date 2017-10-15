using System;

namespace ServiceCore.ItemServiceOperations
{
	public enum MailType : byte
	{
		Unknown,
		TextMail,
		ItemMail,
		ItemChargeMail,
		ChargePayMail,
		ReturnedMail,
		AuctionPayMail,
		AuctionItemMail,
		AuctionFailedMail,
		DungeonItemMail,
		EventMail,
		GMMail,
		GuildLevelUpItemMail
	}
}
