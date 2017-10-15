using System;

namespace ServiceCore.EndPointNetwork
{
	public enum TradeResultCode
	{
		Success,
		ExceptionOccured,
		NotEnoughMoney,
		SystemError,
		AlreadySoldOut,
		TimeLimitItemAddFail,
		NotTradableAddFile,
		EmptyList,
		SearchOverhead,
		SecondPasswordFailed
	}
}
