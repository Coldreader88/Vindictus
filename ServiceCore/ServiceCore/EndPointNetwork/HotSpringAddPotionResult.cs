using System;

namespace ServiceCore.EndPointNetwork
{
	public enum HotSpringAddPotionResult
	{
		Success,
		NotUsableChannel,
		NoItem,
		NoPotionInfo,
		BeforeChangeTime,
		LowerPotionLevel,
		OtherPlayerUseInTime,
		Unknown
	}
}
