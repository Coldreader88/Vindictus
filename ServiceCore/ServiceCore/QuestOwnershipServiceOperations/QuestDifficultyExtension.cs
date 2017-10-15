using System;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	public static class QuestDifficultyExtension
	{
		public static int ToBitField(this int value)
		{
			return 1 << value;
		}
	}
}
