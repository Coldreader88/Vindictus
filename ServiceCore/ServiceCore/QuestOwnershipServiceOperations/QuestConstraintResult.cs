using System;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	public enum QuestConstraintResult
	{
		InvalidQuest,
		NotAvailable,
		LevelConstraintFail,
		MaxPlayCountFail,
		RequiredStatusFail,
		DisableUserShip,
		NoSuchDifficulty,
		InvalidStoryPhase,
		Unknown,
		Success
	}
}
