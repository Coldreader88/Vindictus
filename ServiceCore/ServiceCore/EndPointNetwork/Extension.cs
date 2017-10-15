using System;

namespace ServiceCore.EndPointNetwork
{
	public static class Extension
	{
		public static LearnNewSkillResultMessage.LearnNewSkillResult ToLearnNewSkillResult(this string value)
		{
			if (value == "Succeeced")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Succeeced;
			}
			if (value == "Not_Enough_Gold")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_NotEnoughGold;
			}
			if (value == "No_Such_Skill")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_NoSuchSkill;
			}
			if (value == "Class")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_Class;
			}
			if (value == "Level")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_Level;
			}
			if (value == "Constraint")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_Constraint;
			}
			if (value == "Already_Learnt")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_AlreadyLearnt;
			}
			if (value == "Cannot_Buy_Skill")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_CannotBuySkill;
			}
			if (value == "System")
			{
				return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_System;
			}
			return LearnNewSkillResultMessage.LearnNewSkillResult.Fail_Unknown;
		}
	}
}
