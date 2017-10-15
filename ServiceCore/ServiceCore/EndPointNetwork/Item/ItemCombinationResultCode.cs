using System;

namespace ServiceCore.EndPointNetwork.Item
{
	public enum ItemCombinationResultCode
	{
		CombinationSuccess,
		ChangingSuccess,
		CombinationError_HasNotCombinedItem,
		CombinationError_NoCombinedItem,
		CombinationError_HasNotPartItem,
		CombinationError_NoPartItem,
		CombinationError_SamePartItem,
		CombinationError_NotSamePartsCount,
		CombinationError_NotSameGroup,
		CombinationError_HasNotAttribute,
		CombinationError_HasNotGold,
		CombinationError_HasNotMaterials,
		CombinationError_CreateItemFail,
		CombinationError_DestroyFail,
		CombinationError_SaveFail,
		CombinationError_WrongIndex,
		CombinationError_EnhanceLevelConstraint,
		CombinationError_NoEnhance,
		CombinationError_QualityLevelConstraint,
		CombinationError_NoQuality,
		CombinationError_EnchantLevelConstraint,
		CombinationError_NoEnchant,
		CombinationError_System
	}
}
