using System;

namespace ServiceCore.EndPointNetwork
{
	public enum EnchantItemResult
	{
		EnchantSuccess,
		EnchantFailed,
		RollSuccess,
		Canceled,
		OpenSessionFail,
		InvalidDiceClass,
		NoMoreRollAvailable,
		NoDiceItem,
		InvalidScrollItem,
		InvalidTargetItem,
		DBSubmitError,
		ScrollExpired,
		ItemDestroyed,
		ItemProtected,
		ScrollProtected,
		LevelConstraint,
		RuneExpired
	}
}
