using System;

namespace ServiceCore.EndPointNetwork.Item
{
	public enum BraceletCombinationResultCode
	{
		CombinationSuccess,
		ChangingSuccess,
		ChangingSuccess_Rollback,
		CombinationError_NoBraceletItem,
		CombinationError_NoGemstoneItem,
		CombinationError_NotBraceletItem,
		CombinationError_NotGemstoneItem,
		CombinationError_DestoryItemFail,
		CombinationError_SaveFail,
		CombinationError_WrongGemstoneInfoAttribute,
		CombinationError_WrongStatAttribute,
		CombinationError_WrongGemstoneType,
		CombinationError_AlreadyCombined,
		CombinationError_NotCombined,
		CombinationError_AlreadyHasAttribute,
		CombinationError_HasNotAttribute,
		CombinationError_WrongGold,
		CombiantionError_RequiredGold,
		CombinationError_NotSameType,
		CombinationError_NotExistType,
		CombinationError_NotExistGrade,
		CombinationError_StatExtraction,
		CombinationError_NotExistGemstoneID,
		CombinationError_ChangingDiffStat,
		CombinationError_RollbackFail,
		CombinationError_RollbackNotSameID,
		CombinationError_PreviousGemstoneStat,
		CombinationError_newGemstoneStat
	}
}
