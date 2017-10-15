using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCore;
using ServiceCore.HeroesContents;
using Utility;

namespace DSService
{
	public static class DSContents
	{
		public static Dictionary<string, QuestInfo> QuestInfo { get; set; }

		public static Dictionary<string, int> QuestLevelConstraint { get; set; }

		static DSContents()
		{
			DSContents.Load();
			HeroesContentsLoader.DB3Changed += DSContents.Load;
		}

		public static void Initialize()
		{
		}

		private static void Load()
		{
			DSContents.QuestInfo = HeroesContentsLoader.GetTable<QuestInfo>().ToDictionary((QuestInfo x) => x.ID);
			DSContents.QuestLevelConstraint = (from x in HeroesContentsLoader.GetTable<QuestDifficultyInfo>()
			where ServiceCore.FeatureMatrix.IsEnable(x.Feature) && x.Difficulty == 2
			select x).ToDictionary((QuestDifficultyInfo x) => x.QuestID, (QuestDifficultyInfo x) => x.MinLevel);
		}

		public static QuestInfo GetQuestInfo(string questID)
		{
			return DSContents.QuestInfo.TryGetValue(questID);
		}

		public static int GetLevelConstraint(string questID)
		{
			return DSContents.QuestLevelConstraint.TryGetValue(questID);
		}
	}
}
