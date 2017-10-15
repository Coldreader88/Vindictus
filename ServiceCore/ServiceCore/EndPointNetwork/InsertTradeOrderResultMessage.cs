using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class InsertTradeOrderResultMessage : IMessage
	{
		public int Result { get; set; }

		public enum ErrorCode
		{
			Ok,
			PriceOutOfRange,
			InternalError,
			InternalError_Tir,
			WebError,
			WebError_Tir,
			NoTicket,
			NotTradable,
			TimeLimitItem,
			NotEnoughLevel,
			PremiumTicketDisable,
			NoPremiumTicket
		}
	}
}
