using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LearnNewSkillResultMessage : IMessage
	{
		public int result { get; set; }

		public LearnNewSkillResultMessage(LearnNewSkillResultMessage.LearnNewSkillResult result)
		{
			this.result = Convert.ToInt32(result);
		}

		public override string ToString()
		{
			return string.Format("LearnNewSkillResultMessage [result = {0}]", this.result);
		}

		public enum LearnNewSkillResult
		{
			Empty,
			Succeeced,
			Fail_NotEnoughGold,
			Fail_NoSuchSkill,
			Fail_Class,
			Fail_Level,
			Fail_Constraint,
			Fail_AlreadyLearnt,
			Fail_System,
			Fail_CannotBuySkill,
			Fail_Unknown
		}
	}
}
