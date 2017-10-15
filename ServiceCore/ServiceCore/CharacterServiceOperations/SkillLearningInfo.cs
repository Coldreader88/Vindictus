using System;
using System.Collections.Generic;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SkillLearningInfo
	{
		public string LearningSkillID { get; private set; }

		public int CurrentAp { get; private set; }

		public ICollection<BriefSkillInfo> SkillList { get; private set; }

		public SkillLearningInfo(string learningSkillID, int currentAp, ICollection<BriefSkillInfo> skillList)
		{
			this.LearningSkillID = learningSkillID;
			this.CurrentAp = currentAp;
			this.SkillList = skillList;
		}
	}
}
